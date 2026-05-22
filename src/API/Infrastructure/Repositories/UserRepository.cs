using System.Data;
using Microsoft.Data.SqlClient;
using MotocrossTracker.API.Domain.Entities;
using MotocrossTracker.API.Domain.Interfaces;

namespace MotocrossTracker.API.Infrastructure.Repositories;

public class UserRepository(IDbConnectionFactory factory) : IUserRepository
{
    public async Task<User?> GetByIdAsync(string id)
    {
        await using var conn = (SqlConnection)factory.CreateConnection();
        await using var cmd = new SqlCommand("sp_GetUserById", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Id", id);

        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();

        return await reader.ReadAsync() ? MapUser(reader) : null;
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        await using var conn = (SqlConnection)factory.CreateConnection();
        await using var cmd = new SqlCommand("sp_GetUserByUsername", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Username", username);

        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();

        return await reader.ReadAsync() ? MapUser(reader) : null;
    }

    public async Task<string> CreateAsync(User user)
    {
        await using var conn = (SqlConnection)factory.CreateConnection();
        await using var cmd = new SqlCommand("sp_CreateUser", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Id", user.Id);
        cmd.Parameters.AddWithValue("@Username", user.Username);
        cmd.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
        cmd.Parameters.AddWithValue("@Email", user.Email);

        await conn.OpenAsync();
        await cmd.ExecuteNonQueryAsync();

        return user.Id;
    }

    private static User MapUser(IDataReader reader) => new()
    {
        Id = reader.GetString(0),
        Username = reader.GetString(1),
        PasswordHash = reader.GetString(2),
        Email = reader.GetString(3)
    };
}
