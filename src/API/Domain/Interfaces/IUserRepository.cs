using MotocrossTracker.API.Domain.Entities;

namespace MotocrossTracker.API.Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(string id);
    Task<User?> GetByUsernameAsync(string username);
    Task<string> CreateAsync(User user);
}
