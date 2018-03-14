using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SeeSharpWebshop.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            if (Request.Cookies["adminLogged"] != "true") return RedirectToAction("Login");
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
    }
}