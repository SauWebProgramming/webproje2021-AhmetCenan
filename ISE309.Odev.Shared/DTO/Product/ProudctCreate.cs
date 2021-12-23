using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISE309.Odev.Shared.DTO.Product
{
    public class ProudctCreateDTO
    {
        [Required(ErrorMessage = "Lütfen Ürün Adı Giriniz.")]
        [Display(Name = "Ürün Adı")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "Lütfen Ürün Fiyatı Giriniz.")]
        [Display(Name = "Ürün Fiyatı")]
        public double ProductPrice { get; set; }
        [Required(ErrorMessage = "Lütfen Kategori Seçiniz.")]
        [Display(Name = "Kategori")]
        public int CategoryID { get; set; }
        [Display(Name = "Ürün Resmi")]
        public string ProductImage { get; set; }
        [Required(ErrorMessage = "Lütfen Gramaj Seçiniz.")]
        [Display(Name = "Gramaj")]
        public string Weights { get; set; }
    }
}
