using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AMC.BLL.Interfaces;
using AMC.WEB.ViewModels.User;
using System.Linq;
using AMC.CORE.Models;
using AMC.WEB.ViewModels;

namespace AMC.WEB.Controllers
{
    [Authorize(Policy = "Admin")]
    public class UserController : Controller
    {
        IUserManager _userManager;

        public UserController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetTable()
        {
            AjaxResponse response = new AjaxResponse();
            TableResult<User> table = _userManager.GetUsersTable();

            response.Data = new TableResult<UserViewModel>(table.Items.Select(user => new UserViewModel(user)), table.Count);
            return Json(response);
        }
    }
}