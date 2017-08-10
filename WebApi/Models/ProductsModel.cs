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
}