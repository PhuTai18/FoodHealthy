using ITHealthy.Data;
using ITHealthy.DTOs;
using ITHealthy.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ITHealthy.Controllers
{
    [Route("products")]
    public class ProductsController : Controller
    {
        private readonly ITHealthyDbContext _context;
        private readonly CloudinaryService _cloudinaryService;

        public ProductsController(ITHealthyDbContext context, CloudinaryService cloudinaryService)
        {
            _context = context;
            _cloudinaryService = cloudinaryService;
        }

        // 🔥 /products
        [HttpGet("")]
        public async Task<IActionResult> AllProducts(int? categoryId)
        {
            // categories
            ViewBag.Categories = await _context.Categories.ToListAsync();

            var query = _context.Products
                .Include(p => p.Category)
                .AsQueryable();

            // filter
            if (categoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == categoryId.Value);
            }

            // DTO
            var products = await query.Select(p => new ProductDTO
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                BasePrice = p.BasePrice,
                ImageProduct = p.ImageProduct,
                DescriptionProduct = p.DescriptionProduct,
                CategoryName = p.Category != null ? p.Category.CategoryName : null,
                CategoryId = p.CategoryId,
                Calories = p.Calories,
                Protein = p.Protein,
                Carbs = p.Carbs,
                Fat = p.Fat
            }).ToListAsync();

            return View("~/Views/Client/Products/AllProducts.cshtml",products);
        }

        // 🔥 /products/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .Where(p => p.ProductId == id)
                .Select(p => new ProductDTO
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    BasePrice = p.BasePrice,
                    ImageProduct = p.ImageProduct,
                    DescriptionProduct = p.DescriptionProduct,
                    CategoryName = p.Category != null ? p.Category.CategoryName : null,
                    Calories = p.Calories,
                    Protein = p.Protein,
                    Carbs = p.Carbs,
                    Fat = p.Fat
                })
                .FirstOrDefaultAsync();

            if (product == null)
            {
                return RedirectToAction(nameof(AllProducts));
            }

            //return View("~/Views/Client/Products/AllProducts.cshtml",products);
            return View("~/Views/Client/Products/GetProductById.cshtml", product); // 👉 Views/Products/GetProductById.cshtml
        }
    }
}