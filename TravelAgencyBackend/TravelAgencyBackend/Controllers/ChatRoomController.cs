using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
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

        //取訊息Json
        [HttpGet]
        public IActionResult GetMessages(int chatRoomId) 
        {
            var messages = _context.Messages
                .Where(m => m.ChatRoomId == chatRoomId)
                .OrderBy(m => m.SentAt)
                .Select(m => new 
                {
                    sender = m.SenderType.ToString(),
                    content = m.Content,
                    sentAt = m.SentAt.ToString("yyyy-MM-dd HH:mm"),
                    isRead = m.IsRead,
                })
                .ToList();

            return Json(messages);
        }

        // 關閉聊天室
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Close(int id) 
        {
            var chatRoom = _context.ChatRooms
                .FirstOrDefault(c => c.ChatRoomId == id);
            if (chatRoom == null) return NotFound("聊天室已不存在");

            chatRoom.Status = ChatStatus.Closed;
            _context.SaveChanges();

            return RedirectToAction("Details", new { id });
        }

        // 發送訊息
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SendMessage(int ChatRoomId, string Content) 
        {
            // TODO: 目前登入員工 ID
            int emoployeeId = 1;
            //int.Parse(User.FindFirst("EmployeeId").Value);
            var chatRoom = _context.ChatRooms
                .FirstOrDefault(c => c.ChatRoomId == ChatRoomId);
            if (chatRoom == null) return NotFound("聊天室已不存在");

            var message = new Models.Message
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

        //聊天室列表
        public IActionResult Index()
        {
            // TODO: 目前登入員工 ID
            int emoployeeId = 1;
            //int.Parse(User.FindFirst("EmployeeId").Value);
            var chatRooms = _context.ChatRooms
                .Where(c => c.EmployeeId == emoployeeId)
                .Include(c => c.Member)
                .Include(c => c.Messages)
                .OrderByDescending(c => c.CreatedAt)
                .ToList();

            return View(chatRooms);
        }

        //查看聊天室
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

        // 建立聊天室
        public IActionResult Create()
        {
            var members = _context.Members
                .OrderBy(m => m.Name)
                .ToList();

            return View(members);
        }

        // 建立聊天室
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(int memberId)
        {
            int employeeId = 1;
            //int.Parse(User.FindFirst("EmployeeId").Value);

            //檢查是否有聊天室
            var existingChat = _context.ChatRooms
                .FirstOrDefault(c => c.EmployeeId == employeeId && c.MemberId == memberId);

            if (existingChat != null) 
            {
                return RedirectToAction("Details", new { id = existingChat.ChatRoomId });
            }
            //建立新聊天室
            var newChat = new ChatRoom
            {
                EmployeeId = employeeId,
                MemberId = memberId,
                CreatedAt = DateTime.Now,
                Status = ChatStatus.Opened
            };

            _context.ChatRooms.Add(newChat);
            _context.SaveChanges();

            return RedirectToAction("Details", new { id = newChat.ChatRoomId });
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
