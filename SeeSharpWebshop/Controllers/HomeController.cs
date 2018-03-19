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
        public static Dictionary<ProductModel, int> cart = new Dictionary<ProductModel, int>();

        public readonly string connectionString;

        List<ProductModel> products = new List<ProductModel>();

        private ProductService productService;
        private CartService cartService;

        public HomeController(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("ConnectionString");

            productService = new ProductService(new ProductRepository(this.connectionString));
            cartService = new CartService(new CartRepository(this.connectionString));

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
            using (var connection = new SqliteConnection(this.connectionString))
            {
                var testResult = connection.QuerySingleOrDefault("SELECT Amount FROM carts WHERE guid=@guid AND ProductID=@prodid",
                    new { guid = Request.Cookies["guid"], prodid = id });
                if (testResult != null)
                {
                    connection.Execute("UPDATE carts SET Amount=Amount+1 WHERE guid=@guid AND ProductID=@prodid", new { guid = Request.Cookies["guid"], prodid = id });
                }
                else
                {
                    connection.Execute("INSERT INTO carts(guid, ProductID, Amount) VALUES(@guid, @prodid, @amount)",
                            new { guid = Request.Cookies["guid"], prodid = id, amount = 1 });
                }
            }
            List<ProductModel> product = products.Where(i => i.Id == id).ToList();
            if (product.Any())
            {
                cart.Add(product[0], 1);
            }
            return RedirectToAction("Index");
            //return View(id);
        }

        public IActionResult ClearCart()
        {
            cart = new Dictionary<ProductModel, int>();
            cartService.Clear(Request.Cookies["guid"]);
            return RedirectToAction("Cart");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
