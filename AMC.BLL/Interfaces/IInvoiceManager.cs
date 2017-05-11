using AMC.CORE.Models;

namespace AMC.BLL.Interfaces
{
    public interface IInvoiceManager
    {
        int Create(Invoice invoice);
        Invoice Read(int invoiceNumber);
        DataTableResult<Invoice> GetInvoicesTable(DataTableRequest request);
    }
}
