using ISE309.Odev.BLL.Data;
using ISE309.Odev.Core.DbEntities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ISE309.Odev.WebUI.Controllers
{
    public class RestaurantController : Controller
    {
        private readonly Context _db;
        public RestaurantController(Context db)
        {
            _db = db;
        }
        public IActionResult Index(int id)
        {
            IEnumerable<Category> model = _db.Categories.Where(x => x.RestaurantID == id);
            ViewBag.res = _db.Restaurants.Find(id);
            ViewBag.catprod = _db.Menus;
            return View(model);
        }

        public IActionResult AddCart(int id)
        {
            List<int> list = new List<int>();
            if (HttpContext.Session.GetString("Cart") != null)
            {
                var cartlist = HttpContext.Session.GetString("Cart");
                var oldcart = JsonSerializer.Deserialize<List<int>>(cartlist);
                list.AddRange(oldcart);
            }
            list.Add(id);
            HttpContext.Session.SetString("Cart",JsonSerializer.Serialize(list));
            return RedirectToAction("Index","Home");
        }
    }
}
