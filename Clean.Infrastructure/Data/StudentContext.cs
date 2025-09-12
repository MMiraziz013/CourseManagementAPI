using Clean.Application.Abstractions;
using Dapper;

namespace Clean.Infrastructure.Data;

public class StudentContext : IStudentContext
{
    private readonly IAppDbContext _context;

    public StudentContext(IAppDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<StudentDto>> GetStudentsAsync()
    {
        using (var conn = _context.GetConnection())
        {
            var sql = """
                      select 
                      s.id as Id,
                      s.fullname as FullName,
                      s.phone as Phone,
                      g.groupname as GroupName,
                      c.title as CourseName
                      from studentgroups
                      left join students as s on studentgroups.studentid = s.id
                      left join groups as g on studentgroups.groupid = g.id
                      left join courses as c on g.courseid = c.id
                      group by s.fullname, s.id, s.phone, g.groupname, c.title
                      order by s.id
                      """;
            var list = await conn.QueryAsync<StudentDto> (sql);
            return list.ToList();
        }
    }

    public async Task<List<StudentFilterDto>> SearchStudentsAsync(StudentSearchDto studentSearchDto)
    {
        using (var conn = _context.GetConnection())
        {
            string nameFilter = $"%{studentSearchDto.Name}%";
            string courseFilter = $"{studentSearchDto.CourseName}";
            decimal minPayment = studentSearchDto.MinPayment;
            var sql = """
                      select 
                      s.id as Id,
                      s.fullname as FullName,
                      s.phone as Phone,
                      c.title as CourseName,
                      coalesce(sum(p.amount), 0) as TotalPaid
                      from studentgroups sg
                      left join students as s on sg.studentid = s.id
                      left join groups as g on sg.groupid = g.id
                      left join courses as c on g.courseid = c.id
                      left join payments as p on sg.id = p.studentgroupid
                      where
                          s.fullname like @NameFilter and c.title like @CourseFilter
                      group by s.fullname, s.id, s.phone, g.groupname, c.title
                      having coalesce(sum(p.amount), 0) > @MinPayment
                      order by s.id
                      """;

            var list = 
                await conn.QueryAsync<StudentFilterDto>(sql, new {NameFilter = nameFilter, CourseFilter = courseFilter, MinPayment = minPayment});
            return list.ToList();
        }
    }
}