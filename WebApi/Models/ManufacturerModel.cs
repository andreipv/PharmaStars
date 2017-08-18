using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class ManufacturerModel
    {  
        public int ID { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
    }
}