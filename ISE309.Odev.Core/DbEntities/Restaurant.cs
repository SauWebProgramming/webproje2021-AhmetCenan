using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISE309.Odev.Core.DbEntities
{
    public class Restaurant
    {
        public int RestaurantID { get; set; }
        public string RestaurantName { get; set; }
        public string RestaurantOwner { get; set; }
        public string RestaurantAddress { get; set; }
        public string RestaurantWorkingHours { get; set; }
        public double RestaurantMinDelivery { get; set; }
        public int RestaurantDeliveryTime { get; set; }
        public double RestaurantRating { get; set; }
        public bool RestaurantStatus { get; set; }
        public DateTime RestaurantCreateTime { get; set; }
        public ICollection<Category> Categories { get; set; }
    }
}
