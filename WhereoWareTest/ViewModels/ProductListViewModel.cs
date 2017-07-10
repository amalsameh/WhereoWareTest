using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WhereoWareTest.Models
{
    [Bind(Exclude = "SKU")]
    public class ProductListViewModel
    {
       
        public string SKU { get; set; }
        
        public string Name { get; set; }
       
        [DisplayName("Image")]
        public string ImagePath { get; set; }

    }
}