using Clean.Application.Abstractions;
using Dapper;

namespace Clean.Infrastructure.Data;

public class CourseContext : ICourseContext
{
    private readonly IAppDbContext _context;

    public CourseContext(IAppDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<CourseDto>> GetCoursesAsync()
    {
        using (var conn = _context.GetConnection())
        {
            var sql =
                """
                select 
                    c.id as Id,
                    c.title as Title,
                    c.durationmonths as DurationMonths,
                    c.price as FullPrice,
                    count(s.id) as StudentCount,
                    c.price * count(s.id) as ExpectedIncome,
                    coalesce(sum(p.amount), 0) as CurrentPaidAmount
                    from studentgroups sg 
                    left join groups as g on sg.groupid = g.id
                    left join courses as c on g.courseid = c.id
                    left join payments as p on sg.id = p.studentgroupid
                    left join students as s on sg.studentid = s.id
                    group by c.id, c.title, c.durationmonths, c.price 
                    order by c.id
                """;
            var list = await conn.QueryAsync<CourseDto>(sql);
            return list.ToList();
        }
    }
}