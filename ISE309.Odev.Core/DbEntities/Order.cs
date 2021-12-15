using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISE309.Odev.Core.DbEntities
{
    public class Order
    {
        public int OrderID { get; set; }
        public string CustomerID { get; set; }
        public string CustomerAddress { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? OrderShippingDate { get; set; }
        public double OrderPrice { get; set; }
        public string OrderPayment { get; set; }
        public int RestaurantID { get; set; }

        [ForeignKey(nameof(RestaurantID))]
        public Restaurant Restaurant { get; set; }
    }
}
