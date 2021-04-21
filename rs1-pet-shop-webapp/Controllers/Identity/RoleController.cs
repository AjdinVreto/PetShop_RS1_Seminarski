using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using rs1_pet_shop_webapp.Areas.Identity.Data;
using Identity.Models;

namespace Identity.Controllers
{
    public class MyRoleManager
    {
        private RoleManager<IdentityRole> roleManager;
        private UserManager<ApplicationUser> userManager;

        public MyRoleManager(RoleManager<IdentityRole> roleMgr, UserManager<ApplicationUser> userMrg)
        {
            roleManager = roleMgr;
            userManager = userMrg;
        }

        // public ViewResult Index() => View(roleManager.Roles);

        // public IActionResult Create() => View();

        public async Task<Boolean> Create(string[] names)
        {
            foreach(var name in names)
            {
                await Create(name);
            }
            return true;
        }

        // [HttpPost]
        // public async Task<IActionResult> Create([Required] string name)
        public async Task<Boolean> Create([Required] string name)
        {
            if (name != null && await roleManager.RoleExistsAsync(name) == true)
            {
                IdentityResult result = await roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                    // return RedirectToAction("Index");
                    return true;
                else
                    Errors(result);
            }
            // return View(name);
            return false;
        }

        // [HttpPost]
        // public async Task<IActionResult> Delete(string id)
        public async Task<Boolean> Delete(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await roleManager.DeleteAsync(role);
                if (result.Succeeded)
                    // return RedirectToAction("Index");
                    return true;
                else
                    // Errors(result);
                    return false;
            }
            // else
            //ModelState.AddModelError("", "No role found");
            //return View("Index", roleManager.Roles);
            return false;
        }

        //public async Task<IActionResult> Update(string id)
        public async Task<RoleEdit> Update(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            List<ApplicationUser> members = new List<ApplicationUser>();
            List<ApplicationUser> nonMembers = new List<ApplicationUser>();
            foreach (ApplicationUser user in userManager.Users)
            {
                var list = await userManager.IsInRoleAsync(user, role.Name) ? members : nonMembers;
                list.Add(user);
            }
            // return View(new RoleEdit
            return new RoleEdit
            {
                Role = role,
                Members = members,
                NonMembers = nonMembers
                // });
            };
        }

        // [HttpPost]
        // public async Task<IActionResult> Update(RoleModification model)
        public async Task<RoleEdit> Update(RoleModification model)
        {
            IdentityResult result;
            if (model.RoleName != null && model.RoleName.Length > 0)
            {
                foreach (string userId in model.AddIds ?? new string[] { })
                {
                    ApplicationUser user = await userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await userManager.AddToRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                            Errors(result);
                    }
                }
                foreach (string userId in model.DeleteIds ?? new string[] { })
                {
                    ApplicationUser user = await userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await userManager.RemoveFromRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                            Errors(result);
                    }
                }
            }

            if (model.RoleName != null)
                // return RedirectToAction(nameof(Index));
                return null;
            else
                return await Update(model.RoleId);
            // return false;
        }

        public List<IdentityRole> GetAll()
        {
            var roles = roleManager.Roles.ToList();
            return roles;
        }

        private void Errors(IdentityResult result)
        {
            // foreach (IdentityError error in result.Errors)
            // ModelState.AddModelError("", error.Description);
        }
    }
}