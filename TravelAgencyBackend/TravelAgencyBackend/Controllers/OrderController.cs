using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAgencyBackend.Models;
using TravelAgencyBackend.ViewModels.Order;

namespace TravelAgencyBackend.Controllers
{
    public class OrderController : Controller
    {
        private readonly AppDbContext _context;

        public OrderController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Order
        public async Task<IActionResult> Index()
        {
            var orders = await _context.Orders
                .Include(o => o.Member)
                .Include(o => o.Participant)
                .ToListAsync();

            var orderIndexViewModels = orders.Select(o => new OrderIndexViewModel
            {
                OrderId = o.OrderId,
                MemberId = o.MemberId,
                ItemId = o.ItemId,
                Category = o.Category,
                ParticipantsCount = o.ParticipantsCount,
                TotalAmount = o.TotalAmount,
                Status = o.Status,
                CreatedAt = o.CreatedAt,
                Member = o.Member
            }).ToList();

            return View(orderIndexViewModels);
        }

        // GET: Order/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // 假設您是這樣取得訂單資料的 (請根據您的實際程式碼調整)
            var order = await _context.Orders
                .Include(o => o.Member) // 包含會員資料以便顯示名稱
                .Include(o => o.Participant) // 包含參與者資料以便顯示名稱
                .FirstOrDefaultAsync(m => m.OrderId == id);

            if (order == null)
            {
                return NotFound();
            }

            // 建立 ViewModel
            var viewModel = new OrderDetailsViewModel
            {
                OrderId = order.OrderId,
                MemberId = order.MemberId,
                ItemId = order.ItemId, // ItemId 仍然需要傳遞給 ViewModel
                Category = order.Category,
                ParticipantId = order.ParticipantId,
                CreatedAt = order.CreatedAt,
                ParticipantsCount = order.ParticipantsCount,
                TotalAmount = order.TotalAmount,
                Status = order.Status,
                PaymentMethod = order.PaymentMethod,
                PaymentDate = order.PaymentDate,
                Note = order.Note,
                Member = order.Member, // 傳遞 Member 物件
                Participant = order.Participant, // 傳遞 Participant 物件
                ItemName = "無法取得行程名稱" // 先給定預設值
            };

            // --- 加入查詢行程名稱的邏輯 ---
            if (order.Category == OrderCategory.OfficialTravelDetail)
            {
                // 假設 OfficialTravel 有一個 Name 屬性
                var officialTravel = await _context.OfficialTravels
                                                  .FirstOrDefaultAsync(ot => ot.OfficialTravelId == order.ItemId); // 假設 OfficialTravel 的主鍵是 OfficialTravelId
                if (officialTravel != null)
                {
                    viewModel.ItemName = officialTravel.Title; // 假設名稱屬性是 Title
                }
            }
            else if (order.Category == OrderCategory.CustomTravel)
            {
                // 假設 CustomTravel 有一個 Name 屬性
                var customTravel = await _context.CustomTravels
                                                .FirstOrDefaultAsync(ct => ct.CustomTravelId == order.ItemId); // 假設 CustomTravel 的主鍵是 CustomTravelId
                if (customTravel != null)
                {
                    viewModel.ItemName = customTravel.Note; // 假設名稱屬性是 Name
                }
            }
            // --- 查詢邏輯結束 ---

            return View(viewModel); // 將包含 ItemName 的 viewModel 傳給 View
        }

        // GET: Order/Create
        public IActionResult Create()
        {
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "Name");
            ViewData["ParticipantId"] = new SelectList(_context.Participants, "ParticipantId", "Name");

            // Fetch OfficialTravels and CustomTravels for the dropdowns
            var officialTravels = _context.OfficialTravels.Select(ot => new { Value = ot.OfficialTravelId, Text = ot.Title }).ToList();
            var customTravels = _context.CustomTravels.Select(ct => new { Value = ct.CustomTravelId, Text = ct.Note }).ToList();

            var orderCreateViewModel = new OrderCreateViewModel
            {
                OfficialTravels = new SelectList(officialTravels, "Value", "Text"),
                CustomTravels = new SelectList(customTravels, "Value", "Text")
            };

            return View(orderCreateViewModel);
        }

        // POST: Order/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderCreateViewModel orderCreateViewModel)
        {
            if (ModelState.IsValid)
            {
                var order = new Order
                {
                    MemberId = orderCreateViewModel.MemberId,
                    ItemId = orderCreateViewModel.ItemId,
                    Category = orderCreateViewModel.Category,
                    ParticipantId = orderCreateViewModel.ParticipantId,
                    CreatedAt = DateTime.Now,
                    ParticipantsCount = orderCreateViewModel.ParticipantsCount,
                    TotalAmount = orderCreateViewModel.TotalAmount,
                    Status = orderCreateViewModel.Status,
                    PaymentMethod = orderCreateViewModel.PaymentMethod,
                    PaymentDate = orderCreateViewModel.PaymentDate,
                    Note = orderCreateViewModel.Note
                };

                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // If ModelState is invalid, repopulate the dropdowns
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "Name");
            ViewData["ParticipantId"] = new SelectList(_context.Participants, "ParticipantId", "Name");

            var officialTravels = _context.OfficialTravels.Select(ot => new { Value = ot.OfficialTravelId, Text = ot.Title }).ToList();
            var customTravels = _context.CustomTravels.Select(ct => new { Value = ct.CustomTravelId, Text = ct.Note }).ToList();

            orderCreateViewModel.OfficialTravels = new SelectList(officialTravels, "Value", "Text");
            orderCreateViewModel.CustomTravels = new SelectList(customTravels, "Value", "Text");

            return View(orderCreateViewModel);
        }
        // GET: Order/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            // 取得官方行程和客製化行程的資料，用於填充下拉式選單
            var officialTravels = _context.OfficialTravels.Select(ot => new { Value = ot.OfficialTravelId, Text = ot.Title }).ToList();
            var customTravels = _context.CustomTravels.Select(ct => new { Value = ct.CustomTravelId, Text = ct.Note }).ToList();

            var orderEditViewModel = new OrderEditViewModel
            {
                OrderId = order.OrderId,
                MemberId = order.MemberId,
                ItemId = order.ItemId, // 設定初始的 ItemId
                Category = order.Category,
                ParticipantId = order.ParticipantId,
                ParticipantsCount = order.ParticipantsCount,
                TotalAmount = order.TotalAmount,
                Status = order.Status,
                PaymentMethod = order.PaymentMethod,
                PaymentDate = order.PaymentDate,
                Note = order.Note,
                OfficialTravels = new SelectList(officialTravels, "Value", "Text"),
                CustomTravels = new SelectList(customTravels, "Value", "Text")
            };

            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "Name");
            ViewData["ParticipantId"] = new SelectList(_context.Participants, "ParticipantId", "Name");

            return View(orderEditViewModel);
        }

        // POST: Order/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, OrderEditViewModel orderEditViewModel)
        {
            if (id != orderEditViewModel.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var order = await _context.Orders.FindAsync(id);
                    if (order == null)
                    {
                        return NotFound();
                    }

                    order.MemberId = orderEditViewModel.MemberId;
                    order.ItemId = orderEditViewModel.ItemId;
                    order.Category = orderEditViewModel.Category;
                    order.ParticipantId = orderEditViewModel.ParticipantId;
                    order.ParticipantsCount = orderEditViewModel.ParticipantsCount;
                    order.TotalAmount = orderEditViewModel.TotalAmount;
                    order.Status = orderEditViewModel.Status;
                    order.PaymentMethod = orderEditViewModel.PaymentMethod;
                    order.PaymentDate = orderEditViewModel.PaymentDate;
                    order.Note = orderEditViewModel.Note;

                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(orderEditViewModel.OrderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            // 如果 ModelState 無效，重新取得下拉式選單的資料，並傳回 View
            var officialTravels = _context.OfficialTravels.Select(ot => new { Value = ot.OfficialTravelId, Text = ot.Title }).ToList();
            var customTravels = _context.CustomTravels.Select(ct => new { Value = ct.CustomTravelId, Text = ct.Note }).ToList();

            orderEditViewModel.OfficialTravels = new SelectList(officialTravels, "Value", "Text");
            orderEditViewModel.CustomTravels = new SelectList(customTravels, "Value", "Text");

            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "Name");
            ViewData["ParticipantId"] = new SelectList(_context.Participants, "ParticipantId", "Name");

            return View(orderEditViewModel);
        }
        // GET: Order/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Member)
                .Include(o => o.Participant)
                .Include(o => o.OfficialTravelDetail) // 確保包含官方行程
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            // 使用 OrderDetailsViewModel，保持一致
            var orderDetailsViewModel = new OrderDetailsViewModel
            {
                OrderId = order.OrderId,
                MemberId = order.MemberId,
                ItemId = order.ItemId,
                Category = order.Category,
                ParticipantId = order.ParticipantId,
                CreatedAt = order.CreatedAt,
                ParticipantsCount = order.ParticipantsCount,
                TotalAmount = order.TotalAmount,
                Status = order.Status,
                PaymentMethod = order.PaymentMethod,
                PaymentDate = order.PaymentDate,
                Note = order.Note,
                Member = order.Member,
                Participant = order.Participant,
            };

            return View(orderDetailsViewModel);
        }

        // POST: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }
    }
}

