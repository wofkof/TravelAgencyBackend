using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelAgencyBackend.Models;
using TravelAgencyBackend.ViewModels;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace TravelAgencyBackend.Controllers
{
    public class CustomTravelProductsController : Controller
    {
        private readonly AppDbContext _context;
        public CustomTravelProductsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult List(KeywordViewModel p)
        {
            IEnumerable<City> City = null;
            if (string.IsNullOrEmpty(p.txtKeyword))
            {
                City = _context.Cities.ToList();
            }
            else
            City = _context.Cities.Where(d => d.CityName.Contains(p.txtKeyword)).ToList();

            IEnumerable<District> District = null;
            if (string.IsNullOrEmpty(p.txtKeyword))
            {
                District = _context.Districts.ToList();
            }
            else
                District = _context.Districts.Where(d => d.DistrictName.Contains(p.txtKeyword)).ToList();

            IEnumerable<Attraction> Attraction = null;
            if (string.IsNullOrEmpty(p.txtKeyword))
            {
                Attraction = _context.Attractions.ToList();
            }
            else
                Attraction = _context.Attractions.Where(d => d.ScenicSpotName.Contains(p.txtKeyword)).ToList();

            IEnumerable<Restaurant> Restaurant = null;
            if (string.IsNullOrEmpty(p.txtKeyword))
            {
                Restaurant = _context.Restaurants.ToList();
            }
            else
                Restaurant = _context.Restaurants.Where(d => d.RestaurantName.Contains(p.txtKeyword)).ToList();

            IEnumerable<Hotel> Hotel = null;
            if (string.IsNullOrEmpty(p.txtKeyword))
            {
                Hotel = _context.Hotels.ToList();
            }
            else
                Hotel = _context.Hotels.Where(d => d.HotelName.Contains(p.txtKeyword)).ToList();

            IEnumerable<Transportation> Transportation = null;
            if (string.IsNullOrEmpty(p.txtKeyword))
            {
                Transportation = _context.Transportations.ToList();
            }
            else
                Transportation = _context.Transportations.Where(d => d.TransportMethod.Contains(p.txtKeyword)).ToList();


            var datas = new CustomTravelProductsViewModel
            {
                City = City,
                District = District,
                Attraction = Attraction,
                Restaurant = Restaurant,
                Hotel = Hotel,
                Transportation = Transportation
            };
            return View(datas);
        }

        //City
        public IActionResult CreateCity()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateCity(City p)
        {
            //AppDbContext db = new AppDbContext();
            //db.Cities.Add(p.city);
            //db.SaveChanges();
            _context.Cities.Add(p);
            _context.SaveChanges();
            return RedirectToAction("List");
        }

        public IActionResult DeleteCity(int? id)
        {
            if (id != null)
            {
                
                City d = _context.Cities.FirstOrDefault(p => p.CityId == id);
                if (d != null)
                {
                    _context.Cities.Remove(d);
                    _context.SaveChanges();
                }
            }
            return RedirectToAction("List");
        }
        public IActionResult EditCity(int? id)
        {
            if (id == null)
                return RedirectToAction("List");            
            City d = _context.Cities.FirstOrDefault(p => p.CityId == id);
            if (d == null)
                return RedirectToAction("List");
            return View(d);
        }
        [HttpPost]
        public IActionResult EditCity(City uiCity)
        {            
            City dbCity = _context.Cities.FirstOrDefault(p => p.CityId == uiCity.CityId);
            if (dbCity == null)
                return RedirectToAction("List");
            dbCity.CityName = uiCity.CityName;
            _context.SaveChanges();
            return RedirectToAction("List");
        }

        //District
        public IActionResult CreateDistrict()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateDistrict(District p)
        {            
            _context.Districts.Add(p);
            _context.SaveChanges();
            return RedirectToAction("List");
        }

        public IActionResult DeleteDistrict(int? id)
        {
            if (id != null)
            {                
                District d = _context.Districts.FirstOrDefault(p => p.DistrictId == id);
                if (d != null)
                {
                    _context.Districts.Remove(d);
                    _context.SaveChanges();
                }
            }
            return RedirectToAction("List");
        }
        public IActionResult EditDistrict(int? id)
        {
            if (id == null)
                return RedirectToAction("List");            
            District d = _context.Districts.FirstOrDefault(p => p.DistrictId == id);
            if (d == null)
                return RedirectToAction("List");
            return View(d);
        }
        [HttpPost]
        public IActionResult EditDistrict(District uiDistrict)
        {           
            District dbDistrict = _context.Districts.FirstOrDefault(p => p.DistrictId == uiDistrict.DistrictId);
            if (dbDistrict == null)
                return RedirectToAction("List");
            dbDistrict.CityId = uiDistrict.CityId;
            dbDistrict.DistrictName = uiDistrict.DistrictName;            
            _context.SaveChanges();
            return RedirectToAction("List");
        }

        //Attraction
        public IActionResult CreateAttraction()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateAttraction(Attraction p)
        {            
            _context.Attractions.Add(p);
            _context.SaveChanges();
            return RedirectToAction("List");
        }

        public IActionResult DeleteAttraction(int? id)
        {
            if (id != null)
            {                
                Attraction d = _context.Attractions.FirstOrDefault(p => p.AttractionId == id);
                if (d != null)
                {
                    _context.Attractions.Remove(d);
                    _context.SaveChanges();
                }
            }
            return RedirectToAction("List");
        }
        public IActionResult EditAttraction(int? id)
        {
            if (id == null)
                return RedirectToAction("List");            
            Attraction d = _context.Attractions.FirstOrDefault(p => p.AttractionId == id);
            if (d == null)
                return RedirectToAction("List");
            return View(d);
        }
        [HttpPost]
        public IActionResult EditAttraction(Attraction uiAttraction)
        {
            
            Attraction dbAttraction =_context.Attractions.FirstOrDefault(p => p.AttractionId == uiAttraction.AttractionId);
            if (dbAttraction == null)
                return RedirectToAction("List");
            dbAttraction.DistrictId = uiAttraction.DistrictId;
            dbAttraction.TravelSupplierId = uiAttraction.TravelSupplierId;
            dbAttraction.ScenicSpotName = uiAttraction.ScenicSpotName;
            _context.SaveChanges();
            return RedirectToAction("List");
        }

        //Restaurant
        public IActionResult CreateRestaurant()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateRestaurant(Restaurant p)
        {            
            _context.Restaurants.Add(p);
            _context.SaveChanges();
            return RedirectToAction("List");
        }

        public IActionResult DeleteRestaurant(int? id)
        {
            if (id != null)
            {               
                Restaurant d = _context.Restaurants.FirstOrDefault(p => p.RestaurantId == id);
                if (d != null)
                {
                    _context.Restaurants.Remove(d);
                    _context.SaveChanges();
                }
            }
            return RedirectToAction("List");
        }
        public IActionResult EditRestaurant(int? id)
        {
            if (id == null)
                return RedirectToAction("List");            
            Restaurant d = _context.Restaurants.FirstOrDefault(p => p.RestaurantId == id);
            if (d == null)
                return RedirectToAction("List");
            return View(d);
        }
        [HttpPost]
        public IActionResult EditRestaurant(Restaurant uiRestaurant)
        {            
            Restaurant dbRestaurant = _context.Restaurants.FirstOrDefault(p => p.RestaurantId == uiRestaurant.RestaurantId);
            if (dbRestaurant == null)
                return RedirectToAction("List");
            dbRestaurant.DistrictId = uiRestaurant.DistrictId;
            dbRestaurant.TravelSupplierId = uiRestaurant.TravelSupplierId;
            dbRestaurant.RestaurantName = uiRestaurant.RestaurantName;
            _context.SaveChanges();
            return RedirectToAction("List");
        }

        //Hotel
        public IActionResult CreateHotel()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateHotel(Hotel p)
        {            
            _context.Hotels.Add(p);
            _context.SaveChanges();
            return RedirectToAction("List");
        }

        public IActionResult DeleteHotel(int? id)
        {
            if (id != null)
            {               
                Hotel d = _context.Hotels.FirstOrDefault(p => p.HotelId == id);
                if (d != null)
                {
                    _context.Hotels.Remove(d);
                    _context.SaveChanges();
                }
            }
            return RedirectToAction("List");
        }
        public IActionResult EditHotel(int? id)
        {
            if (id == null)
                return RedirectToAction("List");           
            Hotel d = _context.Hotels.FirstOrDefault(p => p.HotelId == id);
            if (d == null)
                return RedirectToAction("List");
            return View(d);
        }
        [HttpPost]
        public IActionResult EditHotel(Hotel uiHotel)
        {           
            Hotel dbHotel = _context.Hotels.FirstOrDefault(p => p.HotelId == uiHotel.HotelId);
            if (dbHotel == null)
                return RedirectToAction("List");
            dbHotel.DistrictId = uiHotel.DistrictId;
            dbHotel.TravelSupplierId = uiHotel.TravelSupplierId;
            dbHotel.HotelName = uiHotel.HotelName;
            _context.SaveChanges();
            return RedirectToAction("List");
        }

        //Transportation
        public IActionResult CreateTransportation()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateTransportation(Transportation p)
        {            
            _context.Transportations.Add(p);
            _context.SaveChanges();
            return RedirectToAction("List");
        }

        public IActionResult DeleteTransportation(int? id)
        {
            if (id != null)
            {                
                Transportation d = _context.Transportations.FirstOrDefault(p => p.TransportId == id);
                if (d != null)
                {
                    _context.Transportations.Remove(d);
                    _context.SaveChanges();
                }
            }
            return RedirectToAction("List");
        }
        public IActionResult EditTransportation(int? id)
        {
            if (id == null)
                return RedirectToAction("List");
            Transportation d = _context.Transportations.FirstOrDefault(p => p.TransportId == id);
            if (d == null)
                return RedirectToAction("List");
            return View(d);
        }
        [HttpPost]
        public IActionResult EditTransportation(Transportation uiTransportation)
        {            
            Transportation dbTransportation = _context.Transportations.FirstOrDefault(p => p.TransportId == uiTransportation.TransportId);
            if (dbTransportation == null)
                return RedirectToAction("List");
            dbTransportation.TransportMethod = uiTransportation.TransportMethod;
            _context.SaveChanges();
            return RedirectToAction("List");
        }
    }
}
