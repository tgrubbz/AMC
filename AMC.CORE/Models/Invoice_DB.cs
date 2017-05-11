using System;

namespace AMC.CORE.Models
{
    public class Invoice_DB
    {
        public int InvoiceNumber { get; set; }
        public DateTime DateCreated { get; set; }
        public int NotaryUserId { get; set; } // Notary who created the invoice
        public int BillFromLocationId { get; set; } // Location to send payment to
        public int BillToLocationId { get; set; } // Location to be billed
        public string Description { get; set; } // Description of the invoice
        public decimal Total { get; set; }
    }
}
