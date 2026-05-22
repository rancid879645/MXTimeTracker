using MotocrossTracker.API.Application.DTOs;

namespace MotocrossTracker.API.Application.Interfaces;

public interface IUserService
{
    Task<AuthResponse> RegisterAsync(RegisterRequest request);
    Task<AuthResponse?> LoginAsync(LoginRequest request);
}
