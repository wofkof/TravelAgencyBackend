using Microsoft.AspNetCore.Mvc;
using TravelAgencyBackend.Models;
using TravelAgencyBackend.ViewModels;

namespace TravelAgencyBackend.Controllers
{
    public class CustomTravelPendingController : Controller
    {
        private readonly AppDbContext _context;
        public CustomTravelPendingController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult List(KeywordViewModel p)
        {
            IEnumerable<CustomTravel> CustomTravel = null;
            if (string.IsNullOrEmpty(p.txtKeyword))
            {
                CustomTravel = from d in _context.CustomTravels
                               select d;
            }
            else
                CustomTravel = _context.CustomTravels.Where(d => d.MemberId.ToString().Contains(p.txtKeyword)
                || d.ReviewEmployeeId.ToString().Contains(p.txtKeyword)
                );
            var datas = new CustomTravelPendingViewModel
            {
                CustomTravel = CustomTravel
            };
            return View(datas);
        }


        public IActionResult DeleteOrder(int? id)
        {
            if (id != null)
            {

                CustomTravel d = _context.CustomTravels.FirstOrDefault(p => p.CustomTravelId == id);
                if (d != null)
                {
                    _context.CustomTravels.Remove(d);
                    _context.SaveChanges();
                }
            }
            return RedirectToAction("List");
        }
        public IActionResult EditOrder(int? id)
        {
            if (id == null)
                return RedirectToAction("List");
            CustomTravel d = _context.CustomTravels.FirstOrDefault(p => p.CustomTravelId == id);
            if (d == null)
                return RedirectToAction("List");
            return View(d);
        }
        [HttpPost]
        public IActionResult EditOrder(CustomTravel uiCustomTravel)
        {
            CustomTravel dbCustomTravel = _context.CustomTravels.FirstOrDefault(p => p.CustomTravelId == uiCustomTravel.CustomTravelId);
            if (dbCustomTravel == null)
                return RedirectToAction("List");
            dbCustomTravel.MemberId = uiCustomTravel.MemberId;
            dbCustomTravel.ReviewEmployeeId = uiCustomTravel.ReviewEmployeeId;
            dbCustomTravel.CreatedAt = uiCustomTravel.CreatedAt;
            dbCustomTravel.UpdatedAt = uiCustomTravel.UpdatedAt;
            dbCustomTravel.DepartureDate = uiCustomTravel.DepartureDate;
            dbCustomTravel.EndDate = uiCustomTravel.EndDate;
            dbCustomTravel.Days = uiCustomTravel.Days;
            dbCustomTravel.People = uiCustomTravel.People;
            dbCustomTravel.TotalAmount = uiCustomTravel.TotalAmount;
            dbCustomTravel.Status = uiCustomTravel.Status;
            dbCustomTravel.Note = uiCustomTravel.Note;

            _context.SaveChanges();
            return RedirectToAction("List");
        }

        public IActionResult ContentList(int? id)
        {
            IEnumerable<CustomTravel> CustomTravel = null;
            if (id != null)
            {
                var customTravel = _context.CustomTravels
            .Where(d => d.CustomTravelId == id)
            .ToList();

                var content = _context.Contents
                    .Where(c => c.CustomTravelId == id)
                    .ToList();

                var ViewModel = new CustomTravelPendingViewModel
                {
                    CustomTravel = customTravel,
                    Content = content
                };

                return View(ViewModel);
            }
            return RedirectToAction("List");
        }
        public IActionResult CreateContent()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateContent(Content p)
        {
            _context.Contents.Add(p);
            _context.SaveChanges();
            return RedirectToAction("List");
        }
        public IActionResult DeleteContent(int? id)
        {
            if (id != null)
            {

                Content d = _context.Contents.FirstOrDefault(p => p.ContentId == id);
                if (d != null)
                {
                    _context.Contents.Remove(d);
                    _context.SaveChanges();
                }
            }
            return RedirectToAction("List");
        }
        public IActionResult EditContent(int? id)
        {
            if (id == null)
                return RedirectToAction("List");
            Content d = _context.Contents.FirstOrDefault(p => p.ContentId == id);
            if (d == null)
                return RedirectToAction("List");
            return View(d);
        }
        [HttpPost]
        public IActionResult EditContent(Content uiContent)
        {
            Content dbContent = _context.Contents.FirstOrDefault(p => p.ContentId == uiContent.ContentId);
            if (dbContent == null)
                return RedirectToAction("List");
            dbContent.CustomTravelId = uiContent.CustomTravelId;
            dbContent.ItemId = uiContent.ItemId;
            dbContent.Category = uiContent.Category;
            dbContent.Day = uiContent.Day;
            dbContent.Time = uiContent.Time;
            dbContent.HotelName = uiContent.HotelName;

            _context.SaveChanges();
            return RedirectToAction("List");
        }
    }
}