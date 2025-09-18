using Clean.Application.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagementAPI.Controllers;

public class StudentProfileController : Controller
{
    private readonly IStudentProfileService _studentProfileService;

    public StudentProfileController(IStudentProfileService studentProfileService)
    {
        _studentProfileService = studentProfileService;
    }
    
    [HttpPost("profiles")]
    public async Task<IActionResult> CreateProfile([FromForm] AddStudentProfileDto dto, [FromServices] IWebHostEnvironment environment)
    {
        string? filename = null;
        if (dto.ProfilePicture != null)
        {
            filename = Guid.NewGuid() + Path.GetExtension(dto.ProfilePicture.FileName);
            var path = Path.Combine(environment.WebRootPath, "images", filename);

            using var stream = new FileStream(path, FileMode.Create);
            await dto.ProfilePicture.CopyToAsync(stream);
        }

        var response = await _studentProfileService.CreateStudentProfile(dto, filename);
        return Ok(response);
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateProfile([FromForm] AddStudentProfileDto dto)
    {
        var result = await _studentProfileService.UpdateStudentProfile(dto);
        return Ok(result);
    }
}