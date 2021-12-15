using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISE309.Odev.Core.DbEntities
{
    public class Weight
    {
        public int WeightID { get; set; }
        public double WeightValue { get; set; }
        public string WeightType { get; set; }
        public List<Product> Products { get; set; }
    }
}
