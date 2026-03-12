using ITHealthy.Data;
using ITHealthy.DTOs;
using ITHealthy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ITHealthy.Controllers
{

    public class CategoryController : Controller
    {
        private readonly ITHealthyDbContext _context;

        public CategoryController(ITHealthyDbContext context)
        {
            _context = context;
        }

        // all Category Product
        public async Task<ActionResult> AllCategoryPro()
        {
            var category_pro = await _context.Categories
                .Select(p => new CategoryProDTO
                {
                    CategoryId = p.CategoryId,
                    CategoryName = p.CategoryName,
                    DescriptionCat = p.DescriptionCat,
                    ImageCategories = p.ImageCategories
                })
                .ToListAsync();

            return View(category_pro);
        }




        // all Category Ingredient

        public async Task<ActionResult> AllCategoryIng()
        {
            var category_ing = await _context.CategoriesIngredients
                .Select(p => new CategoryIngDTO
                {
                    CategoriesIngredientsId = p.CategoriesIngredientsId,
                    CategoryName = p.CategoryName,
                    DescriptionCat = p.DescriptionCat
                })
                .ToListAsync();
            return View(category_ing);
        }


        // Get Details Category Product by Id
        public async Task<IActionResult> GetByIdCategoryPro(int id)
        {
            var catePro = await _context.Categories.FindAsync(id);
            if (catePro == null)
            {
                TempData["Error"] = "Không tìm thấy loại sản phẩm.";
                return RedirectToAction("AllCategoryPro");
            }
            return View(catePro);
        }

        // Get Details Category Ingredient by Id
        public async Task<IActionResult> GetByIdCategoryIng(int id)
        {
            var cateIng = await _context.CategoriesIngredients.FindAsync(id);
            if (cateIng == null)
            {
                TempData["Error"] = "Không tìm thấy loại nguyên liệu.";
                return RedirectToAction("AllCategoryIng");
            }
            return View(cateIng);
        }

        // CREATE Category Product
        public IActionResult CreateCategoryPro()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateCatePro(Category catePro)
        {
            if (!ModelState.IsValid)
                return View(catePro);

            _context.Categories.Add(catePro);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Tạo loại sản phẩm thành công";
            return RedirectToAction("AllCategoryPro");
        }

        [HttpPost]
        public async Task<IActionResult> CreateCateIng(CategoriesIngredient cateIng)
        {
            if (!ModelState.IsValid)
                return View(cateIng);

            _context.CategoriesIngredients.Add(cateIng);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Tạo loại nguyên liệu thành công";
            return RedirectToAction("AllCategoryIng");
        }

        // UPDATE Category Product
        public IActionResult UpdateCategoryPro(int id)
        {
            var catePro = _context.Categories.Find(id);
            if (catePro == null)
            {
                TempData["Error"] = "Không tìm thấy loại sản phẩm.";
                return RedirectToAction("AllCategoryPro");
            }
            return View(catePro);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCatePro(int id, Category catePro)
        {
            var categoryPro = await _context.Categories.FindAsync(id);
            if (categoryPro == null)
            {
                TempData["Error"] = "Không tìm thấy loại sản phẩm.";
                return RedirectToAction("AllCategoryPro");
            }

            categoryPro.CategoryName = catePro.CategoryName;
            categoryPro.DescriptionCat = catePro.DescriptionCat;

            await _context.SaveChangesAsync();
            TempData["Success"] = "Cập nhật loại sản phẩm thành công!";
            return RedirectToAction("AllCategoryPro");

        }
        // UPDATE Category Ingredient
        public async Task<IActionResult> UpdateCategoryIng(int id)
        {
            var cateIng = _context.CategoriesIngredients.Find(id);
            if (cateIng == null)
            {
                TempData["Error"] = "Không tìm thấy loại nguyên liệu.";
                return RedirectToAction("AllCategoryIng");
            }
            return View(cateIng);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCateIng(int id, CategoriesIngredient cateIng)
        {
            var categoryIng = await _context.CategoriesIngredients.FindAsync(id);
            if (categoryIng == null)
            {
                TempData["Error"] = "Không tìm thấy loại nguyên liệu.";
                return RedirectToAction("AllCategoryIng");
            }

            categoryIng.CategoryName = cateIng.CategoryName;
            categoryIng.DescriptionCat = cateIng.DescriptionCat;

            await _context.SaveChangesAsync();
            TempData["Success"] = "Cập nhật loại nguyên liệu thành công!";
            return RedirectToAction("AllCategoryIng");

        }


        // DELETE Category Product
        public async Task<IActionResult> DeleteCategoryPro(int id)
        {
            var catePro = _context.Categories.Find(id);
            if (catePro == null)
            {
                TempData["Error"] = "Không tìm thấy loại sản phẩm.";
                return RedirectToAction("AllCategoryPro");
            }
            return View(catePro);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCatePro(int id)
        {
            var catePro = await _context.Categories.FindAsync(id);
            if (catePro == null)
            {
                TempData["Error"] = "Không tìm thấy loại sản phẩm.";
                return RedirectToAction("AllCategoryPro");
            }
            _context.Categories.Remove(catePro);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Đã xóa loại sản phẩm thành công!";
            return RedirectToAction("AllCategoryPro");
        }

        // DELETE Category Ingredient
        public async Task<IActionResult> DeleteCategoryIng(int id)
        {
            var cateIng = _context.CategoriesIngredients.Find(id);
            if (cateIng == null)
            {
                TempData["Error"] = "Không tìm thấy loại nguyên liệu.";
                return RedirectToAction("AllCategoryIng");
            }
            return View(cateIng);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCateIng(int id)
        {
            var cateIng = await _context.CategoriesIngredients.FindAsync(id);
            if (cateIng == null)
            {
                TempData["Error"] = "Không tìm thấy loại nguyên liệu.";
                return RedirectToAction("AllCategoryIng");
            }

            _context.CategoriesIngredients.Remove(cateIng);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Đã xóa loại nguyên liệu thành công!";
            return RedirectToAction("AllCategoryIng");
        }
    }
}