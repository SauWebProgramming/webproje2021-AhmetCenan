using ISE309.Odev.BLL.Data;
using ISE309.Odev.Core.DbEntities;
using ISE309.Odev.Shared.DTO;
using ISE309.Odev.Shared.DTO.Category;
using ISE309.Odev.Shared.DTO.Restaurant;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public readonly UserManager<AppUser> _userManager;
        public RestaurantController(Context db, UserManager<AppUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public IActionResult Index(int id)
        {
            var restaurant = _db.Restaurants.Include(x => x.Categories).ThenInclude(x => x.Menus)
                .Include(x => x.Categories).ThenInclude(x => x.Products)
                .Include(x => x.Owner)
                .Where(x => x.RestaurantID == id).FirstOrDefault();
            return View(restaurant);
        }
        
        [Authorize(Roles ="RestaurantOwner")]
        public IActionResult Edit(int id)
        {
            var ownerName = _db.Restaurants.Where(x => x.RestaurantID == id).Select(x => x.Owner.UserName).FirstOrDefault();
            var currentUserName = User.Identity.Name;
            if (ownerName != currentUserName)
            {
                return RedirectToAction("Error", "Home");
            }
            var rest = _db.Restaurants.Find(id);
            RestaurantEditDTO dto = new RestaurantEditDTO()
            {
                RestaurantAddress = rest.RestaurantAddress,
                RestaurantWorkingHours = rest.RestaurantWorkingHours,
                RestaurantMinDelivery = rest.RestaurantMinDelivery,
                RestaurantDeliveryTime = rest.RestaurantDeliveryTime,
                RestaurantLogoImage = rest.RestaurantLogoImage
            };
            ViewBag.id = rest.RestaurantID;
            return View(dto);
        }
        
        [HttpPost]
        [Authorize(Roles ="RestaurantOwner")]
        public IActionResult Edit(RestaurantEditDTO dto, int id)
        {
            if (ModelState.IsValid)
            {
                var ownerName = _db.Restaurants.Where(x => x.RestaurantID == id).Select(x => x.Owner.UserName).FirstOrDefault();
                var currentUserName = User.Identity.Name;
                if (ownerName != currentUserName)
                {
                    return RedirectToAction("Error", "Home");
                }
                var rest = _db.Restaurants.Find(id);
                rest.RestaurantAddress = dto.RestaurantAddress;
                rest.RestaurantWorkingHours = dto.RestaurantWorkingHours;
                rest.RestaurantMinDelivery = dto.RestaurantMinDelivery;
                rest.RestaurantDeliveryTime = dto.RestaurantDeliveryTime;
                rest.RestaurantLogoImage = dto.RestaurantLogoImage;
                _db.SaveChanges();
                return RedirectToAction("Index", new { id = id });
            }
            return View();
        }



        public IActionResult AddMenutoCart(int id)
        {
            List<CartItems> menus = new List<CartItems>();
            CartItems item = new CartItems();
            if (HttpContext.Session.GetString("Cart") != null)
            {
                var cartlist = HttpContext.Session.GetString("Cart");
                var oldcart = JsonSerializer.Deserialize<List<CartItems>>(cartlist);
                menus.AddRange(oldcart);
            }
            item.ItemName = _db.Menus.Where(x => x.MenuID == id).Select(x => x.MenuName).FirstOrDefault();
            item.ItemPrice = _db.Menus.Where(x => x.MenuID == id).Select(x => x.MenuPrice).FirstOrDefault();
            menus.Add(item);
            HttpContext.Session.SetString("Cart", JsonSerializer.Serialize(menus));
            return RedirectToAction("Index", "Home");
        }
        public IActionResult AddProducttoCart(int id)
        {
            List<CartItems> products = new List<CartItems>();
            CartItems item = new CartItems();
            if (HttpContext.Session.GetString("Cart") != null)
            {
                var cartlist = HttpContext.Session.GetString("Cart");
                var oldcart = JsonSerializer.Deserialize<List<CartItems>>(cartlist);
                products.AddRange(oldcart);
            }
            item.ItemName = _db.Products.Where(x => x.ProductID == id).Select(x => x.ProductName).FirstOrDefault();
            item.ItemPrice = _db.Products.Where(x => x.ProductID == id).Select(x => x.ProductPrice).FirstOrDefault();
            products.Add(item);
            HttpContext.Session.SetString("Cart", JsonSerializer.Serialize(products));
            return RedirectToAction("Index", "Home");
        }
        public IActionResult CartDelete()
        {
            HttpContext.Session.Remove("Cart");
            return RedirectToAction("Index", "Home");
        }
    }
}
