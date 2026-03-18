using ITHealthy.DTOs;

namespace ITHealthy.Services.Admin
{
    public interface IStaffService
    {
        // =========================
        // GET LIST (có StoreName)
        // =========================
        Task<List<StaffResponseDTO>> GetAllAsync();

        // =========================
        // GET DETAIL
        // =========================
        Task<StaffResponseDTO?> GetByIdAsync(int id);

        // =========================
        // CREATE
        // =========================
        Task<(bool Success, string Message)> CreateAsync(StaffRequestDTO request);

        // =========================
        // UPDATE
        // =========================
        Task<(bool Success, string Message)> UpdateAsync(int id, StaffRequestDTO request);

        // =========================
        // DELETE
        // =========================
        Task<(bool Success, string Message)> DeleteAsync(int id);
    }
}