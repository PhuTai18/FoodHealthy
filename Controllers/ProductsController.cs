using ITHealthy.Data;
using ITHealthy.DTOs;
using ITHealthy.Models;
using ITHealthy.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ITHealthy.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ITHealthyDbContext _context;
        private readonly CloudinaryService _cloudinaryService;

        public ProductsController(ITHealthyDbContext context, CloudinaryService cloudinaryService)
        {
            _context = context;
            _cloudinaryService = cloudinaryService;
        }

        // 🔹 GET All Product 
        public async Task<ActionResult> AllProducts(int? categoryId)
        {
            // Lấy danh sách category
            var categories = await _context.Categories.ToListAsync();
            ViewBag.Categories = categories;

            // Query sản phẩm
            var query = _context.Products
                .Include(p => p.Category)
                .AsQueryable();

            // Filter category
            if (categoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == categoryId.Value);
            }

            // Lấy danh sách product từ query
            var products = await query
                        .Include(p => p.Category)
                .Select(p => new ProductDTO
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    BasePrice = p.BasePrice,
                    IsAvailable = p.IsAvailable,
                    DescriptionProduct = p.DescriptionProduct,
                    ImageProduct = p.ImageProduct,
                    CategoryName = p.Category != null ? p.Category.CategoryName : null,
                    CategoryId = p.CategoryId,
                    Calories = p.Calories,
                    Protein = p.Protein,
                    Carbs = p.Carbs,
                    Fat = p.Fat
                })
                .ToListAsync();

            return View(products);
        }

        // 🔹 GET Detail Product 

        public async Task<ActionResult> GetProductById(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .Where(p => p.ProductId == id)
                .Select(p => new ProductDTO
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    BasePrice = p.BasePrice,
                    IsAvailable = p.IsAvailable,
                    DescriptionProduct = p.DescriptionProduct,
                    ImageProduct = p.ImageProduct,
                    CategoryName = p.Category != null ? p.Category.CategoryName : null,
                    Calories = p.Calories,
                    Protein = p.Protein,
                    Carbs = p.Carbs,
                    Fat = p.Fat

                })
                .FirstOrDefaultAsync();

            if (product == null)
            {
                TempData["Error"] = "Không tìm thấy sản phẩm";
                return RedirectToAction("AllProducts");
            }
            return View(product);
        }

        // CREATE Product


        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductDTO request)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Dữ liệu không hợp lệ. Vui lòng kiểm tra lại.";
                return View(request);
            }
            try
            {
                string? imageUrl = null;

                if (request.ImageFile != null && request.ImageFile.Length > 0)
                {
                    // ✅ Upload lên Cloudinary, lưu ảnh vào folder products_images
                    imageUrl = await _cloudinaryService.UploadImageAsync(request.ImageFile);
                }

                var product = new Product
                {
                    ProductName = request.ProductName,
                    DescriptionProduct = request.DescriptionProduct,
                    BasePrice = request.BasePrice,
                    Calories = request.Calories,
                    Protein = request.Protein,
                    Carbs = request.Carbs,
                    Fat = request.Fat,
                    ImageProduct = imageUrl,
                    CategoryId = request.CategoryId,
                    IsAvailable = request.IsAvailable ?? true,
                    CreatedAt = DateTime.Now
                };

                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                return RedirectToAction("AllProducts");
            }
            catch (Exception)
            {
                TempData["Error"] = "Có lỗi xảy ra khi thêm sản phẩm.";
                return RedirectToAction("AllProducts");
            }
        }


        // EDIT Product
        public async Task<IActionResult> UpdateProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                TempData["Error"] = "Không tìm thấy sản phẩm.";
                return RedirectToAction("AllProducts");
            }
            var productDto = new ProductDTO
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                DescriptionProduct = product.DescriptionProduct,
                BasePrice = product.BasePrice,
                Calories = product.Calories,
                Protein = product.Protein,
                Carbs = product.Carbs,
                Fat = product.Fat,
                ImageProduct = product.ImageProduct,
                CategoryId = product.CategoryId,
                IsAvailable = product.IsAvailable
            };
            return View(productDto);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(int id, ProductDTO request)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                TempData["Error"] = "Không tìm thấy sản phẩm.";
                return RedirectToAction("AllProducts");
            }
            try
            {

                if (request.ImageFile != null && request.ImageFile.Length > 0)
                {
                    // ✅ Upload ảnh mới lên Cloudinary
                    var imageUrl = await _cloudinaryService.UploadImageProductAsync(request.ImageFile);
                    product.ImageProduct = imageUrl;
                }

                // Cập nhật thông tin
                product.ProductName = request.ProductName;
                product.DescriptionProduct = request.DescriptionProduct;
                product.BasePrice = request.BasePrice;
                product.Calories = request.Calories;
                product.Protein = request.Protein;
                product.Carbs = request.Carbs;
                product.Fat = request.Fat;
                product.CategoryId = request.CategoryId;
                product.IsAvailable = request.IsAvailable;

                await _context.SaveChangesAsync();

                TempData["Success"] = "Cập nhật sản phẩm thành công.";
                return RedirectToAction("AllProducts");


            }
            catch (Exception)
            {
                TempData["Error"] = "Có lỗi xảy ra khi cập nhật sản phẩm.";
                return RedirectToAction("AllProducts");
            }
        }

        //DELETE Product
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                TempData["Error"] = "Không tìm thấy sản phẩm.";
                return RedirectToAction("AllProducts");
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                TempData["Error"] = "Không tìm thấy sản phẩm.";
                return RedirectToAction("AllProducts");
            }
            try
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Xóa sản phẩm thành công.";
                return RedirectToAction("AllProducts");
            }
            catch (Exception)
            {
                TempData["Error"] = "Có lỗi xảy ra khi xóa sản phẩm.";
                return RedirectToAction("AllProducts");
            }

        }



    }
}