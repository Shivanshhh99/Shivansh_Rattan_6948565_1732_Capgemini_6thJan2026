using Microsoft.EntityFrameworkCore;
using SmartHospital.API.Data;
using SmartHospital.API.Models;
using SmartHospital.API.Repositories.Interfaces;

namespace SmartHospital.API.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(HospitalDbContext context) : base(context) { }

    public async Task<User?> GetByEmailAsync(string email) =>
        await _dbSet.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());

    public async Task<IEnumerable<User>> GetByRoleAsync(string role) =>
        await _dbSet.Where(u => u.Role == role).ToListAsync();
}
