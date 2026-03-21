using ITHealthy.Data;
using ITHealthy.DTOs;
using ITHealthy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ITHealthy.Controllers
{
    [Route("addresses")]
    public class CustomerAddressesController : Controller
    {
        private readonly ITHealthyDbContext _context;

        public CustomerAddressesController(ITHealthyDbContext context)
        {
            _context = context;
        }

        private int GetCurrentUserId()
        {
            var claim = User.FindFirst("CustomerId");
            return int.Parse(claim.Value);
        }

        // Lấy tất cả địa chỉ của tất cả khách hàng (dành cho admin)
        [HttpGet("all")]
        public async Task<IActionResult> GetAllAddresses()
        {
            var addresses = await _context.CustomerAddresses
                .Include(a => a.Customer)
                .Select(a => new CustomerAddressDTO
                {
                    AddressId = a.AddressId,
                    CustomerId = a.CustomerId,
                    ReceiverName = a.ReceiverName,
                    PhoneNumber = a.PhoneNumber,
                    StreetAddress = a.StreetAddress,
                    Ward = a.Ward,
                    District = a.District,
                    City = a.City,
                    Country = a.Country,
                    Postcode = a.Postcode,
                    Latitude = a.Latitude,
                    Longitude = a.Longitude,
                    GooglePlaceId = a.GooglePlaceId,
                    AddressType = a.AddressType,
                    IsDefault = a.IsDefault,
                    CreatedAt = a.CreatedAt,
                    UpdatedAt = a.UpdatedAt,
                    CustomerName = a.Customer.FullName
                })
                .ToListAsync();

            return View(addresses);
        }

        // Lấy địa chỉ theo ID
        public async Task<IActionResult> GetAddressById(int id)
        {

            var address = await _context.CustomerAddresses
                .Include(a => a.Customer)
                .Where(a => a.AddressId == id)
                .Select(a => new CustomerAddressDTO
                {
                    AddressId = a.AddressId,
                    CustomerId = a.CustomerId,
                    ReceiverName = a.ReceiverName,
                    PhoneNumber = a.PhoneNumber,
                    StreetAddress = a.StreetAddress,
                    Ward = a.Ward,
                    District = a.District,
                    City = a.City,
                    Country = a.Country,
                    Postcode = a.Postcode,
                    Latitude = a.Latitude,
                    Longitude = a.Longitude,
                    GooglePlaceId = a.GooglePlaceId,
                    AddressType = a.AddressType,
                    IsDefault = a.IsDefault,
                    CreatedAt = a.CreatedAt,
                    UpdatedAt = a.UpdatedAt,
                    CustomerName = a.Customer.FullName
                })
                .FirstOrDefaultAsync();

            if (address == null)
                return NotFound(new { message = "Không tìm thấy địa chỉ." });

            return View(address);
        }

        // Lấy địa chỉ theo ID khách hàng
        public async Task<IActionResult> GetAddressesByCustomer()
        {

            var customerId = GetCurrentUserId();

            var addresses = await _context.CustomerAddresses
                .Where(a => a.CustomerId == customerId)
                .Select(a => new CustomerAddressDTO
                {
                    AddressId = a.AddressId,
                    CustomerId = a.CustomerId,
                    ReceiverName = a.ReceiverName,
                    PhoneNumber = a.PhoneNumber,
                    StreetAddress = a.StreetAddress,
                    Ward = a.Ward,
                    District = a.District,
                    City = a.City,
                    Country = a.Country,
                    Postcode = a.Postcode,
                    AddressType = a.AddressType,
                    IsDefault = a.IsDefault
                })
                .ToListAsync();

            if (!addresses.Any())
            {
                TempData["error"] = "Bạn chưa có địa chỉ nào. Vui lòng thêm địa chỉ để tiếp tục.";
                return RedirectToAction(nameof(GetAllAddresses));
            }

            return View(addresses);
        }

        public IActionResult AddAddress()
        {
            return View();
        }

        // Thêm địa chỉ mới
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAddress(CustomerAddressDTO request)
        {
            var customerId = GetCurrentUserId();
            if (string.IsNullOrEmpty(request.ReceiverName) || string.IsNullOrEmpty(request.PhoneNumber))
            {
                ModelState.AddModelError("", "Tên và SĐT là bắt buộc");
                return View(request);
            }

            var parts = new List<string>
            {
                request.StreetAddress,
                request.Ward,
                request.District,
                request.City,
                request.Country
            }.Where(p => !string.IsNullOrEmpty(p)).ToList();

            string fullAddress = string.Join(", ", parts);
            if (string.IsNullOrEmpty(fullAddress))
            {
                ModelState.AddModelError("", "Địa chỉ không hợp lệ.");
                return View(request);
            }

            // Tự động lấy tọa độ (miễn phí 100% - haochaun.io)YY
            var coordinates = await GeocodingService.GetCoordinatesAsync(fullAddress);

            var address = new CustomerAddress
            {
                CustomerId = customerId,
                ReceiverName = request.ReceiverName,
                PhoneNumber = request.PhoneNumber,
                StreetAddress = request.StreetAddress,
                Ward = request.Ward,
                District = request.District,
                City = request.City,
                Country = request.Country,
                Postcode = request.Postcode,
                Latitude = request.Latitude,
                Longitude = request.Longitude,
                GooglePlaceId = request.GooglePlaceId,
                AddressType = request.AddressType,
                IsDefault = request.IsDefault ?? false,
                CreatedAt = DateTime.Now
            };

            _context.CustomerAddresses.Add(address);
            await _context.SaveChangesAsync();
            TempData["success"] = "Đã thêm địa chỉ thành công.";
            return RedirectToAction(nameof(GetAddressesByCustomer));
        }


        // Chinh sửa địa chỉ
        public async Task<IActionResult> Edit(int id)
        {
            var address = await _context.CustomerAddresses.FindAsync(id);
            if (address == null)
                return NotFound();

            var dto = new CustomerAddressDTO
            {
                AddressId = address.AddressId,
                ReceiverName = address.ReceiverName,
                PhoneNumber = address.PhoneNumber,
                StreetAddress = address.StreetAddress,
                Ward = address.Ward,
                District = address.District,
                City = address.City,
                Country = address.Country,
                Postcode = address.Postcode,
                Latitude = address.Latitude,
                Longitude = address.Longitude,
                GooglePlaceId = address.GooglePlaceId,
                AddressType = address.AddressType,
                IsDefault = address.IsDefault
            };

            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAddress(int id, CustomerAddressDTO request)
        {

            var address = await _context.CustomerAddresses.FindAsync(id);
            if (address == null)
            {
                TempData["error"] = "Không tìm thấy địa chỉ.";
                return RedirectToAction(nameof(GetAddressesByCustomer));
            }

            address.ReceiverName = request.ReceiverName;
            address.PhoneNumber = request.PhoneNumber;
            address.StreetAddress = request.StreetAddress;
            address.Ward = request.Ward;
            address.District = request.District;
            address.City = request.City;
            address.Country = request.Country;
            address.Postcode = request.Postcode;
            address.Latitude = request.Latitude;
            address.Longitude = request.Longitude;
            address.GooglePlaceId = request.GooglePlaceId;
            address.AddressType = request.AddressType;
            address.IsDefault = request.IsDefault;
            address.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            TempData["success"] = "Cập nhật địa chỉ thành công.";
            return RedirectToAction(nameof(GetAddressesByCustomer));

        }

        // Xoa địa chỉ

        public async Task<IActionResult> Delete(int id)
        {
            var address = await _context.CustomerAddresses.FindAsync(id);
            if (address == null)
                return NotFound();

            return View(address);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var address = await _context.CustomerAddresses.FindAsync(id);
            if (address == null)
                return NotFound();
            _context.CustomerAddresses.Remove(address);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(GetAllAddresses));
        }



    }
}
