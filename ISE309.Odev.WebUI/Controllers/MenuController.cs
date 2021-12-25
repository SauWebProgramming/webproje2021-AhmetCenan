using ISE309.Odev.BLL.Data;
using ISE309.Odev.Core.DbEntities;
using ISE309.Odev.Shared.DTO.Menu;
using ISE309.Odev.Shared.DTO.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISE309.Odev.WebUI.Controllers
{
    [Authorize(Roles = "RestaurantOwner")]
    public class MenuController : Controller
    {

        private readonly Context _db;
        public readonly UserManager<AppUser> _userManager;

        public MenuController(Context db, UserManager<AppUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public IActionResult MenuList(int id)
        {
            var ownerName = _db.Restaurants.Where(x => x.RestaurantID == id).Select(x => x.Owner.UserName).FirstOrDefault();
            var currentUserName = User.Identity.Name;
            if (ownerName != currentUserName)
            {
                return RedirectToAction("Error", "Home");
            }
            var model = _db.Menus.Include(x => x.Category).ThenInclude(x => x.Restaurant)
                .Where(x => x.Category.RestaurantID == id)
                .ToList();
            ViewBag.Restaurant = _db.Restaurants.Find(id);
            return View(model);
        }

        public IActionResult MenuCreate(int id)
        {
            var ownerName = _db.Restaurants.Where(x => x.RestaurantID == id).Select(x => x.Owner.UserName).FirstOrDefault();
            var currentUserName = User.Identity.Name;
            if (ownerName != currentUserName)
            {
                return RedirectToAction("Error", "Home");
            }
            ViewBag.id = id;
            ViewBag.categories = _db.Categories.Where(x => x.RestaurantID == id).ToList();
            return View();
        }

        [HttpPost]
        public IActionResult MenuCreate(MenuCreateDTO menu, int id)
        {
            if (ModelState.IsValid)
            {
                Menu newMenu = new Menu()
                {
                    MenuName = menu.MenuName,
                    MenuPrice = menu.MenuPrice,
                    CategoryID = menu.CategoryID,
                    MenuImage = menu.MenuImage,
                    MenuStatus = true,
                    MenuCreateTime = DateTime.Now
                };
                _db.Menus.Add(newMenu);
                _db.SaveChanges();
                return RedirectToAction("MenuList", new { id = id });
            }
            return View();
        }

        public IActionResult MenuDelete(int id)
        {
            var menu = _db.Menus.Include(x => x.Category).Where(x => x.MenuID == id).FirstOrDefault();
            var ownerName = _db.Restaurants.Where(x => x.RestaurantID == menu.Category.RestaurantID).Select(x => x.Owner.UserName).FirstOrDefault();
            var currentUserName = User.Identity.Name;
            if (ownerName != currentUserName)
            {
                return RedirectToAction("Error", "Home");
            }
            menu.MenuStatus = false;
            _db.SaveChanges();
            return RedirectToAction("MenuList", new { id = menu.Category.RestaurantID });
        }

        public IActionResult MenuEdit(int id)
        {

            var menu = _db.Menus.Include(x => x.Category).Where(x => x.MenuID == id).FirstOrDefault();
            var ownerName = _db.Restaurants.Where(x => x.RestaurantID == menu.Category.RestaurantID).Select(x => x.Owner.UserName).FirstOrDefault();
            var currentUserName = User.Identity.Name;
            if (ownerName != currentUserName)
            {
                return RedirectToAction("Error", "Home");
            }
            MenuEditDTO dto = new MenuEditDTO()
            {
                MenuName = menu.MenuName,
                MenuPrice = menu.MenuPrice,
                MenuImage = menu.MenuImage,
                MenuStatus = menu.MenuStatus,
            };
            ViewBag.id = menu.MenuID;
            return View(dto);
        }

        [HttpPost]
        public IActionResult MenuEdit(MenuEditDTO menudto, int id)
        {
            if (ModelState.IsValid)
            {
                var menu = _db.Menus.Include(x => x.Category).Where(x => x.MenuID == id).FirstOrDefault();
                var ownerName = _db.Restaurants.Where(x => x.RestaurantID == menu.Category.RestaurantID).Select(x => x.Owner.UserName).FirstOrDefault();
                var currentUserName = User.Identity.Name;
                if (ownerName != currentUserName)
                {
                    return RedirectToAction("Error", "Home");
                }
                menu.MenuName = menudto.MenuName;
                menu.MenuPrice = menudto.MenuPrice;
                menu.MenuImage = menudto.MenuImage;
                menu.MenuStatus = menudto.MenuStatus;
                _db.SaveChanges();
                return RedirectToAction("MenuList", new { id = menu.Category.RestaurantID });
            }
            return View();
        }
    }
}
