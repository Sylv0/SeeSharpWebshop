using System;
using System.Collections.Generic;
using System.Text;
using NUnit;
using NUnit.Framework;
using SeeSharpWebshop.Project.Core.Repositories;
using SeeSharpWebshop.Project.Core.Services.Implementations;
using FakeItEasy;
using SeeSharpWebshop.Project.Core.Models;

namespace SeeSharpWebshop.UnitTest.Services
{
    class ProductServiceTest
    {
        private ProductService productService;
        private IProductRepository productRepository;

        [SetUp]
        public void SetUp()
        {
            this.productRepository = A.Fake<IProductRepository>();
            this.productService = new ProductService(this.productRepository);
        }

        [Test]
        public void GetAll_ReturnsExpectedProducts()
        {
            // Arrange
            var expectedProducts = new List<ProductModel>
            {
                new ProductModel { Id = 1 }
            };

            A.CallTo(() => this.productRepository.GetAll()).Returns(expectedProducts);

            // Act
            var result = this.productService.GetAll();

            // Assert
            Assert.That(result, Is.EqualTo(expectedProducts));
        }

        [Test]
        public void Get_GivenId_ReturnsExpectedProduct()
        {
            // Arrange
            var id = 1;
            var expectedProduct = new ProductModel { Id = 1, Name = "Kebab", Description = "Mycke' bra!", Price = 9.99f };
            A.CallTo(() => this.productRepository.Get(id)).Returns(expectedProduct);

            // Act
            var result = this.productService.Get(id);

            // Assert
            Assert.That(result, Is.EqualTo(expectedProduct));
        }
    }
}
