using SeeSharpWebshop.Project.Core.Models;
using SeeSharpWebshop.Project.Core.Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeeSharpWebshop.Project.Core.Services.Implementations
{
    public class OrderService
    {
        private readonly OrderRepository orderRepository;

        public OrderService(OrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public bool Save(ReceiptModel model, List<CartModel> cart)
        {
            return orderRepository.Save(model, cart);
        }
    }
}
