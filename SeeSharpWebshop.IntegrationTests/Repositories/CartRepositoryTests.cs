using NUnit.Framework;
using SeeSharpWebshop.Project.Core.Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeeSharpWebshop.IntegrationTests.Repositories
{
    public class CartRepositoryTests
    {
        private CartRepository cartRepository;

        [SetUp]
        public void SetUp()
        {
            this.cartRepository = new CartRepository("Data Source=../../SeeSharpWebshop/App_Data/shop.db");
        }

        [Test]
        public void Get_GivenGuid_ReturnsExpectedCart()
        {
            const string guid = "2cf43b90-46f9-4b0f-9d85-e7b4f4b688d1";

            var result = cartRepository.Get(guid);
        }
    }
}
