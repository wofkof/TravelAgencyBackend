using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelAgencyBackend.Models;
using TravelAgencyBackend.ViewModels;

namespace TravelAgencyBackend.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var employee = await _context.Employees
                .Include(e => e.Role)
                .FirstOrDefaultAsync(e => e.Phone == vm.Phone && e.Password == vm.Password);

            if (employee == null)
            {
                ModelState.AddModelError("", "電話或密碼錯誤");
                return View(vm);
            }

            HttpContext.Session.SetInt32("EmployeeId", employee.EmployeeId);
            HttpContext.Session.SetString("EmployeeName", employee.Name);
            HttpContext.Session.SetInt32("RoleId", employee.RoleId);
            HttpContext.Session.SetString("RoleName", employee.Role.RoleName);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        // 預留：忘記密碼（未來用）
        public IActionResult ForgotPassword()
        {
            return View();
        }
    }
}
