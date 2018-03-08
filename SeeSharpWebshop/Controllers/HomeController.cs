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

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
