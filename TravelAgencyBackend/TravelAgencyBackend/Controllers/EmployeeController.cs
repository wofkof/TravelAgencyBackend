using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public IActionResult Create()
        {
            ViewBag.RoleList = new SelectList(_context.Roles, "RoleId", "RoleName");
            ViewBag.GenderList = new SelectList(Enum.GetValues(typeof(GenderType)));
            ViewBag.StatusList = new SelectList(Enum.GetValues(typeof(EmployeeStatus)));

            var vm = new EmployeeCreateViewModel
            {
                BirthDate = new DateTime(1955, 1, 1),
                HireDate = DateTime.Now,
                RoleId = 3
            };

            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeCreateViewModel vm)
        {
            ViewBag.RoleList = new SelectList(_context.Roles, "RoleId", "RoleName", vm.RoleId);
            ViewBag.GenderList = new SelectList(Enum.GetValues(typeof(GenderType)));
            ViewBag.StatusList = new SelectList(Enum.GetValues(typeof(EmployeeStatus)));

            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var emp = new Employee
            {
                Name = vm.Name,
                Password = vm.Password,
                Email = vm.Email,
                Phone = vm.Phone,
                BirthDate = vm.BirthDate,
                HireDate = vm.HireDate,
                Gender = vm.Gender,
                Status = vm.Status,
                Address = vm.Address,
                Note = vm.Note,
                RoleId = vm.RoleId
            };

            _context.Add(emp);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }

    }

}
