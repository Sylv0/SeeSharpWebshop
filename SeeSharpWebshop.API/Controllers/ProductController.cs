using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SeeSharpWebshop.Project.Core.Models;
using SeeSharpWebshop.Project.Core.Repositories.Implementations;
using SeeSharpWebshop.Project.Core.Services.Implementations;

namespace SeeSharpWebshop.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Product")]
    public class ProductController : Controller
    {
        private readonly string connectionString;
        private readonly ProductService productService;

        public ProductController(IConfiguration config)
        {
            this.connectionString = config.GetConnectionString("ConnectionString");
            productService = new ProductService(new ProductRepository(this.connectionString));
        }
        
        [HttpGet]
        public IEnumerable<ProductModel> Get()
        {
            return productService.GetAll();
        }

        [HttpGet("{id}")]
        public IEnumerable<ProductModel> Get(int id)
        {
            var list = new List<ProductModel>();
            list.Add(productService.Get(id));
            return list;
        }
    }
}