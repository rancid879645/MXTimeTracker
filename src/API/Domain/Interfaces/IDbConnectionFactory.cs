using System.Data;

namespace MotocrossTracker.API.Domain.Interfaces;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}
