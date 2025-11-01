using DataAccessLayer.Entities;
using DataAccessLayer.Repository;
using HPRSApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace HPRSApp.Controllers
{
    public class AdminController : Controller
    {
        IHPRS repo;
        public AdminController(IHPRS repo)
        {
            this.repo = repo;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(AdminViewModel a)
        {
            if (ModelState.IsValid)
            {
                var user = new Admin
                {
                    AdminName = a.AdminName,
                    EmailId = a.EmailId,
                    Password = a.Password
                };
                var result = repo.RegisterUser(user);
                if (result)
                {
                    return RedirectToAction("Login");
                }
                return View(a);
            }
            ViewBag.msg = "Registration Unseccessful!";
            return View(a);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(AdminViewModel a)
        {
            var admin = repo.ValidateUser(a.EmailId, a.Password);

            if (admin)
            {
                HttpContext.Session.SetString("EmailId", a.EmailId);
                return RedirectToAction("ViewAllPatientRecords", "Patient");
            }
            ViewBag.msg = "Credentials are incorrect!";
            return View();
        }
        [HttpGet]
        public IActionResult LogOut()
        {
            var admin = HttpContext.Session.GetString("EmailId");

            if (admin != null)
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Login");
            }

            return View();
        }
        [HttpGet]
        public IActionResult UpdateUser()
        {
            var admin = HttpContext.Session.GetString("EmailId");
            if (admin != null)
            {
                var currentAdmin = repo.GetAdminByEmailId(admin);

                var avm = new AdminViewModel
                {
                    AdminId = currentAdmin.AdminId,
                    EmailId = currentAdmin.EmailId,
                    Password = currentAdmin.Password
                };
                return View(avm);
            }
            return NotFound();

        }
        [HttpPost]

        public IActionResult UpdateUser(AdminViewModel a)
        {
            if (ModelState.IsValid)
            {
                var adminEmail = HttpContext.Session.GetString("EmailId");
                var admin = new Admin 
                {
                    AdminId = a.AdminId,
                    AdminName = a.AdminName,
                    EmailId = adminEmail,
                    Password = a.Password
                };
                
                var result = repo.UpdateAdmin(admin);
                
                if (result)
                {
                    return RedirectToAction("ViewAllPatientRecords", "Patient");

                }
            }
            return View(a);
        }
    }
}
