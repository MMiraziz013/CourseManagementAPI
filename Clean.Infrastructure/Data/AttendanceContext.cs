using Clean.Application.Abstractions;
using Dapper;

namespace Clean.Infrastructure.Data;

public class AttendanceContext : IAttendanceContext
{
    private readonly IAppDbContext _context;

    public AttendanceContext(IAppDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<AttendanceDto>> GetMissingAttendanceAsync(DateOnly date)
    {
        using (var conn = _context.GetConnection())
        {
            var sql =
                """
                select 
                    s.id as Id,
                    s.fullname as StudentName,
                    g.groupname as GroupName,
                    c.title as CourseTitle,
                    a.lessondate as LessonDate
                    from studentgroups
                    left join students as s on studentgroups.studentid = s.id
                    left join groups as g on studentgroups.groupid = g.id
                    left join courses as c on g.courseid = c.id
                    left join attendance as a on studentgroups.id = a.studentgroupid
                    where a.ispresent is false and a.lessondate = @Date
                    group by s.id, s.fullname, g.groupname, c.title, a.lessondate
                """;

            var list = await conn.QueryAsync<AttendanceDto>(sql, new {Date = date});
            return list.ToList();
        }
    }
}