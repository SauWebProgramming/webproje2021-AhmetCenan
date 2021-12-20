using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISE309.Odev.Core.DbEntities
{
    public class Menu
    {
        public int MenuID { get; set; }
        public string MenuName { get; set; }
        public double MenuPrice { get; set; }
        public int? CategoryID { get; set; }
        public bool MenuStatus { get; set; }
        public DateTime MenuCreateTime { get; set; }

        [ForeignKey(nameof(CategoryID))]
        public Category Category { get; set; }
        public ICollection<MenuProduct> MenuProducts { get; set; }
    }
}
