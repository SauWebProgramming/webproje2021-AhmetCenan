using ISE309.Odev.BLL.Data;
using ISE309.Odev.Core.DbEntities;
using ISE309.Odev.Shared.DTO.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISE309.Odev.WebUI.Controllers
{
    [Authorize(Roles = "RestaurantOwner")]
    public class CategoryController : Controller
    {

        private readonly Context _db;
        public readonly UserManager<AppUser> _userManager;

        public CategoryController(Context db, UserManager<AppUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public IActionResult CategoryList(int id)
        {
            var ownerName = _db.Restaurants.Where(x => x.RestaurantID == id).Select(x => x.Owner.UserName).FirstOrDefault();
            var currentUserName = User.Identity.Name;
            if (ownerName != currentUserName)
            {
                return RedirectToAction("Error", "Home");
            }
            var model = _db.Restaurants.Include(x => x.Categories)
                .Where(x => x.RestaurantID == id).FirstOrDefault();
            return View(model);
        }

        public IActionResult CategoryCreate(int id)
        {
            var ownerName = _db.Restaurants.Where(x => x.RestaurantID == id).Select(x => x.Owner.UserName).FirstOrDefault();
            var currentUserName = User.Identity.Name;
            if (ownerName != currentUserName)
            {
                return RedirectToAction("Error", "Home");
            }
            ViewBag.id = id;
            return View();
        }

        [HttpPost]
        public IActionResult CategoryCreate(CategoryCreateDTO category, int id)
        {
            if (ModelState.IsValid)
            {
                Category newCategory = new Category()
                {
                    CategoryName = category.CategoryName,
                    CategoryStatus = category.CategoryStatus,
                    CategoryCreateTime = DateTime.Now,
                    RestaurantID = id
                };
                _db.Categories.Add(newCategory);
                _db.SaveChanges();
                return RedirectToAction("CategoryList", new { id = id });
            }
            return View();
        }

        public IActionResult CategoryDelete(int id)
        {
            var cat = _db.Categories.Find(id);
            var ownerName = _db.Restaurants.Where(x => x.RestaurantID == cat.RestaurantID).Select(x => x.Owner.UserName).FirstOrDefault();
            var currentUserName = User.Identity.Name;
            if (ownerName != currentUserName)
            {
                return RedirectToAction("Error", "Home");
            }

            cat.CategoryStatus = false;
            _db.SaveChanges();
            return RedirectToAction("CategoryList", new { id = cat.RestaurantID });
        }

        public IActionResult CategoryEdit(int id)
        {
            var cat = _db.Categories.Find(id);
            var ownerName = _db.Restaurants.Where(x => x.RestaurantID == cat.RestaurantID).Select(x => x.Owner.UserName).FirstOrDefault();
            var currentUserName = User.Identity.Name;
            if (ownerName != currentUserName)
            {
                return RedirectToAction("Error", "Home");
            }
            CategoryEditDTO dto = new CategoryEditDTO()
            {
                CategoryName = cat.CategoryName,
                CategoryStatus = cat.CategoryStatus
            };
            ViewBag.id = cat.CategoryID;
            return View(dto);
        }

        [HttpPost]
        public IActionResult CategoryEdit(CategoryEditDTO category, int id)
        {
            if (ModelState.IsValid)
            {
                var cat = _db.Categories.Find(id);
                var ownerName = _db.Restaurants.Where(x => x.RestaurantID == cat.RestaurantID).Select(x => x.Owner.UserName).FirstOrDefault();
                var currentUserName = User.Identity.Name;
                if (ownerName != currentUserName)
                {
                    return RedirectToAction("Error", "Home");
                }
                cat.CategoryName = category.CategoryName;
                cat.CategoryStatus = category.CategoryStatus;
                _db.SaveChanges();
                return RedirectToAction("CategoryList", new { id = cat.RestaurantID });
            }
            return View();
        }
    }
}
