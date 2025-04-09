using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelAgencyBackend.Models;
using TravelAgencyBackend.ViewModels;

namespace TravelAgencyBackend.Controllers
{
    public class PermissionController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public PermissionController(AppDbContext context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: PermissionController
        public IActionResult Index()
        {
            var permissions = _context.Permissions.OrderBy(p => p.PermissionId).ToList();
            var viewModels = _mapper.Map<List<PermissionViewModel>>(permissions);

            return View(viewModels);
        }

        // GET: PermissionController/Details/5
        public IActionResult Details(int id)
        {
            return View();
        }

        // GET: PermissionController/Create
        public IActionResult Create() => View();

        // POST: PermissionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PermissionViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var entity = _mapper.Map<Permission>(model);
            _context.Permissions.Add(entity);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // GET: PermissionController/Edit/5
        public IActionResult Edit(int id)
        {
            var entity = _context.Permissions.Find(id);
            if (entity == null) return NotFound($"查無 ID 為 {id} 參數");

            var model = _mapper.Map<PermissionViewModel>(entity);

            return View(model);
        }

        // POST: PermissionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PermissionViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var entity = _context.Permissions.Find(model.PermissionId);
            if (entity == null) return NotFound($"查無 {model} 參數");

            _mapper.Map(model, entity);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // GET: PermissionController/Delete/5
        public IActionResult Delete(int id)
        {
            var entity = _context.Permissions.Find(id);
            if (entity == null) return NotFound($"查無 ID 為 {id} 參數");

            _context.Permissions.Remove(entity);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

    }
}
