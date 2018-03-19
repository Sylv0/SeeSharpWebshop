using System;
using System.Collections.Generic;
using System.Text;

namespace SeeSharpWebshop.Project.Core.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
    }
}
