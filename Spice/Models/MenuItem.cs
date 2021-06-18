using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Spice.Models
{
    public class MenuItem
    {
        public int Id { get; set; }

        [Required,Display(Name="Name"),MaxLength(250)]
        public string Name { get; set; }

        [Required, Display(Name = "Description"),MaxLength(2000)]
        public string Description { get; set; }

        [Required, Display(Name = "Price")]
        public double Price { get; set; }

        public string Image { get; set; }

        [Required]
        public string Spicyness { get; set; }
        public enum ESpicy { NA = 0, NotSpicy = 1, Spicy = 2, VerySpicy = 3 }

        [Display(Name="Category")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category category { get; set; }

        [Display(Name = "Sub Category")]
        public int SubCategoryId { get; set; }
        [ForeignKey("SubCategoryId")]
        public SubCategory SubCategory { get; set; }
    }
}
