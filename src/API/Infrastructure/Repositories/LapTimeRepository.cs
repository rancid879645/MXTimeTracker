using System.Data;
using Microsoft.Data.SqlClient;
using MotocrossTracker.API.Domain.Entities;
using MotocrossTracker.API.Domain.Interfaces;

namespace MotocrossTracker.API.Infrastructure.Repositories;

public class LapTimeRepository(IDbConnectionFactory factory) : ILapTimeRepository
{
    public async Task<LapTime?> GetByIdAsync(int id)
    {
        await using var conn = (SqlConnection)factory.CreateConnection();
        await using var cmd = new SqlCommand("sp_GetLapTimeById", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Id", id);

        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();

        return await reader.ReadAsync() ? MapLapTime(reader) : null;
    }

    public async Task<IEnumerable<LapTime>> GetAllByUserAsync(string userId)
    {
        await using var conn = (SqlConnection)factory.CreateConnection();
        await using var cmd = new SqlCommand("sp_GetLapTimesByUser", conn) { CommandType = CommandType.StoredProcedure };
        cmd.Parameters.AddWithValue("@UserId", userId);

        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();

        var results = new List<LapTime>();
        while (await reader.ReadAsync())
            results.Add(MapLapTime(reader));

        return results;
    }

    public async Task<int> CreateAsync(LapTime lapTime)
    {
        await using var conn = (SqlConnection)factory.CreateConnection();
        await using var cmd = new SqlCommand("sp_CreateLapTime", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@RiderName", lapTime.RiderName);
        cmd.Parameters.AddWithValue("@TrackName", lapTime.TrackName);
        cmd.Parameters.AddWithValue("@Time", lapTime.Time);
        cmd.Parameters.AddWithValue("@Date", lapTime.Date);
        cmd.Parameters.AddWithValue("@UserId", lapTime.UserId);

        await conn.OpenAsync();
        return (int)(await cmd.ExecuteScalarAsync())!;
    }

    public async Task<bool> UpdateAsync(LapTime lapTime)
    {
        await using var conn = (SqlConnection)factory.CreateConnection();
        await using var cmd = new SqlCommand("sp_UpdateLapTime", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Id", lapTime.Id);
        cmd.Parameters.AddWithValue("@RiderName", lapTime.RiderName);
        cmd.Parameters.AddWithValue("@TrackName", lapTime.TrackName);
        cmd.Parameters.AddWithValue("@Time", lapTime.Time);
        cmd.Parameters.AddWithValue("@Date", lapTime.Date);
        cmd.Parameters.AddWithValue("@UserId", lapTime.UserId);

        await conn.OpenAsync();
        return await cmd.ExecuteNonQueryAsync() > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        await using var conn = (SqlConnection)factory.CreateConnection();
        await using var cmd = new SqlCommand("sp_DeleteLapTime", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Id", id);

        await conn.OpenAsync();
        return await cmd.ExecuteNonQueryAsync() > 0;
    }

    private static LapTime MapLapTime(IDataReader reader) => new()
    {
        Id = reader.GetInt32(0),
        RiderName = reader.GetString(1),
        TrackName = reader.GetString(2),
        Time = (TimeSpan)reader[3],
        Date = reader.GetDateTime(4),
        UserId = reader.GetString(5)
    };
}
