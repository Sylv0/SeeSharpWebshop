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
                var query = "select products.id as ProductID, carts.Amount FROM products, carts WHERE carts.guid=@guid AND carts.ProductID=products.id";
                return connection.Query<CartModel>(query, new { guid }).ToList();
            }
        }

        public void Clear(string guid)
        {
            using (var connection = new SqliteConnection(this.connectionString))
            {
                var query = "DELETE FROM carts WHERE guid=@guid";
                connection.Execute(query, new { guid });
            }
        }
    }
}
