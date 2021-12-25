using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ISE309.Odev.WebUI.Areas.Admin.Models.RestaurantDTO
{
    public class RestaurantEditDTO
    {
        [Required(ErrorMessage = "Lütfen Restoran Adı Giriniz.")]
        [Display(Name = "Restoran Adı")]
        public string RestaurantName { get; set; }
        [Required(ErrorMessage = "Lütfen Restoran Sahibi Seçiniz.")]
        [Display(Name = "Restoran Sahibi")]
        public int RestaurantOwner { get; set; }
        [Required(ErrorMessage = "Lütfen Restoran Adresi Giriniz.")]
        [Display(Name = "Restoran Adresi")]
        public string RestaurantAddress { get; set; }
        [Required(ErrorMessage = "Lütfen Restoran Çalışma Saatleri Giriniz.")]
        [Display(Name = "Çalışma Saatleri")]
        public string RestaurantWorkingHours { get; set; }
        [Required(ErrorMessage = "Lütfen Restoran Minimum Sipariş Ücreti Giriniz.")]
        [Display(Name = "Minimum Sipariş Ücreti")]
        public double RestaurantMinDelivery { get; set; }
        [Required(ErrorMessage = "Lütfen Restoran Servis Süresi Giriniz.")]
        [Display(Name = "Servis Süresi")]
        public int RestaurantDeliveryTime { get; set; }
        [Required(ErrorMessage = "Lütfen Restoran Derecesi Giriniz.")]
        [Display(Name = "Restoran Derecesi")]
        public double RestaurantRating { get; set; }
        [Display(Name = "Restoran Logosu")]
        public string RestaurantLogoImage { get; set; }
        [Display(Name = "Restoran Tekrar Aktif Olsun mu")]
        public bool RestaurantStatus { get; set; }
    }
}
