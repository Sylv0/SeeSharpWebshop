using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using SeeSharpWebshop.Models;
using SeeSharpWebshop.Project.Core.Models;
using SeeSharpWebshop.Project.Core.Repositories.Implementations;
using SeeSharpWebshop.Project.Core.Services.Implementations;

namespace SeeSharpWebshop.Controllers
{
    public class HomeController : Controller
    {
        public readonly string connectionString;

        List<ProductModel> products = new List<ProductModel>();

        private ProductService productService;
        private CartService cartService;
        private OrderService orderService;

        public HomeController(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("ConnectionString");

            productService = new ProductService(new ProductRepository(this.connectionString));
            cartService = new CartService(new CartRepository(this.connectionString));
            orderService = new OrderService(new OrderRepository(this.connectionString));

            this.products = productService.GetAll();
        }

        public IActionResult Index()
        {
            if (Request.Cookies["guid"] == null)
            {
                Response.Cookies.Append("guid", Guid.NewGuid().ToString());
            }
            return View(products);
        }

        public IActionResult Cart()
        {
            return View(cartService.Get(Request.Cookies["guid"]));
        }

        public IActionResult About()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddItemToCart(int id)
        {
            cartService.Add(Request.Cookies["guid"], id);
            return RedirectToAction("Index");
            //return View(id);
        }

        public IActionResult ClearCart()
        {
            cartService.Clear(Request.Cookies["guid"]);
            return RedirectToAction("Cart");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult CompletePurchase()
        {
            return View(cartService.Get(Request.Cookies["guid"]));
        }

        [HttpPost]
        public IActionResult CompletePurchase(ReceiptModel model)
        {
            orderService.Save(model, cartService.Get(Request.Cookies["guid"]));
            return View("Receipt", model);
        }
    }
}
