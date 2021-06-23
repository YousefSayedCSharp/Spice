using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spice.Data;
using Spice.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Spice.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CouponsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CouponsController(ApplicationDbContext context)
        {
            _context = context;
        }
        string CreateEditButtons = "Create";
        public async Task<IActionResult> Index()
        {
            //this is view edit and create name
            ViewData["CreateEditButtons"] = CreateEditButtons;

            var coupons = await _context.Coupons.ToListAsync();

            return View(coupons);
        }

        public async Task<IActionResult> Create(int? id)
        {
            if (id == null)
                return BadRequest();

            var coupon = await _context.Coupons.FindAsync(id);
            if (id > 0)
            {
                if (coupon == null)
                    return NotFound();

                return View(coupon);
            }

            return View(new Coupon { });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Coupon model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if(model.CouponType != "Percent"||model.CouponType!="Doller")
            {
                ModelState.AddModelError("CouponType","Please Select the coupon type of list!");
                return View(model);
            }

            var files = HttpContext.Request.Form.Files;

            if(files.Count>0)
            {
                var fs = files[0].OpenReadStream();
                var ms = new MemoryStream();
                fs.CopyTo(ms);
                model.Picture = ms.ToArray();
            }

            if (model.Id > 0)
                _context.Update(model);
            else
                await _context.AddAsync(model);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> details(int? id)
        {
            if (id == null)
                return BadRequest();

            var coupon = await _context.Coupons.FindAsync(id);
            if (coupon == null)
                return NotFound();

            ViewData["CreateEditButtons"] = CreateEditButtons;

            return View(coupon);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return BadRequest();

            var coupon = await _context.Coupons.FindAsync(id);
            if (coupon == null)
                return NotFound();

            _context.Coupons.Remove(coupon);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }//end c@section Scripts{lass
}//end main
