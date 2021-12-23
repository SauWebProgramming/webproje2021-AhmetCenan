using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISE309.Odev.Core.DbEntities
{
    public class MenuProduct
    {
        public int? MenuID { get; set; }
        public Menu Menu { get; set; }
        public int? ProductID { get; set; }
        public Product Product { get; set; }
        public int Count { get; set; }
    }
}
