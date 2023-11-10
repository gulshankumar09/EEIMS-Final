using EEIMS.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using System.Data.Entity;
using EEIMS.Functionalities;

namespace EEIMS.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        // Operations for: Admin
        //
        // Show: Admin's Operations Dashboard
        public ActionResult AdminDashboard()
        {
            return View();
        }

        //
        // Show: /Admin list of all the users 
        public ActionResult AdminIndex()
        {
            return View();
        } 
        
        //
        // Show: /Manager list of all the users
        public ActionResult ManagerIndex()
        {
            return View();
        }   

        //
        // use this function to create roles( accesible only via url) no navigation provided.
        public async  Task<ActionResult> CreateRoles()
        {
            var roleStore = new RoleStore<IdentityRole>(new ApplicationDbContext());
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            await roleManager.CreateAsync(new IdentityRole("Admin"));
            await roleManager.CreateAsync(new IdentityRole("Manager"));
            await roleManager.CreateAsync(new IdentityRole("Employee"));
            
            return new EmptyResult();
        }

        //
        // GET: Return Json for {AdminIndex}  --> Admin list of all the users
        public ActionResult GetAdminUsers()
        {
            var context = new ApplicationDbContext();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var adminRole = roleManager.FindByName("Admin");
            if (adminRole == null)
            {
                return View("Error");
            }
            var adminUsers = userManager.Users.Where(u => u.Roles.Any(r => r.RoleId == adminRole.Id)).ToList();

            List<EmployeeRoleViewModel> adminRoleViewModels = new List<EmployeeRoleViewModel>();

            foreach (var adminUser in adminUsers)
            {
                var employees = context.Employees.FirstOrDefault(e => e.Id == adminUser.Id);
                var adminRoleViewModel = new EmployeeRoleViewModel
                {
                    Id = adminUser.Id,
                    EmployeeId = employees.EmployeeId,
                    FullName = employees.FirstName + " " + employees.LastName,
                    Department = employees.Department
                };
                adminRoleViewModels.Add(adminRoleViewModel);
            }
            return Json(adminRoleViewModels, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: Return Json for {ManagerIndex}  --> Manager list of all the users
        public ActionResult GetManagerUsers()
        {
            var context = new ApplicationDbContext();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var managerRole = roleManager.FindByName("Manager");
            if (managerRole == null)
            {
                return View("Error");
            }
            var adminUsers = userManager.Users.Where(u => u.Roles.Any(r => r.RoleId == managerRole.Id)).ToList();

            List<EmployeeRoleViewModel> managerRoleViewModels = new List<EmployeeRoleViewModel>();

            foreach (var adminUser in adminUsers)
            {
                var employees = context.Employees.FirstOrDefault(e => e.Id == adminUser.Id);
                var managerRoleViewModel = new EmployeeRoleViewModel
                {
                    Id = adminUser.Id,
                    EmployeeId = employees.EmployeeId,
                    FullName = employees.FirstName + " " + employees.LastName,
                    Department = employees.Department
                };
                managerRoleViewModels.Add(managerRoleViewModel);
            }

            return Json(managerRoleViewModels, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: AssingRoles to the Users --> Admin or Manager
        // (default: Employee) when Admin verifies the user then he/she will be assigned to the Employee role.
        [HttpGet]
        public  ActionResult AssignRoles()
        {
            populateRolesListItem();
            return View();
        }

        //
        // POST: AssingRoles to the Users --> Admin or Manager
        [HttpPost]
        public async Task<ActionResult> AssignRoles(AddRoleToUserViewModel model)
        {
            if (ModelState.IsValid)
            {   
                var context = new ApplicationDbContext();
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var user = await userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    if (!await userManager.IsInRoleAsync(user.Id, model.Role))
                    {
                        var result = await userManager.AddToRoleAsync(user.Id, model.Role);

                        if (result.Succeeded)
                        {
                            return RedirectToAction("AdminIndex", "Admin");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Failed to add the role to the user.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "User is already in the specified role.");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User with the specified email not found.");
                }
            }
            populateRolesListItem();

            return View(model);
        }


        // GET: Revoke all Admin & Manager roles from user
        public async Task<ActionResult> RevokeRoles(string id)
        {
            var context = new ApplicationDbContext();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var user = await userManager.FindByIdAsync(id);

            if (user != null)
            {
                var roles = await userManager.GetRolesAsync(user.Id);
                var result = await userManager.RemoveFromRolesAsync(user.Id, roles.ToArray());

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user.Id, "Employee");
                    return RedirectToAction("AdminIndex", "Admin");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed to remove roles from the user.");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "User not found.");
            }

            return RedirectToAction("AdminIndex", "Admin");
        }

        //
        // Method: polpulate the roles
        public void populateRolesListItem()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            var roles = roleManager.Roles.ToList();
            var roleItems = roles.Select(r => new SelectListItem
            {
                Value = r.Name,
                Text = r.Name
            }).ToList();

            ViewBag.Roles = roleItems;
        }


    }
}