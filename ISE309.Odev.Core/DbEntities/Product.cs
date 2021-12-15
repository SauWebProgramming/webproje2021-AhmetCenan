using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISE309.Odev.Core.DbEntities
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public int CategoryID { get; set; }
        public bool ProductStatus { get; set; }
        public DateTime ProductCreateTime { get; set; }

        [ForeignKey(nameof(CategoryID))]
        public Category Category { get; set; }
        public List<Weight> Weights { get; set; }
        public List<MenuProduct> MenuProducts { get; set; }
    }
}
