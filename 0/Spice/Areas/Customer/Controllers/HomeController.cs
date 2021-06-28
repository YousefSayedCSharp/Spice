using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spice.Data;
using Spice.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spice.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            IndexViewModel indexVM = new IndexViewModel
            {
                Coupons = await _context.Coupons.Where(m => m.IsActive).ToListAsync(),
                Categories = await _context.Categories.ToListAsync(),
                MenuItems = await _context.MenuItems.Include(m => m.category).Include(m => m.SubCategory).ToListAsync()
            };

            return View(indexVM);
        }
    }
}
