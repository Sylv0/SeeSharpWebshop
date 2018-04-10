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
    public class UpdateModel
    {
        public string guid { get; set; }
        public int Id { get; set; }
        public int Amount { get; set; }
    }

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
        public string Get(string guid)
        {
            return cartService.GetSize(guid).ToString();
        }

        [HttpPost("add")]
        public HttpStatusCode AddToCart([FromForm] UpdateModel model)
        {
            if (cartService.Add(model.guid, model.Id))
            {
                return HttpStatusCode.OK;
            }
            return HttpStatusCode.Conflict;
        }

        [HttpPost("update")]
        public HttpStatusCode UpdateCart([FromForm] UpdateModel model)
        {
            if (cartService.Update(model.guid, model.Id, model.Amount)) 
            {
                return HttpStatusCode.OK;
            }
            return HttpStatusCode.InternalServerError;
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
