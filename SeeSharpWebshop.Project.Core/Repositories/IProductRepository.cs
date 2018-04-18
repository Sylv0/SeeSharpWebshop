using SeeSharpWebshop.Project.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeeSharpWebshop.Project.Core.Repositories
{
    public interface IProductRepository
    {
        List<ProductModel> GetAll();

        ProductModel Get(int id);

        bool Add(string Name, string Description, float Price);
    }
}
