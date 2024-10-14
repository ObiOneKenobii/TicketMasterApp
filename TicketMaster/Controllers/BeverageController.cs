using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketMaster.Models;
using System.Threading.Tasks;
using TicketMaster.Data;

namespace TicketMaster.Controllers
{
    public class BeveragesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BeveragesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Beverages
        public async Task<IActionResult> Index()
        {
            var beverages = await _context.Beverages.ToListAsync();
            return View(beverages);
        }

        // GET: Beverages/Create
        public IActionResult Create() => View();

        // POST: Beverages/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Beverage beverage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(beverage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(beverage);
        }

        // GET: Beverages/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var beverage = await _context.Beverages.FindAsync(id);
            if (beverage == null) return NotFound();
            return View(beverage);
        }

        // POST: Beverages/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Beverage beverage)
        {
            if (id != beverage.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(beverage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(beverage);
        }

        // GET: Beverages/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var beverage = await _context.Beverages.FindAsync(id);
            if (beverage == null) return NotFound();
            return View(beverage);
        }

        // POST: Beverages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var beverage = await _context.Beverages.FindAsync(id);
            if (beverage != null)
            {
                _context.Beverages.Remove(beverage);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
