using System.Diagnostics;
using Clean.Application.Abstractions;
using Dapper;
using Microsoft.AspNetCore.Http;


namespace Clean.Infrastructure.Data;

public class StudentProfileContext : IStudentProfileContext
{
    private readonly IAppDbContext _context;

    public StudentProfileContext(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<int> CreateStudentProfileAsync(AddStudentProfileDto addStudentProfileDto, string filename)
    {
        using var conn = _context.GetConnection();

        var query = """
                    insert into student_profiles(fullname, phone, profile_picture)
                    values (@FullName, @Phone, @ProfilePicture) returning id
                    """;

        return await conn.ExecuteScalarAsync<int>(query, new
        {
            FullName = addStudentProfileDto.FullName,
            Phone = addStudentProfileDto.Phone,
            ProfilePicture = filename
        });
    }

    public async Task<GetStudentProfileDto?> GetStudentProfileById(int id)
    {
        using (var conn = _context.GetConnection())
        {
            var sql = """
                      select
                      id as Id,
                      fullname as FullName,
                      phone as Phone,
                      profile_picture as ProfilePicture
                      from student_profiles
                      where id = @Id
                      """;
            var student = await conn.QueryFirstOrDefaultAsync<GetStudentProfileDto>(sql, new { Id = id });
            return student;
        }
    }

    public async Task<bool> UpdateStudentProfileAsync(UpdateStudentProfileDto updateStudentProfileDto)
    {
        using (var conn = _context.GetConnection())
        {
            var sql = """
                      update student_profiles
                      set 
                          fullname = @FullName,
                          phone = @Phone,
                          profile_picture = @ProfilePicture
                          where id = @Id
                      """;
            var isUpdated = await conn.ExecuteAsync(sql, updateStudentProfileDto);
            return isUpdated == 1;
        }
    }
}