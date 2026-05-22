using System.Data;
using Microsoft.Data.SqlClient;
using MotocrossTracker.API.Domain.Interfaces;

namespace MotocrossTracker.API.Infrastructure;

public class SqlConnectionFactory(string connectionString) : IDbConnectionFactory
{
    public IDbConnection CreateConnection() => new SqlConnection(connectionString);
}
