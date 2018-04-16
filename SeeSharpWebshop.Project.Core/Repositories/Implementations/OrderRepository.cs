using Dapper;
using Microsoft.Data.Sqlite;
using SeeSharpWebshop.Project.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeeSharpWebshop.Project.Core.Repositories.Implementations
{
    public class OrderRepository
    {
        private readonly string connectionString;

        public OrderRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool Save(ReceiptModel model)
        {
            using (var connection = new SqliteConnection(this.connectionString))
            {
                try
                {
                    connection.Execute("insert into orders values(date() || \"/\" || (select (count(Id)+1) from orders where Id like date() || \"%\"), @Name, @Address, @Zipcode, @City)", new { model.Name, model.Address, model.Zipcode, model.City });
                    return true;
                } catch (Exception e) { return false; }
            }
        }
    }
}
