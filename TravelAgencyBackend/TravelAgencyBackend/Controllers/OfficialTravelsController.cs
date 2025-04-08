using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TravelAgencyBackend.Models;

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
            return View(await appDbContext.ToListAsync());
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
            ViewData["RegionId"] = new SelectList(_context.Regions, "RegionId", "Country");
            return View();
        }

        // POST: OfficialTravels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CreatedByEmployeeId,RegionId,Title,ProjectYear,AvailableFrom,AvailableUntil,Description,Days,CoverPath,Status")] OfficialTravel officialTravel)
        {
            ModelState.Remove("CreatedBy");
            ModelState.Remove("Region");

            if (ModelState.IsValid)
            {
                officialTravel.CreatedAt = DateTime.Now;
                _context.Add(officialTravel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CreatedByEmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "Name", officialTravel.CreatedByEmployeeId);
            ViewData["RegionId"] = new SelectList(_context.Regions, "RegionId", "Country", officialTravel.RegionId);
            return View(officialTravel);
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
            ViewData["RegionId"] = new SelectList(_context.Regions, "RegionId", "Country", officialTravel.RegionId);
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
            ViewData["RegionId"] = new SelectList(_context.Regions, "RegionId", "Country", officialTravel.RegionId);
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
    }
}
