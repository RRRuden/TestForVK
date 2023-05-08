using Domain.Entities;

namespace Domain.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<IEnumerable<User>> GetUsersAsync();
    Task<IEnumerable<User>> GetUsersAsync(PaginationParameters  paginationParameters);
    Task<bool> AdminIsExist();
    Task<bool> LoginIsUsed(string login);
}