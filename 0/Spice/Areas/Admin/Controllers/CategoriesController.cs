using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spice.Data;
using Spice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spice.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["CreateEditButtons"] = "CategoryForm";

            var categories = await _context.Categories.ToListAsync();

            return View(categories);
        }

        [HttpGet]
        public async Task<IActionResult> CategoryForm(int? id)
        {
            if (id == null)
                return BadRequest();

            if(id==0)
                return View("CategoryForm", new Category { });

            var cate = await _context.Categories.FindAsync(id);
            if (cate == null)
                return NotFound();

            return View("CategoryForm", cate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CategoryForm(Category model)
        {
            if (!ModelState.IsValid)
                return View("CategoryForm", model);

            if (model.Id > 0)
            {
                var cate = await _context.Categories.FindAsync(model.Id);
                if (cate == null)
                    return NotFound();

                if(model.Name==cate.Name)
                {
                    ModelState.AddModelError("Name", "Please change category name to Edit this item!");
                    return View("CategoryForm",model);
                }

                cate.Name = model.Name;
            }
            else
            {
                var newCate = new Category
                {
                    Name = model.Name
                };

                await _context.Categories.AddAsync(newCate);
            }

            if (await _context.Categories.AnyAsync(c => c.Name == model.Name))
            {
                ModelState.AddModelError("Name", "The Category Name is exist please change category name and tryagen!");
                return View("CategoryForm", model);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return BadRequest();

            var cate = await _context.Categories.FindAsync(id);
            if (cate == null)
                return NotFound();

            _context.Categories.Remove(cate);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return BadRequest();

            var cate = await _context.Categories.FindAsync(id);
            if (cate == null)
                return NotFound();

            ViewData["CreateEditButtons"] = "CategoryForm";

            return View(cate);
        }
    }
}
