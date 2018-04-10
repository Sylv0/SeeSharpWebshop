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
    class ProductServiceTests
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

        [TestCase(0)]
        [TestCase(-1)]
        public void Get_GivenIdLessThanOne_DoesNotCallRepository_ReturnsNull(int id)
        {
            // Arrange
            ProductModel expecterResult = null;

            A.CallTo(() => this.productRepository.Get(id)).Returns(expecterResult);

            // Act
            var result = productService.Get(id);

            // Assert
            Assert.That(result, Is.EqualTo(expecterResult));
            A.CallTo(() => this.productRepository.Get(A<int>.Ignored)).MustNotHaveHappened();
        }

        [Test]
        public void Get_GivenValidId_ReturnsExpectedProduct()
        {
            // Arrange
            const int Id = 1;
            var expectedProduct = new ProductModel { Id = Id };
            A.CallTo(() => this.productRepository.Get(Id)).Returns(expectedProduct);

            // Act
            var result = this.productService.Get(Id);

            // Assert
            Assert.That(result, Is.EqualTo(expectedProduct));
        }
    }
}
