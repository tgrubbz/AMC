using AMC.CORE.Models;
using System;

namespace AMC.WEB.ViewModels.Invoices
{
    public class InvoicesViewModel
    {
        public int InvoiceNumber { get; set; }
        public DateTime DateCreated { get; set; }
        public string Notary { get; set; }
        public string BillFrom { get; set; }
        public string BillTo { get; set; }
        public decimal Total { get; set; }

        public InvoicesViewModel(Invoice invoice)
        {
            InvoiceNumber = invoice.InvoiceNumber;
            DateCreated = invoice.DateCreated;
            Notary = invoice.Notary.Username;
            BillFrom = invoice.BillFrom.Name;
            BillTo = invoice.BillTo.Name;
            Total = invoice.Total;
        }
    }
}
