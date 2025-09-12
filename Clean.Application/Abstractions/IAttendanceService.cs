namespace Clean.Application.Abstractions;

public interface IAttendanceService
{
    Task<Response<List<AttendanceDto>>> GetMissingAttendanceAsync(DateOnly date);
}