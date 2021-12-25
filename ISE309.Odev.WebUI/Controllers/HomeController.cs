using ISE309.Odev.BLL.Data;
using ISE309.Odev.Core.DbEntities;
using ISE309.Odev.WebUI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ISE309.Odev.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHtmlLocalizer<HomeController> _localizer;
        private readonly ILogger<HomeController> _logger;
        private readonly Context _db;

        public HomeController(ILogger<HomeController> logger,Context db, IHtmlLocalizer<HomeController> localizer)
        {
            _logger = logger;
            _db = db;
            _localizer = localizer;
        }

        public IActionResult Index()
        {
            IEnumerable<Restaurant> model = _db.Restaurants.Where(x=>x.RestaurantStatus == true).OrderByDescending(x=>x.RestaurantRating);
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult CultureManagement(string culture, string returnUrl)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.Now.AddDays(30) });
            return LocalRedirect(returnUrl);
        }
    }
}
