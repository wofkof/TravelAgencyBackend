using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelAgencyBackend.Models;
using TravelAgencyBackend.ViewModels.Employee;

namespace TravelAgencyBackend.Controllers
{
    public class EmployeeController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}
        private readonly AppDbContext _context;

        public EmployeeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> List(EmployeeKeyWordViewModel p)
        {
            var query = _context.Employees
                                .Include(e => e.Role)
                                .AsQueryable();

            if (!string.IsNullOrEmpty(p.txtKeyword))
            {
                query = query.Where(e => e.Name.Contains(p.txtKeyword) ||
                                         e.Email.Contains(p.txtKeyword) ||
                                         e.Phone.Contains(p.txtKeyword));
            }

            var result = await query
                .Select(e => new EmployeeListViewModel
                {
                    EmployeeId = e.EmployeeId,
                    Name = e.Name,
                    Gender = e.Gender,
                    BirthDate = e.BirthDate,
                    Phone = e.Phone,
                    Email = e.Email,
                    Address = e.Address,
                    HireDate = e.HireDate,
                    Status = e.Status,
                    Note = e.Note,
                    RoleName = e.Role.RoleName
                }).ToListAsync();

            ViewBag.Keyword = p.txtKeyword;

            return View(result);
        }
    }
}
