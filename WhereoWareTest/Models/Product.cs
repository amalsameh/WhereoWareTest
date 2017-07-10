using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WhereoWareTest.Models
{
    public class Product
    {
        //I supposed that SKU it unique for each item and its pattern have to be same like sample you provided
        [Key]
        [Required]
        [MaxLength(10),MinLength(10)]
        [RegularExpression(@"\d{7}[A-Z]-[A-Z]", ErrorMessage ="Incorrect Format")]
        public string SKU { get; set; }
        [Required(ErrorMessage = "Name Is Required")]
        [DefaultValue("Product Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Image Is Required")]
        [DisplayName("UPLOAD IMAGE")]
        public string ImagePath { get; set; }
        [DefaultValue("Product Overview")]
        public string Overview { get; set; }

    }
}