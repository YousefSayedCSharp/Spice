using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spice.Data;
using Spice.ViewModels;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Spice.Controllers
{
    [Area("Admin")]
    public class MenuItemsController : Controller
    {
        string CreateEditButtons = "MenuItemForm";
        string folderImagesName = "Images";
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        [BindProperty]
        public MenuItemViewModel MenuItemVM { get; set; }

        public MenuItemsController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            MenuItemVM = new MenuItemViewModel
            {
                menuItem = new Models.MenuItem(),
                categoriesList = _context.Categories.ToListAsync().Result
            };
        }

        public async Task<IActionResult> Index()
        {
            ViewData["CreateEditButtons"] = CreateEditButtons;

            var menuItems = await _context.MenuItems.Include(c => c.category).Include(s => s.SubCategory).ToListAsync();

            return View(menuItems);
        }

        public async Task<IActionResult> MenuItemForm(int? id)
        {
            if (id == null)
                return BadRequest();

            if (id > 0)
            {
                var menuItem = _context.MenuItems.Include(m => m.category).Include(m => m.SubCategory).SingleOrDefault(m => m.Id == id);
                if (menuItem == null)
                    return NotFound();

                MenuItemVM.menuItem = menuItem;
                MenuItemVM.subCategoriesList = await _context.SubCategories.Where(s => s.CategoryId == menuItem.CategoryId).ToListAsync();
            }

            return View(CreateEditButtons, MenuItemVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create()
        {
            var categories = await _context.Categories.ToListAsync();
            var subCategories = await _context.SubCategories.Include(m => m.category).ToListAsync();

            if (!ModelState.IsValid)
            {
                MenuItemVM.categoriesList = categories;
                MenuItemVM.subCategoriesList = subCategories;
                return View(CreateEditButtons, MenuItemVM);
            }

            if (MenuItemVM.menuItem.Spicyness == "Select Spicy")
            {
                ModelState.AddModelError("menuItem.Spicyness", "Please Select as list spicy!");
                MenuItemVM.categoriesList = categories;
                MenuItemVM.subCategoriesList = subCategories;
                return View(CreateEditButtons, MenuItemVM);
            }

            string imagePath = @"\" + folderImagesName + @"\default.jpg";
            if (MenuItemVM.menuItem.Id > 0)
                imagePath = MenuItemVM.menuItem.Image;

            var files = HttpContext.Request.Form.Files;
            if (files.Any())
            {
                string webRootPath = _hostEnvironment.WebRootPath;
                string imageName = DateTime.Now.ToFileTime().ToString() + Path.GetExtension(files.FirstOrDefault().FileName);
                FileStream fs = new FileStream(Path.Combine(webRootPath, folderImagesName, imageName), FileMode.Create);
                files.FirstOrDefault().CopyTo(fs);

                imagePath = "/" + folderImagesName + "/" + imageName;

                if (MenuItemVM.menuItem.Id > 0)
                    MenuItemVM.menuItem.Image = imagePath;
            }

            if (MenuItemVM.menuItem.Id == 0)
                MenuItemVM.menuItem.Image = imagePath;

            if (MenuItemVM.menuItem.Id > 0)
                _context.MenuItems.Update(MenuItemVM.menuItem);
            else
                await _context.MenuItems.AddAsync(MenuItemVM.menuItem);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return BadRequest();

            var MI = await _context.MenuItems.Include(m => m.category).Include(m => m.SubCategory).SingleOrDefaultAsync(m => m.Id == id);
            if (MI == null)
                return NotFound();
            ViewData["CreateEditButtons"] = CreateEditButtons;
            return View(MI);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return BadRequest();

            var MI = await _context.MenuItems.Include(m => m.category).Include(m => m.SubCategory).SingleOrDefaultAsync(m => m.Id == id);
            if (MI == null)
                return NotFound();
            
            _context.MenuItems.Remove(MI);
            await _context.SaveChangesAsync();

            ViewData["CreateEditButtons"] = CreateEditButtons;

            return Ok();
        }
    }
}
