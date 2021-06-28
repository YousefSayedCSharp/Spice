using Spice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spice.ViewModels
{
    public class MenuItemViewModel
    {
        public MenuItem menuItem { get; set; }
        public IEnumerable<Category> categoriesList { get; set; }
        public IEnumerable<SubCategory> subCategoriesList { get; set; }
    }
}
