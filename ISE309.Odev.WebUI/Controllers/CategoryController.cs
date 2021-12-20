using ISE309.Odev.BLL.Data;
using ISE309.Odev.Core.DbEntities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISE309.Odev.WebUI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly Context _db;

        public CategoryController(Context db)
        {
            _db = db;
        }

        public IActionResult Index(int id)
        {
            IEnumerable<Category> model = _db.Categories.Where(x=>x.RestaurantID == id);
            ViewBag.res = _db.Restaurants.Find(id);
            return View(model);
        }
    }
}
