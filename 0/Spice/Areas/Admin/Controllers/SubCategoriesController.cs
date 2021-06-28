using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Spice.Data;
using Spice.Models;
using Spice.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spice.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        [TempData]
        public string StatusMessage { get; set; }

        public SubCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["CreateEditButtons"] = "SubCategoryForm";

            var subCate = await _context.SubCategories.Include(s => s.category).ToListAsync();

            return View(subCate);
        }

        [HttpGet]
        public async Task<IActionResult> SubCategoryForm(int? id)
        {
            if (id == null)
                return BadRequest();

            SubCategoryAndCategoryViewModel model = viewModel();
            model.SubCategory = new Models.SubCategory { };

            if (id > 0)
            {
                var subCate = await _context.SubCategories.FindAsync(id);
                if (subCate == null)
                    return NotFound();
                model.SubCategory = subCate;
            }

            return View("SubCategoryForm", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubCategoryForm(SubCategoryAndCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                SubCategoryAndCategoryViewModel vm = viewModel();
                vm.SubCategory = model.SubCategory;
                return View("SubCategoryForm", vm);
            }

            var doseExistSubCategory = new List<SubCategory>();
            if (model.SubCategory.Id>0)
                doseExistSubCategory = await _context.SubCategories.Include(m => m.category).Where(m => m.category.Id == model.SubCategory.CategoryId && m.Name == model.SubCategory.Name && m.Id != model.SubCategory.Id).ToListAsync();
            else
                doseExistSubCategory = await _context.SubCategories.Include(m => m.category).Where(m => m.category.Id == model.SubCategory.CategoryId && m.Name == model.SubCategory.Name).ToListAsync();


            if (doseExistSubCategory.Any())
            {
                StatusMessage = "Error: This is sub category exists under " + doseExistSubCategory.FirstOrDefault().category.Name + " category.";

                SubCategoryAndCategoryViewModel vm = viewModel();
                vm.SubCategory = model.SubCategory;
                vm.StatusMessage = StatusMessage;

                return View("SubCategoryForm", vm);
            }

            if (model.SubCategory.Id > 0)
                _context.SubCategories.Update(model.SubCategory);
            else
                await _context.SubCategories.AddAsync(model.SubCategory);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> GetSelectedCategories(int id)
        {
            var subcategory = await _context.SubCategories.Where(s => s.CategoryId == id).ToListAsync();
            return Json(new SelectList(subcategory, "Id", "Name"));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return BadRequest();
            var sub = await _context.SubCategories.FindAsync(id);
            if (sub == null)
                return NotFound();

            _context.SubCategories.Remove(sub);
            await _context.SaveChangesAsync();

            return Ok();
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return BadRequest();

            var sub = await _context.SubCategories.Include(m => m.category).SingleOrDefaultAsync(m => m.Id == id);
            if (sub == null)
                return NotFound();

            ViewData["CreateEditButtons"] = "SubCategoryForm";

            return View(sub);
        }

        private SubCategoryAndCategoryViewModel viewModel()
        {
            SubCategoryAndCategoryViewModel vm = new SubCategoryAndCategoryViewModel
            {
                CategoriesList = _context.Categories.ToListAsync().Result
            };

            return vm;
        }
    }
}
