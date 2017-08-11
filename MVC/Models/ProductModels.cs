using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class SimpleProductModel
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public double? Price { get; set; }
        public ICollection<String> Categories { get; set; }
        public String Manufacturer { get; set; }
    }
}