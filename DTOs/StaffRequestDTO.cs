using Microsoft.AspNetCore.Http;

namespace ITHealthy.DTOs
{
    public class StaffRequestDTO
    {
        public int? StaffId { get; set; }

        public int? StoreId { get; set; }

        public string FullName { get; set; } = null!;

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public string? PasswordHash { get; set; }

        public string? Gender { get; set; }

        public DateOnly? Dob { get; set; }

        public string? RoleStaff { get; set; }

        public DateOnly? HireDate { get; set; }

        public bool? IsActive { get; set; }

        public IFormFile? Avatar { get; set; }
    }
}