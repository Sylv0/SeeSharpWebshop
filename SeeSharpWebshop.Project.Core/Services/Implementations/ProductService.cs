﻿using SeeSharpWebshop.Project.Core.Models;
using SeeSharpWebshop.Project.Core.Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeeSharpWebshop.Project.Core.Services.Implementations
{
    public class ProductService
    {
        private readonly ProductRepository productRepository;

        public ProductService(ProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public List<ProductModel> GetAll()
        {
            return productRepository.GetAll();
        }

        public ProductModel Get(int id)
        {
            return productRepository.Get(id);
        }
    }
}