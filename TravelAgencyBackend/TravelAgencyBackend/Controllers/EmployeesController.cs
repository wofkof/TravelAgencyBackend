using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TravelAgencyBackend.Models;
using TravelAgencyBackend.ViewModels;


namespace TravelAgencyBackend.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly AppDbContext _context;

        public EmployeesController(AppDbContext context)
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

            var result = await query.ToListAsync();
            ViewBag.Keyword = p.txtKeyword;

            return View(result);
        }


        public IActionResult Create()
        {
            ViewBag.RoleList = new SelectList(_context.Roles, "RoleId", "RoleName");

            ViewBag.GenderList = new SelectList(new[]
            {
                new { Value = "Male", Text = "男性" },
                new { Value = "Female", Text = "女性" },
                new { Value = "Other", Text = "其他" }
            }, "Value", "Text");

            ViewBag.StatusList = new SelectList(new[]
            {
                new { Value = "Active", Text = "正常" },
                new { Value = "Suspended", Text = "停權" }
            }, "Value", "Text");

            var emp = new Employee
            {
                HireDate = DateTime.Now,
                BirthDate = new DateTime(1955, 1, 1),
                RoleId = 3
            };


            return View(emp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee)
        {
            ViewBag.RoleList = new SelectList(_context.Roles, "RoleId", "RoleName");
            ViewBag.GenderList = new SelectList(new[]
            {
        new { Value = "Male", Text = "男性" },
        new { Value = "Female", Text = "女性" },
        new { Value = "Other", Text = "其他" }
    }, "Value", "Text");

            ViewBag.StatusList = new SelectList(new[]
            {
        new { Value = "Active", Text = "正常" },
        new { Value = "Suspended", Text = "停權" }
    }, "Value", "Text");

            if (!ModelState.IsValid)
            {
                return View(employee);
            }

            _context.Add(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var emp = await _context.Employees.FindAsync(id);
            if (emp == null) return NotFound();

            var vm = new EmployeeEditViewModel
            {
                EmployeeId = emp.EmployeeId,
                Name = emp.Name,
                Email = emp.Email,
                Phone = emp.Phone,
                BirthDate = emp.BirthDate,
                HireDate = emp.HireDate,
                Gender = emp.Gender,
                Status = emp.Status,
                Address = emp.Address,
                Note = emp.Note,
                RoleId = emp.RoleId
                // ❌ 不帶 Password，避免洩漏
            };

            ViewBag.RoleList = new SelectList(_context.Roles, "RoleId", "RoleName", emp.RoleId);
            ViewBag.GenderList = new SelectList(Enum.GetValues(typeof(GenderType)).Cast<GenderType>());
            ViewBag.StatusList = new SelectList(Enum.GetValues(typeof(EmployeeStatus)).Cast<EmployeeStatus>());

            return View(vm);
        }



        // POST: Employees/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EmployeeEditViewModel vm)
        {
            if (id != vm.EmployeeId) return NotFound();

            if (!ModelState.IsValid)
            {
                ViewBag.RoleList = new SelectList(_context.Roles, "RoleId", "RoleName", vm.RoleId);
                ViewBag.GenderList = new SelectList(Enum.GetValues(typeof(GenderType)).Cast<GenderType>());
                ViewBag.StatusList = new SelectList(Enum.GetValues(typeof(EmployeeStatus)).Cast<EmployeeStatus>());
                return View(vm);
            }

            var emp = await _context.Employees.FindAsync(id);
            if (emp == null) return NotFound();

            // 更新欄位
            emp.Name = vm.Name;
            emp.Email = vm.Email;
            emp.Phone = vm.Phone;
            emp.BirthDate = vm.BirthDate;
            emp.HireDate = vm.HireDate;
            emp.Gender = vm.Gender;
            emp.Status = vm.Status;
            emp.Address = vm.Address;
            emp.Note = vm.Note;
            emp.RoleId = vm.RoleId;

            // ✅ 密碼只有在有輸入時才更新
            if (!string.IsNullOrWhiteSpace(vm.Password))
            {
                emp.Password = vm.Password;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }



        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Role)
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.EmployeeId == id);
        }
    }
}
