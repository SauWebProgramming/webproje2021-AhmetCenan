using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ISE309.Odev.WebUI.Areas.Admin.Models.UserDTO
{
    public class UserEditDTO
    {
        [Required(ErrorMessage = "Lütfen Kullanıcı Adı Giriniz.")]
        [Display(Name="Kullanıcı Adı")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Lütfen Adı Giriniz.")]
        [Display(Name = "Adı")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Lütfen SoyAdı Giriniz.")]
        [Display(Name = "SoyAdı")]
        public string SurName { get; set; }
        [Required(ErrorMessage = "Lütfen Email Giriniz.")]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
