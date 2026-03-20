using ITHealthy.Data;
using ITHealthy.Models;
using Microsoft.EntityFrameworkCore;

namespace ITHealthy.Services
{
    public class StaffService
    {
        private readonly ITHealthyDbContext _context;

        public StaffService(ITHealthyDbContext context)
        {
            _context = context;
        }

        public async Task<List<Staff>> GetAllAsync()
        {
            return await _context.Staff.ToListAsync();
        }

        public async Task<Staff?> GetByIdAsync(int id)
        {
            return await _context.Staff.FindAsync(id);
        }

        public async Task CreateAsync(Staff staff)
        {
            _context.Staff.Add(staff);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Staff staff)
        {
            _context.Staff.Update(staff);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var staff = await _context.Staff.FindAsync(id);
            if (staff != null)
            {
                _context.Staff.Remove(staff);
                await _context.SaveChangesAsync();
            }
        }
    }
}