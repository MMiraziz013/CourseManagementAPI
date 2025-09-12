using Clean.Application.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagementAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AttendanceController : Controller
{
    private readonly IAttendanceService _attendanceService;

    public AttendanceController(IAttendanceService attendanceService)
    {
        _attendanceService = attendanceService;
    }
    
    // GET
    [HttpGet(Name = "GetMissedAttendance")]
    public async Task<IActionResult> GetMissedAttendanceAsync(DateOnly date)
    {
        var response = await _attendanceService.GetMissingAttendanceAsync(date);
        return Ok(response);
    }
}