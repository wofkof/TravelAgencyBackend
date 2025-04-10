using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TravelAgencyBackend.Models;
using TravelAgencyBackend.ViewModels.Cart;

namespace TravelAgencyBackend.Controllers
{
    public class CartsController : Controller
    {
        private readonly AppDbContext _context;

        public CartsController(AppDbContext context)
        {
            _context = context;
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
            var 購物車 = _context.Carts.Include(c => c.Member).AsQueryable();

            if (!string.IsNullOrEmpty(p.txtKeyword))
            {
                var keyword = $"%{p.txtKeyword}%"; // SQL LIKE 需要使用 % 作為萬用字元
                購物車 = 購物車.Where(c =>
                    EF.Functions.Like(c.Category.ToString(), keyword) ||
                    EF.Functions.Like(c.Status.ToString(), keyword));
            }
            if (!string.IsNullOrEmpty(p.txtKeyword))
            {
                var keyword = $"%{p.txtKeyword}%"; // SQL Like 用的萬用字元
                購物車 = 購物車.Where(c =>
                    EF.Functions.Like(c.Category.ToString(), keyword) ||
                    EF.Functions.Like(c.Member.Name, keyword) ||
                    EF.Functions.Like(c.Status.ToString(), keyword) || // 檢查狀態
                    EF.Functions.Like(c.Category.ToString(), keyword) // <--- 加入對 Category 的檢查
                );
            }

            p.Carts = await 購物車.ToListAsync();
            return View(p);
        }


        // GET: Carts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
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
