using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AMC.BLL.Interfaces;
using AMC.WEB.ViewModels.Users;
using System.Linq;
using AMC.CORE.Models;
using AMC.WEB.ViewModels;
using AMC.CORE.Enumerations;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace AMC.WEB.Controllers
{
    [Authorize(Policy = "Admin")]
    public class UsersController : Controller
    {
        IUserManager _userManager;

        public UsersController(IUserManager userManager)
        {
            _userManager = userManager;
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
            DataTableResult<User> table = _userManager.GetUsersTable(request);

            response.Data = new DataTableResult<UsersViewModel>(table.Items.Select(user => new UsersViewModel(user)), table.Length);
            return Json(response);
        }

        [HttpGet]
        [Route("/Users/Setup/{username}")]
        public IActionResult Setup(string username)
        {
            // Check if the username exists
            User user = _userManager.Read(username);
            if (user == null)
            {
                // TODO: add tempdata msg
                return RedirectToAction("Index");
            }

            SetupViewModel model = new SetupViewModel();
            model.Username = user.Username;
            model.Role = user.Role;

            // Create the UserRole list
            model.RoleList = new List<SelectListItem>();
            foreach (UserRole role in Enum.GetValues(typeof(UserRole)))
            {
                model.RoleList.Add(new SelectListItem() { Text = role.ToFriendlyString(), Value = role.ToString() });
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Setup(SetupViewModel model)
        {
            // Check if the username exists
            User user = _userManager.Read(model.Username);
            if (user == null)
            {
                // TODO: add tempdata msg
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                // Setup the user
                user.Role = model.Role;

                if(_userManager.Setup(user) > 0)
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("Username", "User could not be setup");
            }
            
            // Create the UserRole list
            model.RoleList = new List<SelectListItem>();
            foreach (UserRole role in Enum.GetValues(typeof(UserRole)))
            {
                model.RoleList.Add(new SelectListItem() { Text = role.ToFriendlyString(), Value = role.ToString() });
            }

            return View(model);
        }

        [HttpGet]
        [Route("/Users/Delete/{username}")]
        public IActionResult Delete(string username)
        {
            // Check if the username exists
            User user = _userManager.Read(username);
            if (user == null)
            {
                // TODO: add tempdata msg
                return RedirectToAction("Index");
            }

            DeleteViewModel model = new DeleteViewModel();
            model.Username = user.Username;

            return PartialView("_Delete", model);
        }

        [HttpPost]
        public IActionResult Delete(DeleteViewModel model)
        {
            // Check if the username exists
            User user = _userManager.Read(model.Username);
            if (user == null)
            {
                // TODO: add tempdata msg
                return RedirectToAction("Index");
            }
            
            if(ModelState.IsValid)
            {
                if (_userManager.Delete(user.Id) > 0)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }

        //[HttpGet]
        //public IActionResult Edit(string username)
        //{
        //    // Check if the username exists
        //    User user = _userManager.Read(username);
        //    if (user == null)
        //    {
        //        // TODO: add tempdata msg
        //        return RedirectToAction("Index");
        //    }

        //    EditViewModel model = new EditViewModel();
        //    model.Username = user.Username;
        //    model.Role = user.Role;
        //    model.IsRegistered = user.IsRegistered;
        //    model.IsSetup = user.IsSetup;

        //    // Create the UserRole list
        //    model.RoleList = new List<SelectListItem>();
        //    foreach (UserRole role in Enum.GetValues(typeof(UserRole)))
        //    {
        //        model.RoleList.Add(new SelectListItem() { Text = role.ToFriendlyString(), Value = role.ToString() });
        //    }

        //    return View(model);
        //}

        //[HttpPost]
        //public IActionResult Edit(EditViewModel model)
        //{
        //    // Check if the username exists
        //    User user = _userManager.Read(model.Username);
        //    if (user == null)
        //    {
        //        // TODO: add tempdata msg
        //        return RedirectToAction("Index");
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        // Set the user's role
        //        user.Role = model.Role;
        //        user.IsRegistered = model.IsRegistered;
        //        user.IsSetup = model.IsSetup;

        //        if (_userManager.Setup(user) > 0)
        //        {
        //            return RedirectToAction("Index");
        //        }

        //        ModelState.AddModelError("Username", "User could not be setup");
        //    }

        //    // Create the UserRole list
        //    model.RoleList = new List<SelectListItem>();
        //    foreach (UserRole role in Enum.GetValues(typeof(UserRole)))
        //    {
        //        model.RoleList.Add(new SelectListItem() { Text = role.ToFriendlyString(), Value = role.ToString() });
        //    }

        //    return View(model);
        //}
    }
}