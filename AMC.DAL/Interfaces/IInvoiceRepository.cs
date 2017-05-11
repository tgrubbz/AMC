using AMC.CORE.Models;

namespace AMC.DAL.Interfaces
{
    public interface IInvoiceRepository
    {
        int Create(Invoice_DB invoice);
        Invoice_DB Read(int invoiceNumber);
        DataTableResult<Invoice> GetTable(DataTableRequest request);
    }
}
