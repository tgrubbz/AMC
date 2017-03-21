using Microsoft.AspNetCore.Mvc;

namespace AMC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
