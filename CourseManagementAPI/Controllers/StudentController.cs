using Clean.Application.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagementAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentController : Controller
{
    private readonly IStudentService _studentService;

    public StudentController(IStudentService studentService)
    {
        _studentService = studentService;
    }
    
    // GET
    [HttpGet(Name = "GetStudents")]
    public async Task<IActionResult> GetStudentsAsync()
    {
        var response = await _studentService.GetStudentsAsync();
        return Ok(response);
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchFilteredStudentsAsync([FromQuery] StudentSearchDto studentSearchDto)
    {
        var response = await _studentService.SearchStudentsAsync(studentSearchDto);
        return Ok(response);
    }
}