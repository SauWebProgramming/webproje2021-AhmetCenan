using ISE309.Odev.BLL.Data;
using ISE309.Odev.Core.DbEntities;
using ISE309.Odev.Shared.DTO;
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
            ViewBag.cart = new List<CartItems>();
            if (HttpContext.Session.GetString("Cart") != null)
            {
                var cartlist = HttpContext.Session.GetString("Cart");
                var cart = JsonSerializer.Deserialize<List<CartItems>>(cartlist);
                ViewBag.cart = cart;
                double totalPrice = 0;
                foreach (var item in cart)
                {
                    totalPrice += item.ItemPrice;
                }
                totalPrice = Math.Round(totalPrice,2);
                ViewBag.totalPrice = totalPrice;
                return View();
            }
            return View();
        }
    }
}
