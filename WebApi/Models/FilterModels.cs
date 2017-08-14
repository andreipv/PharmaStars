using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class FilterModel
    {
        public ICollection<String> ManufacturerFilters { get; set; }
        public ICollection<String> CategoryFilters { get; set; }
    }
}