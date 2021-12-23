using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISE309.Odev.Shared.DTO.Category
{
    public class CategoryCreateDTO
    {
        [Required(ErrorMessage = "Lütfen Kategori Adı Giriniz.")]
        [Display(Name = "Kategori Adı")]
        public string CategoryName { get; set; }
        
        [Display(Name = "Aktif Olsun Mu?")]
        public bool CategoryStatus { get; set; }
    }
}
