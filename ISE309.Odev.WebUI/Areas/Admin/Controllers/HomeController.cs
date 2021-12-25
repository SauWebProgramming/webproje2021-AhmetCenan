using ISE309.Odev.BLL.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISE309.Odev.WebUI.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private readonly Context _db;

        public HomeController(Context db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var model = _db.Orders.Include(x=>x.Customer).OrderBy(x => x.OrderDate).ToList();
            ViewBag.restoran = _db.Restaurants.Count();
            ViewBag.kategori = _db.Categories.Count();
            ViewBag.menuurun = _db.Menus.Count() + _db.Products.Count();
            ViewBag.siparis = _db.Orders.Count();

            return View(model);
        }
    }
}
