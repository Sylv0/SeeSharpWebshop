using NUnit.Framework;
using SeeSharpWebshop.Project.Core.Models;
using SeeSharpWebshop.Project.Core.Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeeSharpWebshop.UnitTests.Repositories
{
    class ProductRepositoryTests
    {
        private ProductRepository productRepository;

        [SetUp]
        private void SetUp()
        {
            productRepository = new ProductRepository("Data Source=../../SeeSharpWebshop/App_Data/shop.db");
        }

        [Test]
        public void GetAll_ReturnsExpectedProducts()
        {
            // Arrange
            var expectedResult = new List<ProductModel>();

            // Act
            var result = productRepository.GetAll();

            // Assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}
