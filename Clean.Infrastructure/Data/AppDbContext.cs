using Clean.Application.Abstractions;
using Npgsql;

namespace Clean.Infrastructure.Data;

public class AppDbContext : IAppDbContext
{
    private string _connectionString =
        "YOUR CONNECTION STRING";
    
    public NpgsqlConnection GetConnection()
    {
        return new NpgsqlConnection(_connectionString);
    }
}