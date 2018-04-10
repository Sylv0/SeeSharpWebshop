using SeeSharpWebshop.Project.Core.Models;
using SeeSharpWebshop.Project.Core.Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SeeSharpWebshop.Project.Core.Services.Implementations
{
    public class CartService
    {
        private readonly CartRepository cartRepository;

        public CartService(CartRepository cartRepository)
        {
            this.cartRepository = cartRepository;
        }

        public List<CartModel> Get()
        {
            return cartRepository.Get();
        }

        public List<CartModel> Get(string guid)
        {
            return cartRepository.Get(guid);
        }

        public bool Add(string guid, int id)
        {
            return cartRepository.Add(guid, id);
        }

        public bool Update(string guid, int Id, int Amount)
        {
            return cartRepository.Update(guid, Id, Amount);
        }

        public int GetSize(string guid)
        {
            return cartRepository.Get(guid).Select(item => item.Amount).Sum();
        }

        public bool Remove(string guid, int id)
        {
            return cartRepository.Remove(guid, id);
        }

        public void Clear(string guid)
        {
            cartRepository.Clear(guid);
        }
    }
}
