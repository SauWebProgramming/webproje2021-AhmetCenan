using ISE309.Odev.BLL.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ISE309.Odev.WebUI.Views.Shared.Components.Cart
{
    public class Cart : ViewComponent
    {
        private readonly Context _db;

        public Cart(Context db)
        {
            _db = db;
        }

        public IViewComponentResult Invoke()
        {
            //var model = _db.Menus;
            ViewBag.cart = new List<int>();
            ViewBag.menus = new List<int>();
            if (HttpContext.Session.GetString("Cart") != null)
            {
                var cartlist = HttpContext.Session.GetString("Cart");
                var cart = JsonSerializer.Deserialize<List<int>>(cartlist);
                var model = _db.Menus.Where(x=> cart.Contains(x.MenuID));
                ViewBag.cart = cart;
                ViewBag.menus = model;
                return View();
            }
            return View();
        }
    }
}
