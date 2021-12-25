using ISE309.Odev.BLL.Data;
using ISE309.Odev.Core.DbEntities;
using ISE309.Odev.WebUI.Areas.Admin.Models.UserDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISE309.Odev.WebUI.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        public readonly UserManager<AppUser> _userManager;
        public readonly SignInManager<AppUser> _signInManager;
        public readonly RoleManager<AppRole> _roleManager;
        public readonly Context _db;

        public UserController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager
            , RoleManager<AppRole> roleManager, Context db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _db = db;
        }

        public IActionResult AllUsers()
        {
            return View(_userManager.Users.ToList());
        }
        
        public async Task<IActionResult> Edit(int id)
        {
            var user = _db.Users.Find(id);
            UserEditDTO dto = new UserEditDTO
            {
                UserName = user.UserName,
                Name = user.Name,
                SurName = user.SurName,
                Email = user.Email
            };
            var userroles = await _userManager.GetRolesAsync(user);
            ViewBag.roles = userroles;
            var allroles = _roleManager.Roles.Select(x=>x.Name).ToList();
            List<string> otherroles = new List<string>();
            foreach (var item in allroles)
            {
                if (!userroles.Contains(item))
                {
                    otherroles.Add(item);
                }
            }
            ViewBag.otherroles = otherroles;
            ViewBag.id = user.Id;
            return View(dto);
        }

        [HttpPost]
        public IActionResult Edit(UserEditDTO dto, int id)
        {
            if (ModelState.IsValid)
            {
                var user = _db.Users.Find(id);
                user.UserName = dto.UserName;
                user.Name = dto.Name;
                user.SurName = dto.SurName;
                user.Email = dto.Email;
                _db.SaveChanges();
                return RedirectToAction("AllUsers");
            }
            return View();
        }
        public async Task<IActionResult> DeleteUserRole(string role,int id)
        {
            var user = _db.Users.Find(id);
            IdentityResult result = await _userManager.RemoveFromRoleAsync(user, role);
            if (result.Succeeded)
            {
                return RedirectToAction("Edit", new { id = id });
            }
            return RedirectToAction("Edit", new { id = id });
        }
        
        public async Task<IActionResult> AddUserRole(string role,int id)
        {
            var user = _db.Users.Find(id);
            IdentityResult result = await _userManager.AddToRoleAsync(user , role);
            if (result.Succeeded)
            {
                return RedirectToAction("Edit", new { id = id });
            }
            return RedirectToAction("Edit", new { id = id });
        }

        public IActionResult AllRoles()
        {
            ViewBag.roles = _roleManager.Roles.ToList();
            return View();
        }

        //Rol oluşturma
        [HttpPost]
        public async Task<IActionResult> CreateRole(string role)
        {
            if (role=="" || role == null)
            {
                return Ok("Boş Rol Giremezsin");
            }
            var role2 = _roleManager.Roles.Where(x => x.Name == role).FirstOrDefault();
            if (_roleManager.Roles.Contains(role2))
            {
                return Ok("Böyle bir rol zaten var");
            }
            IdentityResult result = await _roleManager.CreateAsync(new AppRole { Name = role });
            if (result.Succeeded)
            {
                return RedirectToAction("AllRoles", "User");
            }
            return RedirectToAction("AllRoles");
        }

        public async Task<IActionResult> DeleteRoleAsync(string rolename)
        {
            var role = _roleManager.Roles.Where(x=>x.Name == rolename).FirstOrDefault();
            IdentityResult result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction("AllRoles");
            }
            return RedirectToAction("AllRoles");
        }

    }
}
