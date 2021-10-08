using MVC_Product_Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Product_Manager.ViewModel
{
    public class CategoryViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public string Search { get; set; }
        public int PageSize { get; set; }
        public int Page { get; set; }
        public Pagination Pagination { get; set; }
    }
}
