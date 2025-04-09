﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TravelAgencyBackend.Models;
using TravelAgencyBackend.ViewModels.Employee;
using TravelAgencyBackend.ViewModles.Employee;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using TravelAgencyBackend.Helpers;



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

        //GET: Employees
        //public async Task<IActionResult> List(EmployeeKeyWordViewModel p)
        //{
        //    var query = _context.Employees
        //        .Where(e => e.Status != EmployeeStatus.Deleted)
        //        .Include(e => e.Role)
        //        .AsQueryable();

        //    if (!string.IsNullOrEmpty(p.txtKeyword))
        //    {
        //        query = query.Where(e => e.Name.Contains(p.txtKeyword) ||
        //                                 e.Email.Contains(p.txtKeyword) ||
        //                                 e.Phone.Contains(p.txtKeyword));
        //    }

        //    // 1️⃣ 先用匿名型別把原始資料查出來（不能用 GetDisplayName）
        //    var rawData = await query
        //        .Select(e => new
        //        {
        //            e.EmployeeId,
        //            e.Name,
        //            e.Gender,
        //            e.BirthDate,
        //            e.Phone,
        //            e.Email,
        //            e.Address,
        //            e.HireDate,
        //            e.Status,
        //            e.Note,
        //            RoleName = e.Role.RoleName
        //        }).ToListAsync();

        //    // 2️⃣ 再轉成 ViewModel，這時就可以安全使用 GetDisplayName()
        //    var result = rawData.Select(e => new EmployeeListViewModel
        //    {
        //        EmployeeId = e.EmployeeId,
        //        Name = e.Name,
        //        Gender = e.Gender,
        //        BirthDate = e.BirthDate,
        //        Phone = e.Phone,
        //        Email = e.Email,
        //        Address = e.Address,
        //        HireDate = e.HireDate,
        //        Status = e.Status, // ✅ 原 enum 型別
        //        Note = e.Note,
        //        RoleName = e.RoleName
        //    }).ToList();


        //    ViewBag.Keyword = p.txtKeyword;

        //    return View(result);
        //}

        public async Task<IActionResult> List(EmployeeKeyWordViewModel p, int page = 1)
        {
            int pageSize = 10;

            var query = _context.Employees
                .Where(e => e.Status != EmployeeStatus.Deleted)
                .Include(e => e.Role)
                .AsQueryable();

            if (!string.IsNullOrEmpty(p.txtKeyword))
            {
                query = query.Where(e => e.Name.Contains(p.txtKeyword) ||
                                         e.Email.Contains(p.txtKeyword) ||
                                         e.Phone.Contains(p.txtKeyword));
            }

            var totalCount = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var rawData = await query
                .OrderBy(e => e.EmployeeId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(e => new
                {
                    e.EmployeeId,
                    e.Name,
                    e.Gender,
                    e.BirthDate,
                    e.Phone,
                    e.Email,
                    e.Address,
                    e.HireDate,
                    e.Status,
                    e.Note,
                    RoleName = e.Role.RoleName
                }).ToListAsync();

            var result = rawData.Select(e => new EmployeeListViewModel
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
                RoleName = e.RoleName
            }).ToList();

            var vm = new EmployeeListPageViewModel
            {
                Employees = result,
                CurrentPage = page,
                TotalPages = totalPages,
                TotalCount = totalCount,
                PageSize = pageSize,
                Keyword = p.txtKeyword
            };

            return View(vm);
        }

        public IActionResult Create()
        {
            ViewBag.RoleList = new SelectList(_context.Roles, "RoleId", "RoleName");
            ViewBag.GenderList = EnumHelper.GetSelectListWithDisplayName<GenderType>();
            ViewBag.StatusList = EnumHelper.GetSelectListWithDisplayName<EmployeeStatus>(excludeDeleted: true);


            var vm = new EmployeeCreateViewModel
            {
                BirthDate = new DateTime(1955, 1, 1),
                HireDate = DateTime.Now
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeCreateViewModel vm)
        {
            ViewBag.RoleList = new SelectList(_context.Roles, "RoleId", "RoleName", vm.RoleId);
            ViewBag.GenderList = EnumHelper.GetSelectListWithDisplayName<GenderType>();
            ViewBag.StatusList = EnumHelper.GetSelectListWithDisplayName<EmployeeStatus>(excludeDeleted: true);


            if (_context.Employees.Any(e => e.Email == vm.Email && e.Status != EmployeeStatus.Deleted))
            {
                ModelState.AddModelError("Email", "此信箱已被使用，請改用其他信箱。");
            }

            // ✅ 驗證 Phone 是否重複
            if (_context.Employees.Any(e => e.Phone == vm.Phone && e.Status != EmployeeStatus.Deleted))
            {
                ModelState.AddModelError("Phone", "此電話號碼已被使用，請再次確認輸入內容。");
            }

            // ❌ 有驗證錯誤就直接回原畫面
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            // ✅ 建立新員工實體
            var emp = new Employee
            {
                Name = vm.Name,
                Password = vm.Password,
                Email = vm.Email,
                Phone = vm.Phone,
                BirthDate = vm.BirthDate,
                HireDate = vm.HireDate,
                Gender = vm.Gender!.Value,
                Status = vm.Status!.Value,
                Address = vm.Address,
                Note = vm.Note,
                RoleId = vm.RoleId!.Value,

            };

            _context.Add(emp);
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

            };

            ViewBag.RoleList = new SelectList(_context.Roles, "RoleId", "RoleName", emp.RoleId);
            ViewBag.GenderList = Enum.GetValues(typeof(GenderType))
            .Cast<GenderType>()
            .Select(g => new SelectListItem
            {
                Text = g.GetType()
                .GetMember(g.ToString())
                .First()
                .GetCustomAttribute<DisplayAttribute>()?.Name ?? g.ToString(),
                Value = ((int)g).ToString()
            }).ToList();

            ViewBag.StatusList = Enum.GetValues(typeof(EmployeeStatus))
                .Cast<EmployeeStatus>()
                .Select(s => new SelectListItem
                {
                    Text = s.GetType()
                            .GetMember(s.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>()?.Name ?? s.ToString(),
                    Value = ((int)s).ToString()
                }).ToList();

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
                ViewBag.StatusList = new SelectList(
                Enum.GetValues(typeof(EmployeeStatus))
                    .Cast<EmployeeStatus>()
                    .Where(s => s != EmployeeStatus.Deleted) 
);

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

            if (!string.IsNullOrWhiteSpace(vm.Password))
            {
                emp.Password = vm.Password;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var emp = await _context.Employees
                .Include(e => e.Role)
                .FirstOrDefaultAsync(e => e.EmployeeId == id);

            if (emp == null) return NotFound();

            var vm = new EmployeeDeleteViewModel
            {
                EmployeeId = emp.EmployeeId,
                Name = emp.Name,
                RoleName = emp.Role.RoleName,
                Phone = emp.Phone,
                Email = emp.Email,
                Note = emp.Note
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int EmployeeId)
        {
            var emp = await _context.Employees.FindAsync(EmployeeId);
            if (emp == null) return NotFound();

            // 軟刪除：只更改狀態，不移除資料
            emp.Status = EmployeeStatus.Deleted;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(List));
        }

        public IActionResult Details(int id)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.EmployeeId == id);

            if (employee == null)
            {
                return NotFound();
            }

            var vm = new EmployeeDetailViewModel
            {
                EmployeeId = employee.EmployeeId,
                Name = employee.Name,
                Gender = employee.Gender.GetDisplayName(),
                BirthDate = employee.BirthDate,
                Phone = employee.Phone,
                Email = employee.Email,
                Address = employee.Address,
                HireDate = employee.HireDate,
                Status = employee.Status.GetDisplayName(),
                Note = employee.Note
            };

            return View(vm);
        }


        /// 檢查指定 ID 的員工是否存在於資料庫中。
        /// Scaffold 預設產生的方法，未來可用於處理資料庫更新時的併發檢查（例如 Edit 或 Delete 操作）。
        /// 目前尚未使用，保留以供日後擴充用。
        //private bool EmployeeExists(int id)
        //{
        //    return _context.Employees.Any(e => e.EmployeeId == id);
        //}
    }

}
