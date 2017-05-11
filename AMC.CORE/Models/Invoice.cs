using System;

namespace AMC.CORE.Models
{
    public class Invoice
    {
        public int InvoiceNumber { get; set; }
        public DateTime DateCreated { get; set; }
        public User Notary { get; set; } // Notary who created the invoice
        public Location BillFrom { get; set; } // Location to send payment to
        public Location BillTo { get; set; } // Location to be billed
        public string Description { get; set; } // Description of the invoice
        public decimal Total { get; set; }
    }
}
