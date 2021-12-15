using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISE309.Odev.Core.DbEntities
{
    public class Category
    {
        public int CategoryID { get; set; }
        public int RestaurantID { get; set; }
        public string CategoryName { get; set; }
        public bool CategoryStatus { get; set; }
        public DateTime CategoryCreateTime { get; set; }

        [ForeignKey(nameof(RestaurantID))]
        public Restaurant Restaurant { get; set; }
    }
}
