using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SeeSharpWebshop.Project.Core.Repositories.Implementations;
using SeeSharpWebshop.Project.Core.Services.Implementations;
using Newtonsoft.Json;
using SeeSharpWebshop.Project.Core.Models;
using System.Net;

namespace SeeSharpWebshop.API.Controllers
{
    [Route("api/[controller]")]
    public class CartController : Controller
    {
        private readonly string connectionString;
        private readonly CartService cartService;

        public CartController(IConfiguration config)
        {
            this.connectionString = config.GetConnectionString("ConnectionString");
            this.cartService = new CartService(new CartRepository(this.connectionString));
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<CartModel> Get()
        {
            return cartService.Get();
        }

        // GET api/values/5
        [HttpGet("{guid}")]
        public IEnumerable<CartModel> Get(string guid)
        {
            List<CartModel> list = new List<CartModel>();
            
            return cartService.Get(guid);
        }

        // POST api/values
        [HttpPost("{value}")]
        public string Post([FromBody]ProductModel value)
        {
            return $"{value.Name}: {value.Price}\n{value.Description} | id:{value.Id}";
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
