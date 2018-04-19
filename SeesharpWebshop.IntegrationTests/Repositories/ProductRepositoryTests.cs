using NUnit.Framework;
using SeeSharpWebshop.Project.Core.Models;
using SeeSharpWebshop.Project.Core.Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace SeesharpWebshop.IntegrationTests.Repositories
{
    class ProductRepositoryTests
    {
        private ProductRepository productRepository;

        [SetUp]
        public void SetUp()
        {
            this.productRepository = new ProductRepository("Data Source=C:/Users/gtc12/source/repos/SeeSharpWebshop/SeesharpWebshop/App_Data/shop.db");
        }

        [Test]
        public void Get_GivenId_ReturnsExpectedItem()
        {
            // Arrange
            const int ExpectedId = 1;
            const string ExpectedName = "Kebab";
            const float ExpectedPrice = 24.99f;

            // Act
            var result = this.productRepository.Get(ExpectedId);

            // Assert
            Assert.That(result.Id, Is.EqualTo(ExpectedId));
            Assert.That(result.Name, Is.EqualTo(ExpectedName));
            Assert.That(result.Price, Is.EqualTo(ExpectedPrice));
        }
    }
}
