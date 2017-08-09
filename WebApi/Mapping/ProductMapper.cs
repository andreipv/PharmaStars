using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;
using WebApi.Models;

namespace WebApi.Mapping
{
    public class ProductMapper
    {
        public Product ModelToEntity(ProductsModel productModel)
        {
            Product product = new Product();

            product.ID = productModel.Id;
            product.Name = productModel.Name;
            product.Manufacturer = productModel.Manufacturer;
            product.Quantity = productModel.Quantity;
            product.Price = productModel.Price;
            product.Description = productModel.Description;
            product.Categories = productModel.Categories;

            return product;
        }

        public ProductsModel EntityToModel(Product product)
        {
            ProductsModel model = new ProductsModel();

            model.Id = product.ID;
            model.Name = product.Name;
            model.Manufacturer = product.Manufacturer;
            model.Price = product.Price;
            model.Quantity = product.Quantity;
            model.Categories = product.Categories;
            model.Description = product.Description;

            return model;

        }
    }
}