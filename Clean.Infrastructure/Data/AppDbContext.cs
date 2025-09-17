using Clean.Application.Abstractions;
using Npgsql;

namespace Clean.Infrastructure.Data;

public class AppDbContext : IAppDbContext
{
    private string _connectionString =
        "Server=localhost; Port=5432;Database=course_db;User Id=postgres; Password=Mm1311Scorpio$";
    
    public NpgsqlConnection GetConnection()
    {
        return new NpgsqlConnection(_connectionString);
    }
}