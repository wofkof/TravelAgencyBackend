using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TravelAgencyBackend.Models;
using TravelAgencyBackend.ViewModels;

namespace TravelAgencyBackend.Controllers
{
    public class OfficialTravelsController : Controller
    {
        private readonly AppDbContext _context;

        public OfficialTravelsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: OfficialTravels
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.OfficialTravels.Include(o => o.CreatedBy).Include(o => o.Region);
            return View(appDbContext);
        }

        // GET: OfficialTravels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var officialTravel = await _context.OfficialTravels
                .Include(o => o.CreatedBy)
                .Include(o => o.Region)
                .FirstOrDefaultAsync(m => m.OfficialTravelId == id);
            if (officialTravel == null)
            {
                return NotFound();
            }

            return View(officialTravel);
        }

        // GET: OfficialTravels/Create
        public IActionResult Create()
        {
            ViewData["CreatedByEmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "Name");
            ViewData["RegionId"] = new SelectList(_context.Regions, "RegionId", "Name");
            return View();
        }

        // POST: OfficialTravels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ViewModels.OfficialTravelViewModel model)
        {
            if (ModelState.IsValid)
            {
                string? coverPath = null;

                if (model.CoverImage != null && model.CoverImage.Length > 0)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "covers");
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(model.CoverImage.FileName);
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.CoverImage.CopyToAsync(stream);
                    }

                    coverPath = "/uploads/covers/" + uniqueFileName;
                }

                var officialTravel = new OfficialTravel
                {
                    OfficialTravelId = model.OfficialTravelId,
                    CreatedByEmployeeId = model.CreatedByEmployeeId,
                    RegionId = model.RegionId,
                    Title = model.Title,
                    ProjectYear = model.ProjectYear,
                    AvailableFrom = model.AvailableFrom,
                    AvailableUntil = model.AvailableUntil,
                    Description = model.Description,
                    Days = model.Days,
                    CoverPath = coverPath,
                    Status = (Models.TravelStatus)model.Status,
                    CreatedAt = DateTime.Now
                };

                _context.Add(officialTravel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CreatedByEmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "Name", model.CreatedByEmployeeId);
            ViewData["RegionId"] = new SelectList(_context.Regions, "RegionId", "Name", model.RegionId);
            return View(model);
        }

        // GET: OfficialTravels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var officialTravel = await _context.OfficialTravels.FindAsync(id);
            if (officialTravel == null)
            {
                return NotFound();
            }
            ViewData["CreatedByEmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "Name", officialTravel.CreatedByEmployeeId);
            ViewData["RegionId"] = new SelectList(_context.Regions, "RegionId", "Name", officialTravel.RegionId);
            //ViewData["TravelStatusList"] = GetTravelStatusSelectList();
            return View(officialTravel);
        }

        // POST: OfficialTravels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OfficialTravelId,CreatedByEmployeeId,RegionId,Title,ProjectYear,AvailableFrom,AvailableUntil,Description,Days,CoverPath,CreatedAt,Status")] OfficialTravel officialTravel)
        {
            ModelState.Remove("CreatedBy");
            ModelState.Remove("Region");
            if (id != officialTravel.OfficialTravelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    officialTravel.UpdatedAt = DateTime.Now;
                    _context.Update(officialTravel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OfficialTravelExists(officialTravel.OfficialTravelId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CreatedByEmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "Name", officialTravel.CreatedByEmployeeId);
            ViewData["RegionId"] = new SelectList(_context.Regions, "RegionId", "Name", officialTravel.RegionId);
            //ViewData["TravelStatusList"] = GetTravelStatusSelectList();
            return View(officialTravel);
        }

        // GET: OfficialTravels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var officialTravel = await _context.OfficialTravels
                .Include(o => o.CreatedBy)
                .Include(o => o.Region)
                .FirstOrDefaultAsync(m => m.OfficialTravelId == id);
            if (officialTravel == null)
            {
                return NotFound();
            }

            return View(officialTravel);
        }

        // POST: OfficialTravels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var officialTravel = await _context.OfficialTravels.FindAsync(id);
            if (officialTravel != null)
            {
                _context.OfficialTravels.Remove(officialTravel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OfficialTravelExists(int id)
        {
            return _context.OfficialTravels.Any(e => e.OfficialTravelId == id);
        }

        //private IEnumerable<SelectListItem> GetTravelStatusSelectList()
        //{
        //    return Enum.GetValues(typeof(TravelStatus))
        //        .Cast<TravelStatus>()
        //        .Select(e => new SelectListItem
        //        {
        //            Value = e.ToString(),
        //            Text = e.GetType()
        //                    .GetMember(e.ToString())
        //                    .First()
        //                    .GetCustomAttribute<DisplayAttribute>()?.Name ?? e.ToString()
        //        });
        //}

        
    }
}
