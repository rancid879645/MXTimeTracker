using MotocrossTracker.API.Application.DTOs;
using MotocrossTracker.API.Application.Interfaces;
using MotocrossTracker.API.Domain.Entities;
using MotocrossTracker.API.Domain.Interfaces;
using MotocrossTracker.API.Infrastructure.Auth;

namespace MotocrossTracker.API.Application.Services;

public class UserService(IUserRepository repository, TokenService tokenService) : IUserService
{
    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Username))
            throw new ArgumentException("Username is required.");

        if (string.IsNullOrWhiteSpace(request.Password))
            throw new ArgumentException("Password is required.");

        if (string.IsNullOrWhiteSpace(request.Email))
            throw new ArgumentException("Email is required.");

        var existing = await repository.GetByUsernameAsync(request.Username);
        if (existing is not null)
            throw new InvalidOperationException("Username is already taken.");

        var user = new User
        {
            Id = Guid.NewGuid().ToString(),
            Username = request.Username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Email = request.Email
        };

        await repository.CreateAsync(user);

        return new AuthResponse
        {
            Token = tokenService.GenerateToken(user),
            UserId = user.Id,
            Username = user.Username
        };
    }

    public async Task<AuthResponse?> LoginAsync(LoginRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
            return null;

        var user = await repository.GetByUsernameAsync(request.Username);

        if (user is null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            return null;

        return new AuthResponse
        {
            Token = tokenService.GenerateToken(user),
            UserId = user.Id,
            Username = user.Username
        };
    }
}
