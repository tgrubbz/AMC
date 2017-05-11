using AMC.BLL.Interfaces;
using AMC.CORE.Models;
using AMC.WEB.ViewModels;
using AMC.WEB.ViewModels.Invoices;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AMC.WEB.Controllers
{
    public class InvoiceController : Controller
    {
        IInvoiceManager _invoiceManager;

        public InvoiceController(IInvoiceManager invoiceManager)
        {
            _invoiceManager = invoiceManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetTable(DataTableRequest request)
        {
            AjaxResponse response = new AjaxResponse();
            DataTableResult<Invoice> table = _invoiceManager.GetInvoicesTable(request);

            response.Data = new DataTableResult<InvoicesViewModel>(table.Items.Select(invoice => new InvoicesViewModel(invoice)), table.Length);
            return Json(response);
        }
    }
}