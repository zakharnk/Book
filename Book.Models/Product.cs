using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Book.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string ISBN { get; set;}

        [Required]
        [Display(Name = "List Price")]
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        //[Range(1.0, 1000.0, ConvertValueInInvariantCulture = true, ErrorMessage = "Price for 50+ books must be between 1-1000")]
        public double ListPrice { get; set; }

        [Required]
        [Display(Name = "Price for 1-50")]
        //[Range(1.0, 1000.0, ConvertValueInInvariantCulture = true, ErrorMessage = "Price for 1-50 books must be between 1-1000")]
        public double Price { get; set; }

        [Required]
        [Display(Name = "Price for 50+ books")]
       // [Range(1.0, 1000.0, ConvertValueInInvariantCulture = true, ErrorMessage = "Price for 50+ books must be between 1-1000")]
        public double PriceFor50 { get; set; }

        [Required]
        [Display(Name = "Price for 100+ books")]
       // [Range(1.0, 1000.0, ConvertValueInInvariantCulture = true ,ErrorMessage = "Price for 100+ books must be between 1-1000")]
        public double PriceFor100 { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }
        [ValidateNever]
        public string ImageUrl { get; set; }
    }
}
