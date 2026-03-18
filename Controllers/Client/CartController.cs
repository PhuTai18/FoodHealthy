using ITHealthy.Data;
using ITHealthy.DTOs;
using ITHealthy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ITHealthy.Controllers
{
    [Route("carts")]
    public class CartController : Controller
    {
        private readonly ITHealthyDbContext _context;

        public CartController(ITHealthyDbContext context)
        {
            _context = context;
        }

        // Lấy CustomerId từ session, null nếu chưa login
        private int? GetSessionCustomerId()
        {
            return HttpContext.Session.GetInt32("CustomerId");
        }

        // Hiển thị giỏ hàng của user
        [HttpGet("")]
        public async Task<IActionResult> GetCartByCustomer()
        {
            var customerId = GetSessionCustomerId();
            if (customerId == null)
            {
                TempData["error"] = "Bạn chưa đăng nhập.";
                return RedirectToAction("Login", "Auth");
            }

            var cart = await _context.Carts
                .Include(c => c.CartItems)
                    .ThenInclude(i => i.Product)
                .Include(c => c.CartItems)
                    .ThenInclude(i => i.Combo)
                .Include(c => c.CartItems)
                    .ThenInclude(i => i.Bowl)
                .FirstOrDefaultAsync(c => c.CustomerId == customerId.Value);

            if (cart == null)
            {
                TempData["error"] = "Giỏ hàng trống.";
                return RedirectToAction("Index", "Home");
            }

            var totalPrice = cart.CartItems.Sum(i => (i.UnitPrice ?? 0) * i.Quantity);

            var result = new CartViewModel
            {
                CartId = cart.CartId,
                CustomerId = cart.CustomerId,
                TotalPrice = totalPrice,
                Items = cart.CartItems.Select(i => new CartItemViewModel
                {
                    CartItemId = i.CartItemId,
                    ProductId = i.ProductId,
                    ProductName = i.Product?.ProductName,
                    ImageProduct = i.Product?.ImageProduct,
                    DescriptionProduct = i.Product?.DescriptionProduct,
                    ComboId = i.ComboId,
                    ComboName = i.Combo?.ComboName,
                    BowlId = i.BowlId,
                    BowlName = i.Bowl?.BowlName,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    SubTotal = (i.UnitPrice ?? 0) * i.Quantity
                }).ToList()
            };

            TempData["success"] = "Đã lấy giỏ hàng thành công.";
            return View("~/Views/Client/Cart/GetCartByCustomer.cshtml", result);
        }

        // Thêm sản phẩm vào giỏ
        [HttpPost("add")]
        public async Task<IActionResult> AddToCart(AddToCartDto dto)
        {
            var customerId = GetSessionCustomerId();
            if (customerId == null)
            {
                TempData["error"] = "Bạn chưa đăng nhập.";
                return RedirectToAction("Login", "Auth");
            }

            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.CustomerId == customerId.Value);

            if (cart == null)
            {
                cart = new Cart
                {
                    CustomerId = customerId.Value,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
            }

            var existingItem = cart.CartItems.FirstOrDefault(i =>
                i.ProductId == dto.ProductId &&
                i.ComboId == dto.ComboId &&
                i.BowlId == dto.BowlId
            );

            if (existingItem != null)
            {
                existingItem.Quantity += dto.Quantity;
                existingItem.UnitPrice = dto.UnitPrice ?? existingItem.UnitPrice;
                existingItem.AddedAt = DateTime.Now;
            }
            else
            {
                _context.CartItems.Add(new CartItem
                {
                    CartId = cart.CartId,
                    ProductId = dto.ProductId,
                    ComboId = dto.ComboId,
                    BowlId = dto.BowlId,
                    Quantity = dto.Quantity,
                    UnitPrice = dto.UnitPrice,
                    AddedAt = DateTime.Now
                });
            }

            cart.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();

            TempData["success"] = "Đã thêm sản phẩm vào giỏ hàng.";
            return RedirectToAction("GetCartByCustomer");
        }

        // Cập nhật số lượng sản phẩm
        [HttpPost("update/{cartItemId}")]
        public async Task<IActionResult> UpdateQuantity(int cartItemId, UpdateCartItemDto dto)
        {
            var customerId = GetSessionCustomerId();
            if (customerId == null)
            {
                TempData["error"] = "Bạn chưa đăng nhập.";
                return RedirectToAction("Login", "Auth");
            }

            var item = await _context.CartItems
                .Include(i => i.Cart)
                .FirstOrDefaultAsync(i => i.CartItemId == cartItemId && i.Cart.CustomerId == customerId.Value);

            if (item == null)
            {
                TempData["error"] = "Không tìm thấy sản phẩm trong giỏ hàng.";
                return RedirectToAction("GetCartByCustomer");
            }

            if (dto.Quantity <= 0)
            {
                TempData["error"] = "Số lượng phải lớn hơn 0.";
                return RedirectToAction("GetCartByCustomer");
            }

            item.Quantity = dto.Quantity;
            item.AddedAt = DateTime.Now;
            item.Cart.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            TempData["success"] = "Cập nhật số lượng thành công.";
            return RedirectToAction("GetCartByCustomer");
        }

        // Xoá sản phẩm khỏi giỏ
        [HttpPost("remove/{cartItemId}")]
        public async Task<IActionResult> RemoveFromCart(int cartItemId)
        {
            var customerId = GetSessionCustomerId();
            if (customerId == null)
            {
                TempData["error"] = "Bạn chưa đăng nhập.";
                return RedirectToAction("Login", "Auth");
            }

            var item = await _context.CartItems
                .Include(i => i.Cart)
                .FirstOrDefaultAsync(i => i.CartItemId == cartItemId && i.Cart.CustomerId == customerId.Value);

            if (item == null)
            {
                TempData["error"] = "Không tìm thấy sản phẩm trong giỏ hàng.";
                return RedirectToAction("GetCartByCustomer");
            }

            _context.CartItems.Remove(item);
            item.Cart.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            TempData["success"] = "Đã xoá sản phẩm khỏi giỏ hàng.";
            return RedirectToAction("GetCartByCustomer");
        }
    }

    public class UpdateCartItemDto
    {
        public int Quantity { get; set; }
    }
}