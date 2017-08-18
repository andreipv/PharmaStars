using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class ProductsModel
    {
        public int Id;

        public String Name;

        public int? Quantity;

        public String Description;
        
        public double? Price;

        public ICollection<Category> Categories;

        public Manufacturer Manufacturer;
    }

    public class SimpleProductModel
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public double? Price { get; set; }
        public String ImgPath { get; set; }
        public ICollection<String> Categories { get; set; }
        public String Manufacturer { get; set; }
    }

    public class FullProductModel
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public double? Price { get; set; }
        public String ImgPath { get; set; }
        public int? Quantity { get; set; }
        public ICollection<int> Categories { get; set; }
        public int IDManufacturer { get; set; }
        public String Description { get; set; }
    }
}