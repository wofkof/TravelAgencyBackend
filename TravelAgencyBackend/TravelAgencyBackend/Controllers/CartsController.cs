using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TravelAgencyBackend.Helpers;
using TravelAgencyBackend.Models;
using TravelAgencyBackend.Services;
using TravelAgencyBackend.ViewModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TravelAgencyBackend.Controllers
{
    public class CartsController : BaseController
    {
        private readonly AppDbContext _context;
        private readonly PermissionCheckService _perm;

        public CartsController(AppDbContext context, PermissionCheckService perm)
            : base(perm)
        {
            _context = context;
            _perm = perm;
        }

        // GET: Carts
        //public async Task<IActionResult> Index()
        //{
        //    var appDbContext = _context.Carts.Include(c => c.Member);
        //    return View(await appDbContext.ToListAsync());
        //}
        // GET: 購物車

        public async Task<IActionResult> Index(CartKeyWordViewModel p)
        {
            var check = CheckPermissionOrForbid("查看購物車");
            if (check != null) return check;
            
            var 購物車 = _context.Carts
                                .Include(c => c.Member)
                                .AsQueryable();

            if (!string.IsNullOrEmpty(p.txtKeyword))
            {
                // 對 Category 做顯示名稱比對
                var categoryMatches = Enum.GetValues(typeof(CartCategory))
                    .Cast<CartCategory>()
                    .Where(e => e.GetDisplayName().Contains(p.txtKeyword))
                    .Select(e => e.ToString()).ToList();

                // 對 Status 做顯示名稱比對
                var statusMatches = Enum.GetValues(typeof(CartStatus))
                    .Cast<CartStatus>()
                    .Where(e => e.GetDisplayName().Contains(p.txtKeyword))
                    .Select(e => e.ToString()).ToList();

                // 資料庫比對的是 enum 原始名稱（不是中文）
                購物車 = 購物車.Where(e =>
                    categoryMatches.Contains(e.Category.ToString()) ||
                    statusMatches.Contains(e.Status.ToString()));
            }

             p.Carts = await 購物車.ToListAsync();
            return View(p);
        }



        // GET: Carts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var check = CheckPermissionOrForbid("查看購物車");
            if (check != null) return check;

            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Carts
                .Include(c => c.Member)
                .FirstOrDefaultAsync(m => m.CartId == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // GET: Carts/Create
        public IActionResult Create()
        {
            var check = CheckPermissionOrForbid("管理購物車");
            if ( check != null) return check;

            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "Account");
            return View();
        }

        // POST: Carts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CartId,MemberId,ItemId,Category,Status")] Cart cart)
        {
            var check = CheckPermissionOrForbid("管理購物車");
            if (check != null) return check;

            ModelState.Remove("Member");
            if (ModelState.IsValid)
            {
                cart.CreatedAt = DateTime.Now;
                _context.Add(cart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "Account", cart.MemberId);
            return View(cart);
        }

        // GET: Carts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var check = CheckPermissionOrForbid("管理購物車");
            if (check != null) return check;

            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Carts.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "Account", cart.MemberId);
            return View(cart);
        }

        // POST: Carts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CartId,MemberId,ItemId,Category,Status")] Cart cart)
        {
            var check = CheckPermissionOrForbid("管理購物車");
            if (check != null) return check;

            ModelState.Remove("Member");

            if (id != cart.CartId)
            {
                return NotFound();
            }

            var time = await _context.Carts.AsNoTracking().FirstOrDefaultAsync(c => c.CartId == id);

            cart.CreatedAt = time.CreatedAt;
            if (ModelState.IsValid)
            {
                try
                {
                    
                    _context.Update(cart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartExists(cart.CartId))
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
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "Account", cart.MemberId);
            return View(cart);
        }

        // GET: Carts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var check = CheckPermissionOrForbid("管理購物車");
            if (check != null) return check;

            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Carts
                .Include(c => c.Member)
                .FirstOrDefaultAsync(m => m.CartId == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // POST: Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var check = CheckPermissionOrForbid("管理購物車");
            if (check != null) return check;

            var cart = await _context.Carts.FindAsync(id);
            if (cart != null)
            {
                _context.Carts.Remove(cart);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartExists(int id)
        {
            return _context.Carts.Any(e => e.CartId == id);
        }
    }
}
