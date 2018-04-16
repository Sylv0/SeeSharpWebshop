using System;
using System.Collections.Generic;
using System.Text;

namespace SeeSharpWebshop.Project.Core.Models
{
    public class OrderModel
    {
        public ReceiptModel Receipt { get; set; }
        public List<ProductModel> Products { get; set; }
    }
}
