﻿using Dapper;
using Microsoft.Data.Sqlite;
using SeeSharpWebshop.Project.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeeSharpWebshop.Project.Core.Repositories.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly string connectionString;

        public ProductRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<ProductModel> GetAll()
        {
            using (var connection = new SqliteConnection(this.connectionString))
            {
                return connection.Query<ProductModel>("SELECT * FROM products").ToList();
            }
        }

        public ProductModel Get(int id)
        {
            using (var connection = new SqliteConnection(this.connectionString))
            {
                return connection.QuerySingleOrDefault<ProductModel>("SELECT * FROM products WHERE id=@id", new { id });
            }
        }

        public bool Add(string Name, string Description, float Price)
        {
            using (var connection = new SqliteConnection(this.connectionString))
            {
                try
                {
                    connection.Execute("INSERT INTO products VALUES(null, @Name, @Description, @Price)", new { Name, Description, Price });
                    return true;
                }
                catch (Exception e) { return false; }
            }
        }
    }
}
