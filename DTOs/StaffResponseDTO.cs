namespace ITHealthy.DTOs
{
    public class StaffResponseDTO
    {
        public int StaffId { get; set; }

        public string FullName { get; set; } = null!;

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? Role { get; set; }

        public bool IsActive { get; set; }

        public string? Avatar { get; set; }

        public string? StoreName { get; set; }

        public int? StoreId { get; set; }
    }
}