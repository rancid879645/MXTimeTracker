using Moq;
using Microsoft.Extensions.Configuration;
using MotocrossTracker.API.Domain.Entities;
using MotocrossTracker.API.Infrastructure.Auth;

namespace MotocrossTracker.UnitTests.Auth;

public class TokenServiceTests
{
    private readonly TokenService _tokenService;

    public TokenServiceTests()
    {
        var configMock = new Mock<IConfiguration>();
        configMock.Setup(c => c["Jwt:Key"]).Returns("test-secret-key-that-is-at-least-32-chars!!");
        configMock.Setup(c => c["Jwt:Issuer"]).Returns("test-issuer");
        configMock.Setup(c => c["Jwt:Audience"]).Returns("test-audience");

        _tokenService = new TokenService(configMock.Object);
    }

    [Fact]
    public void GenerateToken_WithValidUser_ShouldReturnNonEmptyString()
    {
        var user = new User { Id = "user-1", Username = "jorge", Email = "rider@mxgp.com" };

        var token = _tokenService.GenerateToken(user);

        Assert.False(string.IsNullOrEmpty(token));
    }

    [Fact]
    public void GenerateToken_WithValidUser_ShouldReturnValidJwtFormat()
    {
        var user = new User { Id = "user-1", Username = "jorge", Email = "rider@mxgp.com" };

        var token = _tokenService.GenerateToken(user);
        var parts = token.Split('.');

        Assert.Equal(3, parts.Length);
    }
}
