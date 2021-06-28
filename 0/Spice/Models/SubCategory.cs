using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Spice.Models
{
    public class SubCategory
    {
        public int Id { get; set; }

        [Required]
        [Display(Name="Sub Category Name")]
        [MaxLength(250)]
        public string Name { get; set; }

        [Display(Name="Category")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category category { get; set; }
    }
}
