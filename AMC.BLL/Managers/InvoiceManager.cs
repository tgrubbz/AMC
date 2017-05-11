using System;
using AMC.BLL.Interfaces;
using AMC.CORE.Models;
using AMC.DAL.Interfaces;

namespace AMC.BLL.Managers
{
    public class InvoiceManager : IInvoiceManager
    {
        IInvoiceRepository _invoiceRepo;
        ILocationRepository _locationRepo;
        IUserRepository _userRepo;
        public InvoiceManager(IInvoiceRepository invoiceRepo, ILocationRepository locationRepo, IUserRepository userRepo)
        {
            _invoiceRepo = invoiceRepo;
            _locationRepo = locationRepo;
            _userRepo = userRepo;
        }

        public int Create(Invoice invoice)
        {
            // Check for existing user
            User notary = _userRepo.Read(invoice.Notary.Id);
            if(notary == null)
            {
                // TODO: return a manager result
                return 0;
            }

            // Check for existing location
            invoice.BillFrom = _locationRepo.Read(invoice.BillFrom.Id);
            if(invoice.BillFrom == null)
            {
                // Create a new location
                invoice.BillFrom.Id = _locationRepo.Create(invoice.BillFrom);
            }
            if (invoice.BillFrom.Id == 0)
            {
                // TODO: return a manager result
                return 0;
            }

            // Check for existing location
            invoice.BillTo = _locationRepo.Read(invoice.BillTo.Id);
            if (invoice.BillTo == null)
            {
                // Create a new location
                invoice.BillTo.Id = _locationRepo.Create(invoice.BillTo);
            }
            if (invoice.BillTo.Id == 0)
            {
                // TODO: return a manager result
                return 0;
            }

            // Create the invoice database item
            Invoice_DB invoice_db = new Invoice_DB();
            invoice_db.BillFromLocationId = invoice.BillFrom.Id;
            invoice_db.BillToLocationId = invoice.BillTo.Id;
            invoice_db.Description = invoice.Description;
            invoice_db.NotaryUserId = invoice.Notary.Id;
            invoice_db.Total = invoice.Total;

            return _invoiceRepo.Create(invoice_db);
        }

        public DataTableResult<Invoice> GetInvoicesTable(DataTableRequest request)
        {
            return _invoiceRepo.GetTable(request);
        }

        public Invoice Read(int invoiceNumber)
        {
            Invoice invoice = new Invoice();
            Invoice_DB invoice_db = _invoiceRepo.Read(invoiceNumber);

            // Copy the database fields
            invoice.DateCreated = invoice_db.DateCreated;
            invoice.Description = invoice_db.Description;
            invoice.InvoiceNumber = invoice_db.InvoiceNumber;
            invoice.Total = invoice_db.Total;

            // Lookup the nested objects
            invoice.Notary = _userRepo.Read(invoice_db.NotaryUserId);
            invoice.BillFrom = _locationRepo.Read(invoice_db.BillFromLocationId);
            invoice.BillTo = _locationRepo.Read(invoice_db.BillToLocationId);

            return invoice;
        }
    }
}
