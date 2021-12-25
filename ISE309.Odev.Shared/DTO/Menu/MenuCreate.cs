using ISE309.Odev.Shared.DTO.Product;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISE309.Odev.Shared.DTO.Menu
{
    public class MenuCreateDTO
    {
        [Required(ErrorMessage = "Lütfen Menü Adı Giriniz.")]
        [Display(Name = "Menü Adı")]
        public string MenuName { get; set; }
        [Required(ErrorMessage = "Lütfen Menü Fiyatı Giriniz.")]
        [Display(Name = "Menü Fiyatı")]
        public double MenuPrice { get; set; }
        [Required(ErrorMessage = "Lütfen Kategori Seçiniz.")]
        [Display(Name = "Kategori")]
        public int CategoryID { get; set; }
        [Display(Name = "Menü Resmi")]
        public string MenuImage { get; set; }
    }
}
