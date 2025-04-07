﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelAgencyBackend.Models;
using TravelAgencyBackend.ViewModels;
using TravelAgencyBackend.Helpers;
using AutoMapper;
using TravelAgencyBackend.ViewComponent;

namespace TravelAgencyBackend.Controllers
{
    public class MemberController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly PermissionCheckService _perm;


        public MemberController(AppDbContext context, IMapper mapper, PermissionCheckService perm)
        {
            _context = context;
            _mapper = mapper;
            _perm = perm;
        }

        // ✅ 修改密碼（GET）
        public IActionResult ChangePassword(int id)
        {
            var member = _context.Members.Find(id);
            if (member == null) return NotFound($"找不到 ID 為 {id} 的會員");

            ViewBag.MemberId = id;
            ViewBag.Account = member.Account;
            return View();
        }

        // ✅ 修改密碼（POST）
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

        // ✅ Index（含搜尋與分頁）
        public IActionResult Index(MemberIndexViewModel model)
        {
            if (!_perm.HasPermission("管理會員")) return Forbid("您沒管理會員的權限");
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

            var result = query
                .OrderBy(m => m.MemberId)
                .Skip((model.Page - 1) * model.PageSize)
                .Take(model.PageSize)
                .ToList();

            model.Members = _mapper.Map<List<MemberListItemViewModel>>(result);
            return View(model);
        }

        // ✅ 詳細資料
        public IActionResult Details(int id)
        {
            var member = _context.Members.Find(id);
            if (member == null) return NotFound();

            var vm = _mapper.Map<MemberDetailViewModel>(member);
            return View(vm);
        }

        // ✅ 建立會員（GET）
        public IActionResult Create()
        {
            return View();
        }

        // ✅ 建立會員（POST）
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MemberCreateViewModel vm)
        {
            if (_context.Members.Any(m => m.Account == vm.Account))
                ModelState.AddModelError("Account", "此帳號已被註冊");

            if (_context.Members.Any(m => m.Email == vm.Email))
                ModelState.AddModelError("Email", "此信箱已被註冊");

            if (_context.Members.Any(m => m.Phone == vm.Phone))
                ModelState.AddModelError("Phone", "此手機已被註冊");

            if (!ModelState.IsValid)
                return View(vm);

            var member = _mapper.Map<Member>(vm);
            member.Password = PasswordHasher.Hash(vm.Password);
            member.CreatedAt = DateTime.Now;
            member.Status = MemberStatus.Active;

            _context.Members.Add(member);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // ✅ 編輯會員（GET）
        public IActionResult Edit(int id)
        {
            var member = _context.Members.Find(id);
            if (member == null) return NotFound($"找不到 ID 為 {id} 的會員");

            var vm = _mapper.Map<MemberEditViewModel>(member);
            return View(vm);
        }

        // ✅ 編輯會員（POST）
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, MemberEditViewModel vm)
        {
            if (id != vm.MemberId) return BadRequest("參數錯誤：id 不一致");

            if (_context.Members.Any(m => m.Email == vm.Email && m.MemberId != vm.MemberId))
                ModelState.AddModelError("Email", "該信箱已被註冊");

            if (_context.Members.Any(m => m.Phone == vm.Phone && m.MemberId != vm.MemberId))
                ModelState.AddModelError("Phone", "該手機號碼已被註冊");

            if (!ModelState.IsValid) return View(vm);

            var member = _context.Members.Find(id);
            if (member == null) return NotFound($"找不到 ID 為 {id} 的會員");

            member.Name = vm.Name;
            member.Email = vm.Email;
            member.Phone = vm.Phone;
            member.Status = vm.Status;
            member.Note = vm.Note;
            member.UpdatedAt = DateTime.Now;

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // ✅ 刪除會員
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var member = _context.Members.Find(id);
            if (member == null) return NotFound($"找不到 ID 為 {id} 的會員");

            _context.Members.Remove(member);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
