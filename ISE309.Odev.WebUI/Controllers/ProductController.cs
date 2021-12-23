using ISE309.Odev.BLL.Data;
using ISE309.Odev.Core.DbEntities;
using ISE309.Odev.Shared.DTO.Product;
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
    public class ProductController : Controller
    {

        private readonly Context _db;
        public readonly UserManager<AppUser> _userManager;

        public ProductController(Context db, UserManager<AppUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public IActionResult ProductList(int id)
        {
            var ownerName = _db.Restaurants.Where(x => x.RestaurantID == id).Select(x => x.Owner.UserName).FirstOrDefault();
            var currentUserName = User.Identity.Name;
            if (ownerName != currentUserName)
            {
                return RedirectToAction("Error", "Home");
            }
            var model = _db.Products.Include(x => x.Category).ThenInclude(x => x.Restaurant)
                .Where(x => x.Category.RestaurantID == id)
                .ToList();
            ViewBag.Restaurant = _db.Restaurants.Find(id);
            return View(model);
        }

        public IActionResult ProductCreate(int id)
        {
            var ownerName = _db.Restaurants.Where(x => x.RestaurantID == id).Select(x => x.Owner.UserName).FirstOrDefault();
            var currentUserName = User.Identity.Name;
            if (ownerName != currentUserName)
            {
                return RedirectToAction("Error", "Home");
            }
            ViewBag.id = id;
            ViewBag.categories = _db.Categories.Where(x=>x.RestaurantID == id).ToList();
            return View();
        }

        [HttpPost]
        public IActionResult ProductCreate(ProudctCreateDTO product, int id)
        {
            if (ModelState.IsValid)
            {
                Product newProduct = new Product()
                {
                    ProductName = product.ProductName,
                    ProductPrice = product.ProductPrice,
                    CategoryID = product.CategoryID,
                    ProductImage = product.ProductImage,
                    ProductStatus = true,
                    ProductCreateTime = DateTime.Now,
                    Weights = product.Weights
                };
                _db.Products.Add(newProduct);
                _db.SaveChanges();
                return RedirectToAction("ProductList", new { id = id });
            }
            return View();
        }

        public IActionResult ProductDelete(int id)
        {
            var prod = _db.Products.Include(x=>x.Category).Where(x=>x.ProductID == id).FirstOrDefault();
            var ownerName = _db.Restaurants.Where(x => x.RestaurantID == prod.Category.RestaurantID).Select(x => x.Owner.UserName).FirstOrDefault();
            var currentUserName = User.Identity.Name;
            if (ownerName != currentUserName)
            {
                return RedirectToAction("Error", "Home");
            }
            prod.ProductStatus = false;
            _db.SaveChanges();
            return RedirectToAction("ProductList", new { id = prod.Category.RestaurantID });
        }

        public IActionResult ProductEdit(int id)
        {
            var prod = _db.Products.Include(x => x.Category).Where(x => x.ProductID == id).FirstOrDefault();
            var ownerName = _db.Restaurants.Where(x => x.RestaurantID == prod.Category.RestaurantID).Select(x => x.Owner.UserName).FirstOrDefault();
            var currentUserName = User.Identity.Name;
            if (ownerName != currentUserName)
            {
                return RedirectToAction("Error", "Home");
            }
            ProductEditDTO dto = new ProductEditDTO()
            {
                ProductName = prod.ProductName,
                ProductPrice = prod.ProductPrice,
                ProductImage = prod.ProductImage,
                ProductStatus = prod.ProductStatus,
                Weights = prod.Weights
            };
            ViewBag.id = prod.ProductID;
            return View(dto);
        }

        [HttpPost]
        public IActionResult ProductEdit(ProductEditDTO product, int id)
        {
            var prod = _db.Products.Include(x => x.Category).Where(x => x.ProductID == id).FirstOrDefault();
            var ownerName = _db.Restaurants.Where(x => x.RestaurantID == prod.Category.RestaurantID).Select(x => x.Owner.UserName).FirstOrDefault();
            var currentUserName = User.Identity.Name;
            if (ownerName != currentUserName)
            {
                return RedirectToAction("Error", "Home");
            }
            prod.ProductName = product.ProductName;
            prod.ProductPrice = product.ProductPrice;
            prod.ProductImage = product.ProductImage;
            prod.ProductStatus = product.ProductStatus;
            prod.Weights = product.Weights;
            _db.SaveChanges();
            return RedirectToAction("ProductList", new { id = prod.Category.RestaurantID });
        }
    }
}
