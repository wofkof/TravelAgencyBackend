using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TravelAgencyBackend.Models;
using TravelAgencyBackend.ViewModels;

namespace TravelAgencyBackend.Controllers
{
    public class ParticipantsController : Controller
    {
        private readonly AppDbContext _context;

        public ParticipantsController(AppDbContext context)
        {
            _context = context;
        }

        // 參與人列表
        public IActionResult Index(ParticipantIndexViewModel model, int? memberId)
        {
            var query = _context.Participants
                .Include(p => p.Member)
                .AsQueryable();

            // 如果從「會員清單」點進來
            if (memberId.HasValue)
            {
                model.FilterMemberId = memberId;
            }

            // 搜尋
            if (!string.IsNullOrWhiteSpace(model.SearchText))
            {
                var keyword = model.SearchText.Trim();
                query = query.Where(p =>
                    p.Name.Contains(keyword) ||
                    p.Phone.Contains(keyword) ||
                    p.IdNumber.Contains(keyword));
            }

            // 篩選會員
            if (model.FilterMemberId.HasValue)
            {
                query = query.Where(p => p.MemberId == model.FilterMemberId);
                var member = _context.Members.FirstOrDefault(m => m.MemberId == model.FilterMemberId);
                ViewBag.Title = $"【{member?.Name ?? "未知"}】的參與人";
            }
            else
            {
                ViewBag.Title = "全部參與人";
            }

            model.TotalCount = query.Count();
            model.Participants = query
                .Skip((model.Page - 1) * model.PageSize)
                .Take(model.PageSize)
                .ToList();

            model.Members = _context.Members.ToList(); // 會員下拉清單

            return View(model);
        }


        // GET: Participants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var participant = await _context.Participants
                .Include(p => p.Member)
                .FirstOrDefaultAsync(m => m.ParticipantId == id);
            if (participant == null)
            {
                return NotFound();
            }

            return View(participant);
        }

        private void SetFormOptions(object? selectedMemberId = null, string? selectedPlace = null)
        {
            ViewBag.Members = new SelectList(_context.Members, "MemberId", "Name", selectedMemberId);
            ViewBag.IssuedPlaces = new SelectList(new[]
            {
                "台北", "台中", "嘉義", "高雄", "花蓮"
            }, selectedPlace);
        }

        // 新增參與人 
        public IActionResult Create(int memberId)
        {
            var member = _context.Members.Find(memberId);
            if (member == null) return NotFound($"找不到 ID 為 {memberId} 的參與人");
            
            var model = new Participant
            {
                MemberId = memberId
            };

            ViewBag.MemberName = member.Name;
            SetFormOptions();
            return View(model);
        }

        // 新增參與人
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Participant participant)
        {
            // 驗證
            if (_context.Participants.Any(p => p.IdNumber == participant.IdNumber))
                ModelState.AddModelError("IdNumber", "身分證號已存在");

            if (_context.Participants.Any(p => p.Phone == participant.Phone))
                ModelState.AddModelError("Phone", "手機已存在");

            if (_context.Participants.Any(p => p.PassportNumber == participant.PassportNumber))
                ModelState.AddModelError("PassportNumber", "護照號碼已存在");

            if (!ModelState.IsValid)
            {
                SetFormOptions(participant.MemberId, participant.IssuedPlace);
                return View(participant);
            }

            _context.Participants.Add(participant);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index), new { memberId = participant.MemberId });

        }

        // GET: Participants/Edit/5
        public IActionResult Edit(int id)
        {
            var participant = _context.Participants
                .Include(p => p.Member)
                .FirstOrDefault(p => p.ParticipantId == id);

            if (participant == null)
                return NotFound($"找不到 ID 為 {id} 的參與人");

            ViewBag.MemberName = participant.Member.Name;
            return View(participant);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Participant participant)
        {
            if (id != participant.ParticipantId) return NotFound($"找不到 ID 為 {id} 的參與人");

            if (_context.Participants.Any(p => p.IdNumber == participant.IdNumber
            && p.ParticipantId != participant.ParticipantId))
                ModelState.AddModelError("IdNumber", "身分證號已存在");

            if (_context.Participants.Any(p => p.Phone == participant.Phone
            && p.ParticipantId != participant.ParticipantId))
                ModelState.AddModelError("Phone", "手機已存在");

            if (_context.Participants.Any(p => p.PassportNumber == participant.PassportNumber
            && p.ParticipantId != participant.ParticipantId))
                ModelState.AddModelError("PassportNumber", "護照號碼已存在");

            if (ModelState.IsValid)
            {
                _context.Update(participant);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index), new { memberId = participant.MemberId });
            }

            return View(participant);
        }

        // POST: Participants/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var participant = _context.Participants.Find(id);
            if (participant == null) return NotFound($"找不到 ID 為 {id} 的參與人");

            int memberId = participant.MemberId;

            _context.Participants.Remove(participant);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index), new { memberId = memberId }); 
        }
    }
}
