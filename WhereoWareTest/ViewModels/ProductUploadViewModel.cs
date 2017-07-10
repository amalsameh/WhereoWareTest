using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WhereoWareTest.Models
{
    public class ProductUploadViewModel
    {
        [Required]
        [MaxLength(10), MinLength(10)]
        [RegularExpression(@"\d{7}[A-Z]-[A-Z]", ErrorMessage = "Incorrect Format")]
        public string SKU { get; set; }

        [Required(ErrorMessage = "Name Is Required")]
        public string Name { get; set; }
    
        [DisplayName("- OVERVIEW")]
        [MaxLength(200)]
        public string Overview { get; set; }

        [Required(ErrorMessage = "Product Image is Required")]
        public HttpPostedFileBase ProductImage { get; set; }
    }
}