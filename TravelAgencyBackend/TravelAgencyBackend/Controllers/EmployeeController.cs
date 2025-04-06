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
                .Where(e => e.Status != EmployeeStatus.Deleted)
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
            ViewBag.StatusList = new SelectList(
            Enum.GetValues(typeof(EmployeeStatus))
                .Cast<EmployeeStatus>()
                .Where(s => s != EmployeeStatus.Deleted) // 👈 避免讓人選「刪除」
            );

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
            //ViewBag.StatusList = new SelectList(Enum.GetValues(typeof(EmployeeStatus)));
            ViewBag.StatusList = new SelectList(
    Enum.GetValues(typeof(EmployeeStatus))
        .Cast<EmployeeStatus>()
        .Where(s => s != EmployeeStatus.Deleted) // 👈 避免讓人選「刪除」
);


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
            ViewBag.StatusList = new SelectList(
     Enum.GetValues(typeof(EmployeeStatus))
         .Cast<EmployeeStatus>()
         .Where(s => s != EmployeeStatus.Deleted) // 👈 避免讓人選「刪除」
 );


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
                    .Where(s => s != EmployeeStatus.Deleted) // 👈 避免讓人選「刪除」
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

            // ✅ 密碼只有在有輸入時才更新
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

        /// 檢查指定 ID 的員工是否存在於資料庫中。
        /// Scaffold 預設產生的方法，未來可用於處理資料庫更新時的併發檢查（例如 Edit 或 Delete 操作）。
        /// 目前尚未使用，保留以供日後擴充用。
        //private bool EmployeeExists(int id)
        //{
        //    return _context.Employees.Any(e => e.EmployeeId == id);
        //}
    }

}
