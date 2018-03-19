using Dapper;
using Microsoft.Data.Sqlite;
using SeeSharpWebshop.Project.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeeSharpWebshop.Project.Core.Repositories.Implementations
{
    public class CartRepository
    {
        private readonly string connectionString;

        public CartRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<CartModel> Get(string guid)
        {
            using (var connection = new SqliteConnection(this.connectionString))
            {
                return connection.Query<CartModel>("select products.id, carts.productamount FROM products, carts WHERE carts.guid=@guid AND carts.productid=products.id",
                    new { guid }).ToList();
            }
        }
    }
}
