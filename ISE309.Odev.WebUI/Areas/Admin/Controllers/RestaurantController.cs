using ISE309.Odev.BLL.Data;
using ISE309.Odev.Core.DbEntities;
using ISE309.Odev.WebUI.Areas.Admin.Models.RestaurantDTO;
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
    public class RestaurantController : Controller
    {
        private readonly Context _db;

        public RestaurantController(Context db)
        {
            _db = db;
        }

        public IActionResult RestaurantList()
        {
            IEnumerable<Restaurant> model = _db.Restaurants;
            return View(model);
        }

        public IActionResult RestaurantCreate()
        {
            ViewBag.User = _db.Users.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult RestaurantCreate(RestaurantCreateDTO dto)
        {
            if (ModelState.IsValid)
            {
                if (dto.RestaurantOwner > 0)
                {
                    Restaurant restaurant = new Restaurant
                    {
                        RestaurantName = dto.RestaurantName,
                        RestaurantAddress = dto.RestaurantAddress,
                        RestaurantMinDelivery = dto.RestaurantMinDelivery,
                        RestaurantDeliveryTime = dto.RestaurantDeliveryTime,
                        RestaurantRating = dto.RestaurantRating,
                        RestaurantLogoImage = dto.RestaurantLogoImage,
                        RestaurantStatus = dto.RestaurantStatus
                    };
                    _db.Restaurants.Add(restaurant);
                    _db.SaveChanges();
                    return RedirectToAction("RestaurantList");
                }
            }
            return View();
        }

        public IActionResult RestaurantEdit(int id)
        {
            var rest = _db.Restaurants.Include(x=>x.Owner).Where(x=>x.RestaurantID == id).FirstOrDefault();
            RestaurantEditDTO dto = new RestaurantEditDTO
            {
                RestaurantName = rest.RestaurantName,
                RestaurantOwner = rest.Owner.Id,
                RestaurantAddress = rest.RestaurantAddress,
                RestaurantWorkingHours = rest.RestaurantWorkingHours,
                RestaurantMinDelivery = rest.RestaurantMinDelivery,
                RestaurantDeliveryTime = rest.RestaurantDeliveryTime,
                RestaurantRating = rest.RestaurantRating,
                RestaurantLogoImage = rest.RestaurantLogoImage,
                RestaurantStatus = rest.RestaurantStatus
            };
            ViewBag.id = rest.RestaurantID;
            ViewBag.User = _db.Users.ToList();
            return View(dto);
        }

        [HttpPost]
        public IActionResult RestaurantEdit(RestaurantEditDTO dto,int id)
        {
            if (ModelState.IsValid)
            {
                var rest = _db.Restaurants.Include(x => x.Owner)
                    .Where(x => x.RestaurantID == id).FirstOrDefault();
                rest.RestaurantName = dto.RestaurantName;
                rest.Owner.Id = dto.RestaurantOwner;
                rest.RestaurantAddress = dto.RestaurantAddress;
                rest.RestaurantWorkingHours = dto.RestaurantWorkingHours;
                rest.RestaurantMinDelivery = dto.RestaurantMinDelivery;
                rest.RestaurantDeliveryTime = dto.RestaurantDeliveryTime;
                rest.RestaurantRating = dto.RestaurantRating;
                rest.RestaurantLogoImage = dto.RestaurantLogoImage;
                rest.RestaurantStatus = dto.RestaurantStatus;
                _db.SaveChanges();
                return RedirectToAction("RestaurantList");
            }
            return View();
        }
        
        public IActionResult RestaurantStatus(int id)
        {
            var rest = _db.Restaurants.Find(id);
            rest.RestaurantStatus = false;
            _db.SaveChanges();
            return RedirectToAction("RestaurantList");
        }

    }
}
