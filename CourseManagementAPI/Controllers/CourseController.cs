using Clean.Application.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;

namespace CourseManagementAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CourseController : Controller
{
    private readonly ICourseService _courseService;

    public CourseController(ICourseService courseService)
    {
        _courseService = courseService;
    }
    
    // GET
    [HttpGet(Name = "GetCourses")]
    public async Task<IActionResult> GetCoursesAsync()
    {
        var response = await _courseService.GetCoursesAsync();
        return Ok(response);
    }
}