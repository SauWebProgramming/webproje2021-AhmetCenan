using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISE309.Odev.Shared.DTO.Restaurant
{
    public class RestaurantEditDTO
    {
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
        [Display(Name = "Restoran Logosu")]
        public string RestaurantLogoImage { get; set; }
    }
}
