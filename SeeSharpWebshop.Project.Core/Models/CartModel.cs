using System;
using System.Collections.Generic;
using System.Text;

namespace SeeSharpWebshop.Project.Core.Models
{
    public class CartModel
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int Amount { get; set; }
    }
}
