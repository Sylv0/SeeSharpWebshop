using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SeeSharpWebshop.Controllers
{

    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            if (Request.Cookies["adminLogged"] != "true") return RedirectToAction("Login");
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (!string.IsNullOrEmpty(Request.Cookies["adminLogged"]))
            {
                return RedirectToAction("Logout");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            if (model.Email == "a@a.a" && model.Password == "admin")
            {
                Response.Cookies.Append("adminLogged", "true");
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Logout()
        {
            Response.Cookies.Delete("adminLogged");
            return RedirectToAction("Login");
        }
    }
}