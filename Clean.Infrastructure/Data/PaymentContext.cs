using Clean.Application.Abstractions;
using Dapper;

namespace Clean.Infrastructure.Data;

public class PaymentContext : IPaymentContext
{
    private readonly IAppDbContext _context;

    public PaymentContext(IAppDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<PaymentDto>> GetPaymentsAsync()
    {
        using (var conn = _context.GetConnection())
        {
            var sql =
                """
                select
                    p.id as Id,
                    s.fullname as StudentName,
                    g.groupname as GroupName,
                    c.title as CourseTitle,
                    p.amount as Amount,
                    p.paidat as PaidAt
                    from payments p 
                    left join studentgroups as sg on p.studentgroupid = sg.id
                    left join students as s on sg.studentid = s.id
                    left join groups as g on sg.groupid = g.id
                    left join courses as c on g.courseid = c.id
                    group by p.id, s.fullname, g.groupname, c.title, p.amount, p.paidat
                    order by p.paidat
                """;
            var list = await conn.QueryAsync<PaymentDto>(sql);
            return list.ToList();
        }
    } 
}