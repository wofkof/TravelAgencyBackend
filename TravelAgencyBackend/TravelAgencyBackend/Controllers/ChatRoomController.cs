using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelAgencyBackend.Models;

namespace TravelAgencyBackend.Controllers
{
    public class ChatRoomController : Controller
    {
        private readonly AppDbContext _context;

        public ChatRoomController(AppDbContext context) 
        {
            _context = context;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SendMessage(int ChatRoomId, string Content) 
        {
            // TODO: 目前登入員工 ID
            int emoployeeId = 1;

            var chatRoom = _context.ChatRooms
                .FirstOrDefault(c => c.ChatRoomId == ChatRoomId);
            if (chatRoom == null) return NotFound("聊天室已不存在");

            var message = new Message
            {
                ChatRoomId = ChatRoomId,
                SenderType = SenderType.Employee,
                SenderId = emoployeeId,
                Content = Content,
                SentAt = DateTime.Now,
                IsRead = false
            };

            _context.Messages.Add(message);
            _context.SaveChanges();

            return RedirectToAction("Details", new { id = ChatRoomId });
        }

        // GET: ChatRoomController
        public IActionResult Index()
        {
            // TODO: 目前登入員工 ID
            int emoployeeId = 1;

            var chatRooms = _context.ChatRooms
                .Where(c => c.EmployeeId == emoployeeId)
                .Include(c => c.Member)
                .Include(c => c.Messages)
                .OrderByDescending(c => c.CreatedAt)
                .ToList();

            return View(chatRooms);
        }

        // GET: ChatRoomController/Details/5
        public IActionResult Details(int id)
        {
            var chatRoom = _context.ChatRooms
                .Include(c => c.Member)
                .Include(c => c.Messages)
                .FirstOrDefault(c => c.ChatRoomId == id);

            if (chatRoom == null) return NotFound("聊天室已不存在");

            var unreadMessage = chatRoom.Messages
                .Where(m => m.SenderType == SenderType.Member && !m.IsRead)
                .ToList();

            if (unreadMessage.Any()) 
            {
                foreach (var msg in unreadMessage) 
                {
                    msg.IsRead = true;
                }

                _context.SaveChanges();
            }

            return View(chatRoom);
        }

        // GET: ChatRoomController/Create
        public IActionResult Create()
        {
            var members = _context.Members
                .OrderBy(m => m.Name)
                .ToList();

            return View(members);
        }

        // POST: ChatRoomController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ChatRoomController/Edit/5
        public IActionResult Edit(int id)
        {
            return View();
        }

        // POST: ChatRoomController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ChatRoomController/Delete/5
        public IActionResult Delete(int id)
        {
            return View();
        }

        // POST: ChatRoomController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
