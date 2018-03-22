using System;
using System.Collections.Generic;
using System.Text;

namespace SeeSharpWebshop.Project.Core.Models
{
    public class ReceiptModel
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Zipcode { get; set; }
        public string City { get; set; }
    }
}
