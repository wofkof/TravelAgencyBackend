﻿using Microsoft.AspNetCore.Mvc;
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
                CustomTravel = _context.CustomTravels.Where(d =>
                d.MemberId.ToString().Contains(p.txtKeyword)
                || d.MemberId.ToString().Contains(p.txtKeyword)
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
                    .OrderBy(c => c.Day)
                    .ThenBy(c => c.Time)
                    .ToList();
                var attractions = _context.Attractions.ToList();
                var restaurants = _context.Restaurants.ToList();
                var hotels = _context.Hotels.ToList();
                var transportations = _context.Transportations.ToList();

                var ViewModel = new CustomTravelPendingViewModel
                {
                    CustomTravel = customTravel,
                    Content = content,
                    Attraction = attractions,
                    Restaurant = restaurants,
                    Hotel = hotels,
                    Transportation = transportations
                };

                return View(ViewModel);
            }
            return RedirectToAction("List");
        }
        public IActionResult CreateContent()
        {
            var id = _context.Contents.FirstOrDefault()?.CustomTravelId;
            
            var datas = new CustomTravelPendingViewModel
            {
                NewContent = new Content { CustomTravelId = id.Value },                
                Content = _context.Contents.ToList(),
                City = _context.Cities.ToList(),
                District = _context.Districts.ToList(),
                Attraction = _context.Attractions.ToList(),
                Restaurant = _context.Restaurants.ToList(),
                Hotel = _context.Hotels.ToList(),
                Transportation = _context.Transportations.ToList()
            };

            return View(datas);
        }
        [HttpPost]
        public IActionResult CreateContent(CustomTravelPendingViewModel p)
        {         
            _context.Contents.Add(p.NewContent);
            _context.SaveChanges();
            return RedirectToAction("ContentList", new { id = p.NewContent.CustomTravelId });
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

                    int? customTravelId = d.CustomTravelId;
                    return RedirectToAction("ContentList", new { id = customTravelId });
                }
            }
            return RedirectToAction("List");
        }
        public IActionResult EditContent(int? id)
        {
            if (id == null)
                return RedirectToAction("ContentList", new { id });
            Content d = _context.Contents.FirstOrDefault(p => p.ContentId == id);
            if (d == null)
                return RedirectToAction("ContentList", new { id });

            var datas = new CustomTravelPendingViewModel
            {
                EditContent = d,
                City = _context.Cities.ToList(),
                District = _context.Districts.ToList(),
                Attraction = _context.Attractions.ToList(),
                Restaurant = _context.Restaurants.ToList(),
                Hotel = _context.Hotels.ToList(),
                Transportation = _context.Transportations.ToList()
            };
            return View(datas);
        }
        [HttpPost]
        public IActionResult EditContent(CustomTravelPendingViewModel t)
        {
            Content dbContent = _context.Contents.FirstOrDefault(p => p.ContentId == t.EditContent.ContentId);
            if (dbContent == null)
                return RedirectToAction("ContentList", new { id = t.EditContent.CustomTravelId });

            dbContent.CustomTravelId = t.EditContent.CustomTravelId;
            dbContent.ItemId = t.EditContent.ItemId;
            dbContent.Category = t.EditContent.Category;
            dbContent.Day = t.EditContent.Day;
            dbContent.Time = t.EditContent.Time;
            dbContent.HotelName = t.EditContent.HotelName;
                        
            _context.SaveChanges();
            
            return RedirectToAction("ContentList", new { id = dbContent.CustomTravelId });
        }
    }
}