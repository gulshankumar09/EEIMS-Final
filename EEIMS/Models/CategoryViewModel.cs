using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;

namespace EEIMS.Models
{
    public class AddCategoryViewModel
    {
        
        public int CategoryId { get; set; }

        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }
    }
}