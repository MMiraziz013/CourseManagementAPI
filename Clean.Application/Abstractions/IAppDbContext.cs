using Npgsql;

namespace Clean.Application.Abstractions;

public interface IAppDbContext
{
    NpgsqlConnection GetConnection();
}