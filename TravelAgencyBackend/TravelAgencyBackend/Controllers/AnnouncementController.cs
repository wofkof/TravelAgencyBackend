using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TravelAgencyBackend.Models;
using TravelAgencyBackend.ViewModels.Announcement;

namespace TravelAgencyBackend.Controllers
{
    public class AnnouncementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        private readonly AppDbContext _context;

        public AnnouncementController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult List()
        {
            var data = _context.Announcements
                .Where(a => a.Status != AnnouncementStatus.Deleted)
                .Include(a => a.Employee)
                .Select(a => new AnnouncementViewModel
                {
                    AnnouncementId = a.AnnouncementId,
                    Title = a.Title,
                    Content = a.Content,
                    SentAt = a.SentAt,
                    Status = a.Status,
                    EmployeeName = a.Employee.Name
                }).ToList();

            return View(data);
        }


        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Employees = new SelectList(
             _context.Employees
            .Where(e => e.Status == EmployeeStatus.Active),
            "EmployeeId",
            "Name"
            );


            return View();
        }

        [HttpPost]
        public IActionResult Create(AnnouncementViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Employees = new SelectList(
            _context.Employees
            .Where(e => e.Status == EmployeeStatus.Active),
            "EmployeeId",
            "Name"
            );


                return View(vm);
            }

            var announcement = new Announcement
            {
                Title = vm.Title,
                Content = vm.Content,
                SentAt = vm.SentAt,
                Status = vm.Status,
                EmployeeId = vm.EmployeeId
            };

            _context.Announcements.Add(announcement);
            _context.SaveChanges();
            return RedirectToAction("List");
        }
        public IActionResult Details(int id)
        {
            var data = _context.Announcements
                .Include(a => a.Employee)
                .FirstOrDefault(a => a.AnnouncementId == id);

            if (data == null)
            {
                return NotFound();
            }

            var vm = new AnnouncementViewModel
            {
                AnnouncementId = data.AnnouncementId,
                Title = data.Title,
                Content = data.Content,
                SentAt = data.SentAt,
                Status = data.Status,
                EmployeeId = data.EmployeeId,
                EmployeeName = data.Employee.Name
            };

            return View(vm);
        }
        // GET: /Announcement/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var data = _context.Announcements
                .Include(a => a.Employee)
                .FirstOrDefault(a => a.AnnouncementId == id);

            if (data == null)
            {
                return NotFound();
            }

            var vm = new AnnouncementViewModel
            {
                AnnouncementId = data.AnnouncementId,
                Title = data.Title,
                Content = data.Content,
                SentAt = data.SentAt,
                Status = data.Status,
                EmployeeId = data.EmployeeId,
                EmployeeName = data.Employee.Name
            };

            ViewBag.Employees = new SelectList(_context.Employees, "EmployeeId", "Name", vm.EmployeeId);
            return View(vm);
        }

        // POST: /Announcement/Edit/5
        [HttpPost]
        public IActionResult Edit(int id, AnnouncementViewModel vm)
        {
            if (id != vm.AnnouncementId)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Employees = new SelectList(_context.Employees, "EmployeeId", "Name", vm.EmployeeId);
                return View(vm);
            }

            var data = _context.Announcements.FirstOrDefault(a => a.AnnouncementId == id);
            if (data == null)
            {
                return NotFound();
            }

            data.Title = vm.Title;
            data.Content = vm.Content;
            data.SentAt = vm.SentAt;
            data.Status = vm.Status;
            data.EmployeeId = vm.EmployeeId;

            _context.SaveChanges();
            return RedirectToAction("List");
        }

        // GET: /Announcement/Delete/5
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var data = _context.Announcements
                .Include(a => a.Employee)
                .FirstOrDefault(a => a.AnnouncementId == id);

            if (data == null)
                return NotFound();

            var vm = new AnnouncementViewModel
            {
                AnnouncementId = data.AnnouncementId,
                Title = data.Title,
                Content = data.Content,
                SentAt = data.SentAt,
                Status = data.Status,
                EmployeeId = data.EmployeeId,
                EmployeeName = data.Employee.Name
            };

            return View(vm);
        }

        // POST: /Announcement/Delete/5
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var data = _context.Announcements.FirstOrDefault(a => a.AnnouncementId == id);
            if (data == null)
                return NotFound();

            data.Status = AnnouncementStatus.Deleted;
            _context.SaveChanges();

            return RedirectToAction("List");
        }

    }
}
