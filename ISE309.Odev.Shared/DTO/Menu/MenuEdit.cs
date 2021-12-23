using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISE309.Odev.Shared.DTO.Menu
{
    public class MenuEditDTO
    {
        [Required(ErrorMessage = "Lütfen Ürün Adı Giriniz.")]
        [Display(Name = "Ürün Adı")]
        public string MenuName { get; set; }
        [Required(ErrorMessage = "Lütfen Ürün Fiyatı Giriniz.")]
        [Display(Name = "Ürün Fiyatı")]
        public double MenuPrice { get; set; }
        [Display(Name = "Ürün Resmi")]
        public string MenuImage { get; set; }
        [Display(Name = "Tekrar Aktif Olsun mu?")]
        public bool MenuStatus { get; set; }
    }
}
