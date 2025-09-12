namespace Clean.Application.Abstractions;

public interface IAttendanceContext
{
    Task<List<AttendanceDto>> GetMissingAttendanceAsync(DateOnly date);

}