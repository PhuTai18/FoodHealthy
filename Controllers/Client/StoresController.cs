using ITHealthy.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ITHealthy.Controllers
{
    public class StoresController : Controller
    {
        private readonly ITHealthyDbContext _context;

        public StoresController(ITHealthyDbContext context)
        {
            _context = context;
        }

        // 🔥 /Stores
        public async Task<IActionResult> Index(string? keyword)
        {
            var query = _context.Stores.AsQueryable();

            // 🔍 Search theo city / district / name
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(s =>
                    s.StoreName.Contains(keyword) ||
                    s.City.Contains(keyword) ||
                    s.District.Contains(keyword));
            }

            var stores = await query
                .OrderBy(s => s.City)
                .ThenBy(s => s.District)
                .ToListAsync();

            return View("~/Views/Client/Stores/Index.cshtml",stores);
        }

        // 🔥 /Stores/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var store = await _context.Stores
                .FirstOrDefaultAsync(s => s.StoreId == id);

            if (store == null)
                return NotFound();

            return View(store);
        }
    }
}