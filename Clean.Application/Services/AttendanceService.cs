using Clean.Application.Abstractions;

namespace Clean.Application.Services;

public class AttendanceService : IAttendanceService
{
    private readonly IAttendanceContext _context;

    public AttendanceService(IAttendanceContext context)
    {
        _context = context;
    }
    
    public async Task<Response<List<AttendanceDto>>> GetMissingAttendanceAsync(DateOnly date)
    {
        var response = await _context.GetMissingAttendanceAsync(date);
        return new Response<List<AttendanceDto>>(response);
    }
}