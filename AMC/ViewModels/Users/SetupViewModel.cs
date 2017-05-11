using AMC.CORE.Enumerations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AMC.WEB.ViewModels.Users
{
    public class SetupViewModel
    {
        public string Username { get; set; }
        
        [Required]
        public UserRole Role { get; set; }
        public List<SelectListItem> RoleList { get; set; }

        public SetupViewModel()
        {
        }
    } 
}
