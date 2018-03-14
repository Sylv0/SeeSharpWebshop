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

namespace SeeSharpWebshop.Controllers
{
    public class HomeController : Controller
    {
        public static Dictionary<ProductViewModel, int> cart = new Dictionary<ProductViewModel, int>();

        private readonly string connectionString;

        List<ProductViewModel> products = new List<ProductViewModel>();

        public HomeController(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("ConnectionString");
            using (var connection = new SqliteConnection(this.connectionString))
            {
                this.products = connection.Query<ProductViewModel>("SELECT * FROM products").ToList();
            }
        }

        public IActionResult Index()
        {

            return View(products);
        }

        public IActionResult Cart()
        {
            return View(cart);
        }

        public IActionResult About()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddItemToCart(int id)
        {
            if(Request.Cookies["guid"] == null)
            {
                Response.Cookies.Append("guid", Guid.NewGuid().ToString());
            }
            List<ProductViewModel> product = products.Where(i => i.id == id).ToList();
            if (product.Any())
            {
                cart.Add(product[0], 1);
            }
            return RedirectToAction("Index");
            //return View(id);
        }

        public IActionResult ClearCart()
        {
            cart = new Dictionary<ProductViewModel, int>();
            return RedirectToAction("Cart");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
