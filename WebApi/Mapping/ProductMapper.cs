using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;
using WebApi.Models;
using DAL.Repositories;

namespace WebApi.Mapping
{
    public static class ProductMapper
    {
        
        public static Product ModelToEntity(ProductsModel productModel)
        {
            UnitOfWork uow = new UnitOfWork();
            Product product = new Product()
            {
                ID = productModel.Id,
                Name = productModel.Name,
                Manufacturer = productModel.Manufacturer,
                Quantity = productModel.Quantity,
                Price = productModel.Price,
                Description = productModel.Description,
                Categories = productModel.Categories
            };
            return product;
        }

        public static ProductsModel EntityToModel(Product product)
        {
            ProductsModel model = new ProductsModel()
            {
                Id = product.ID,
                Name = product.Name,
                Manufacturer = product.Manufacturer,
                Price = product.Price,
                Quantity = product.Quantity,
                Categories = product.Categories,
                Description = product.Description
            };
            return model;
        }

        public static SimpleProductModel EntityToSimpleModel(Product product)
        {
            SimpleProductModel model = new SimpleProductModel()
            {
                Id = product.ID,
                Name = product.Name,
                Manufacturer = product.Manufacturer.Name,
                Price = product.Price,
                ImgPath = product.ImgPath
            };
            model.Categories = new List<string>();
            foreach(var cat in product.Categories)
            {
                model.Categories.Add(cat.Name);
            }
            return model;
        }
    }
}