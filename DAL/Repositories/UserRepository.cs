using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _context.Users
            .Include(x => x.UserGroup)
            .Include(x => x.UserState)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task Create(User entity)
    {
        await _context.Users.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Update(User entity)
    {
        _context.Users.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(User entity)
    {
        _context.Users.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        return await _context.Users.Include(x => x.UserGroup).Include(x => x.UserState).ToListAsync();
    }

    public async Task<IEnumerable<User>> GetUsersAsync(PaginationParameters paginationParameters)
    {
        return await _context.Users
            .Include(x => x.UserGroup).Include(x => x.UserState)
            .Skip((paginationParameters.PageNumber - 1) * paginationParameters.PageSize)
            .Take(paginationParameters.PageSize)
            .ToListAsync();
    }

    public async Task<bool> AdminIsExist()
    {
        return await _context.Users.AnyAsync(x => x.UserGroup.Code == Group.Admin);
    }

    public async Task<bool> LoginIsUsed(string login)
    {
        return await _context.Users.AnyAsync(x => x.Login == login);
    }
}