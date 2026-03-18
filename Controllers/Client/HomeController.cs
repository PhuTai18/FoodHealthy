using ITHealthy.Data;
using ITHealthy.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ITHealthy.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITHealthyDbContext _context;

        public HomeController(ITHealthyDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _context.Products
                .AsNoTracking() // 🔥 tăng performance
                .Where(p => p.IsAvailable == true)
                .OrderByDescending(p => p.CreatedAt)
                .Take(20)
                .Select(p => new ProductDTO
                {
                    ProductId = p.ProductId,              
                    CategoryId = p.CategoryId,
                    ProductName = p.ProductName,
                    DescriptionProduct = p.DescriptionProduct,
                    BasePrice = p.BasePrice,
                    Calories = p.Calories,
                    Protein = p.Protein,
                    Carbs = p.Carbs,
                    Fat = p.Fat,
                    ImageProduct = p.ImageProduct,

                    // optional
                    CategoryName = p.Category != null 
                        ? p.Category.CategoryName 
                        : null
                })
                .ToListAsync();

            return View("~/Views/Client/Home/Index.cshtml", products);
        }
    }
}