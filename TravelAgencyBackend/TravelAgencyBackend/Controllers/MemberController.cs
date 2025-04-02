using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelAgencyBackend.Models;
using TravelAgencyBackend.ViewModles;
using TravelAgencyBackend.Helpers;

namespace TravelAgencyBackend.Controllers
{
    public class MemberController : Controller
    {
        private readonly AppDbContext _context;

        public MemberController(AppDbContext context)
        {
            _context = context;
        }

        // 管理員能夠修改會員密碼
        public IActionResult ChangePassword(int id)
        {
            var member = _context.Members.Find(id);
            if (member == null) return NotFound($"找不到 ID 為 {id} 的會員");

            ViewBag.MemberId = id;
            ViewBag.Account = member.Account;
            return View();
        }
        // 管理員能夠修改會員密碼
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePassword(int id, string newPassword, string confirmPassword) 
        {
            if (newPassword != confirmPassword) 
            {
                ModelState.AddModelError(string.Empty, "密碼與確認密碼不一致");
                ViewBag.MemberId = id;
                return View();
            }

            var member = _context.Members.Find(id);
            if (member == null) return NotFound($"找不到 ID 為 {id} 的會員");

            member.Password = PasswordHasher.Hash(newPassword);
            member.UpdatedAt = DateTime.Now;
            _context.SaveChanges();

            return RedirectToAction(nameof(Details), new { id });
        }

        // 會員列表+搜尋功能
        public IActionResult Index(MemberIndexViewModel model)
        {
            string keyword = model.SearchText?.Trim() ?? "";

            var query = _context.Members.AsNoTracking()
                .Where(m =>
                    (string.IsNullOrEmpty(keyword)
                     || m.Name.Contains(keyword)
                     || m.Phone.Contains(keyword)
                     || m.Email.Contains(keyword))
                    &&
                    (!model.FilterStatus.HasValue || m.Status == model.FilterStatus)
                );

            model.TotalCount = query.Count();

            model.Members = query
                .OrderBy(m => m.MemberId)
                .Skip((model.Page - 1) * model.PageSize)
                .Take(model.PageSize)
                .ToList();

            return View(model);
        }

        // 查看會員資料
        public IActionResult Details(int id)
        {
            var member = _context.Members.Find(id);
            if (member == null) return NotFound();

            return View(member);
        }

        // 新增會員
        public IActionResult Create()
        {
            return View();
        }

        // 新增會員
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Member member)
        {
            if (_context.Members.Any(m => m.Account == member.Account))
            {
                ModelState.AddModelError("Account", "此帳號已被註冊");
            }
            if (_context.Members.Any(m => m.Email == member.Email))
            {
                ModelState.AddModelError("Email", "此信箱已被註冊");
            }
            if (_context.Members.Any(m => m.Phone == member.Phone))
            {
                ModelState.AddModelError("Phone", "此手機已被註冊");
            }

            if (ModelState.IsValid)
            {
                member.Password = PasswordHasher.Hash(member.Password);
                member.CreatedAt = DateTime.Now;
                member.Status = MemberStatus.Active;
                _context.Members.Add(member);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }

        // 修改會員資料
        public IActionResult Edit(int id)
        {
            var member = _context.Members.Find(id);
            if (member == null) return NotFound($"找不到 ID 為 {id} 的會員");

            return View(member);
        }

        // 修改會員資料
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Member updatedMember)
        {
            if (id != updatedMember.MemberId) return BadRequest("參數錯誤：id 不一致");

            if (ModelState.IsValid)
            {
                var member = _context.Members.Find(id);
                if (member == null) return NotFound($"找不到 ID 為 {id} 的會員");

                member.Name = updatedMember.Name;
                member.Email = updatedMember.Email;
                member.Phone = updatedMember.Phone;
                member.Status = updatedMember.Status;
                member.Note = updatedMember.Note;
                member.UpdatedAt = DateTime.Now;

                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(updatedMember);
        }

        // 刪除會員
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, Member deleteMember)
        {
            if (id != deleteMember.MemberId) return BadRequest("參數錯誤：id 不一致");

            var member = _context.Members.Find(id);

            if (member == null) return NotFound($"找不到 ID 為 {id} 的會員");

            _context.Members.Remove(member);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}

