using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Runtime.CompilerServices;

namespace AMC.BLL
{
    /// <summary>
    /// Based on ASP.NET Core Identity's PasswordHasher
    /// https://github.com/aspnet/Identity/blob/5480aa182bad3fb3b729a0169d0462873331e306/src/Microsoft.AspNetCore.Identity/PasswordHasher.cs
    /// </summary>
    public static class PasswordHasher
    {
        private static int _saltSize = 128 / 8;
        private static KeyDerivationPrf _kdprf = KeyDerivationPrf.HMACSHA256;
        private static int _iterations = 10000;
        private static int _numBytesRequested = 256 / 8;
        private static readonly RandomNumberGenerator _rng = RandomNumberGenerator.Create(); // secure PRNG

        private static uint ReadNetworkByteOrder(byte[] buffer, int offset)
        {
            return ((uint)(buffer[offset + 0]) << 24)
                | ((uint)(buffer[offset + 1]) << 16)
                | ((uint)(buffer[offset + 2]) << 8)
                | ((uint)(buffer[offset + 3]));
        }

        private static void WriteNetworkByteOrder(byte[] buffer, int offset, uint value)
        {
            buffer[offset + 0] = (byte)(value >> 24);
            buffer[offset + 1] = (byte)(value >> 16);
            buffer[offset + 2] = (byte)(value >> 8);
            buffer[offset + 3] = (byte)(value >> 0);
        }

        // Compares two byte arrays for equality. The method is specifically written so that the loop is not optimized.
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        private static bool ByteArraysEqual(byte[] a, byte[] b)
        {
            if (a == null && b == null)
            {
                return true;
            }
            if (a == null || b == null || a.Length != b.Length)
            {
                return false;
            }
            var areSame = true;
            for (var i = 0; i < a.Length; i++)
            {
                areSame &= (a[i] == b[i]);
            }
            return areSame;
        }

        public static string HashPassword(string password)
        {
            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }

            // Get a crypto secure byte array
            byte[] salt = new byte[_saltSize];
            _rng.GetBytes(salt);

            // Derive the key
            byte[] subkey = KeyDerivation.Pbkdf2(password, salt, _kdprf, _iterations, _numBytesRequested);
            byte[] outputBytes = new byte[13 + salt.Length + subkey.Length];

            // Write the output bytes
            outputBytes[0] = 0x01; // format marker
            WriteNetworkByteOrder(outputBytes, 1, (uint)_kdprf);
            WriteNetworkByteOrder(outputBytes, 5, (uint)_iterations);
            WriteNetworkByteOrder(outputBytes, 9, (uint)_saltSize);

            // Copy the salt and key
            Buffer.BlockCopy(salt, 0, outputBytes, 13, salt.Length);
            Buffer.BlockCopy(subkey, 0, outputBytes, 13 + _saltSize, subkey.Length);

            return Convert.ToBase64String(outputBytes);
        }

        public static bool VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            if (hashedPassword == null)
            {
                throw new ArgumentNullException(nameof(hashedPassword));
            }
            if (providedPassword == null)
            {
                throw new ArgumentNullException(nameof(providedPassword));
            }

            byte[] decodedHashedPassword = Convert.FromBase64String(hashedPassword);

            try
            {
                // Read header information
                KeyDerivationPrf prf = (KeyDerivationPrf)ReadNetworkByteOrder(decodedHashedPassword, 1);
                int iterations = (int)ReadNetworkByteOrder(decodedHashedPassword, 5);
                int saltSize = (int)ReadNetworkByteOrder(decodedHashedPassword, 9);

                // Read the salt: must be >= 128 bits
                if (saltSize < _saltSize)
                {
                    return false;
                }
                byte[] salt = new byte[saltSize];
                Buffer.BlockCopy(decodedHashedPassword, 13, salt, 0, salt.Length);

                // Read the subkey (the rest of the payload): must be >= 128 bits
                int subkeyLength = decodedHashedPassword.Length - 13 - salt.Length;
                if (subkeyLength < _saltSize)
                {
                    return false;
                }
                byte[] expectedSubkey = new byte[subkeyLength];
                Buffer.BlockCopy(decodedHashedPassword, 13 + salt.Length, expectedSubkey, 0, expectedSubkey.Length);

                // Hash the incoming password and verify it
                byte[] actualSubkey = KeyDerivation.Pbkdf2(providedPassword, salt, prf, iterations, subkeyLength);
                return ByteArraysEqual(actualSubkey, expectedSubkey);
            }
            catch
            {
                // This should never occur except in the case of a malformed payload, where
                // we might go off the end of the array. Regardless, a malformed payload
                // implies verification failed.
                return false;
            }
        }
    }
}
