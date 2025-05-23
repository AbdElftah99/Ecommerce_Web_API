using AdminDashboard.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminDashboard.Controllers
{
    public class RoleController(RoleManager<IdentityRole> _roleManager) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }

        public async Task<IActionResult> Create(RoleFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                var roleExist = await _roleManager.RoleExistsAsync(model.Name);

                if (!roleExist)
                {
                    await _roleManager.CreateAsync(new IdentityRole(model.Name.Trim()));
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("Name", "Role exist already");
                    return View("Index", await _roleManager.Roles.ToListAsync());
                }
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string id)
        {
            var roleExist = await _roleManager.FindByIdAsync(id);
            await _roleManager.DeleteAsync(roleExist);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            var mappedRole = new RoleViewModel()
            {
                Name = role.Name
            };

            return View(mappedRole);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id , RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var roleExist = await _roleManager.FindByIdAsync(id);

                if (roleExist != null)
                {
                    var role = await _roleManager.FindByIdAsync(id);
                    role.Name = model.Name;

                    await _roleManager.UpdateAsync(role);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("Name", "Role doesn't Exist");
                    return View("Index", await _roleManager.Roles.ToListAsync());
                }
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
