using ISE309.Odev.BLL.Data;
using ISE309.Odev.Core.DbEntities;
using ISE309.Odev.Shared.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ISE309.Odev.WebUI.Controllers
{
    public class OrderController : Controller
    {
        private readonly Context _db;
        public readonly UserManager<AppUser> _userManager;

        public OrderController(Context db, UserManager<AppUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public IActionResult Confirm()
        {
            ViewBag.cart = new List<CartItems>();
            ViewBag.desc = "";
            if (HttpContext.Session.GetString("Cart") != null)
            {
                var cartlist = HttpContext.Session.GetString("Cart");
                var cart = JsonSerializer.Deserialize<List<CartItems>>(cartlist);
                ViewBag.cart = cart;
                var orderDesc = "";
                foreach (var item in cart)
                {
                    orderDesc += item.ItemName + " ";
                }
                ViewBag.desc = orderDesc;
                double totalPrice = 0;
                foreach (var item in cart)
                {
                    totalPrice += item.ItemPrice;
                }
                totalPrice = Math.Round(totalPrice, 2);
                ViewBag.totalPrice = totalPrice;
                return View();
            }
            return View();
        }
        [HttpPost]
        public IActionResult Confirm(string address,string payment,string shippingdate)
        {
            ViewBag.cart = new List<CartItems>();
            ViewBag.desc = "";
            if (HttpContext.Session.GetString("Cart") != null)
            {
                var cartlist = HttpContext.Session.GetString("Cart");
                var cart = JsonSerializer.Deserialize<List<CartItems>>(cartlist);
                var orderDesc = "";
                foreach (var item in cart)
                {
                    orderDesc += item.ItemName + " ";
                }
                double totalPrice = 0;
                foreach (var item in cart)
                {
                    totalPrice += item.ItemPrice;
                }
                totalPrice = Math.Round(totalPrice, 2);
                var currentUser = _db.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
                var shipdate = DateTime.Parse(shippingdate);
                Order order = new Order()
                {
                    OrderDetails = orderDesc,
                    Customer = currentUser,
                    CustomerAddress = address,
                    OrderDate = DateTime.Now,
                    OrderShippingDate = shipdate,
                    OrderPrice =totalPrice,
                    OrderPayment = payment
                };
                _db.Orders.Add(order);
                _db.SaveChanges();
                HttpContext.Session.Remove("Cart");
                return RedirectToAction("Index","Home");
            }
            return View();
        }
    }
}
