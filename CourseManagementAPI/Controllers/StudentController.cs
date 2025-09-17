using Clean.Application.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagementAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentController : Controller
{
    private readonly IStudentService _studentService;
    private readonly IQrCodeService _qrCodeService;

    public StudentController(IStudentService studentService, IQrCodeService qrCodeService)
    {
        _studentService = studentService;
        _qrCodeService = qrCodeService;
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
    
    
    [HttpGet("{id}/qrcode")]
    public async Task<IActionResult> GetStudentQr(int id)
    {
        // 1. Find student in DB
        var student = await _studentService.GetByIdAsync(id);
        string logoPath = Path.Combine(AppContext.BaseDirectory, "Assets", "My_Logo.png");
        if (student.Data == null)
            return NotFound($"Student with ID {id} not found.");

        // 2. Build the URL for the QR code
        string url = $"http://localhost:5165/students/{id}";
        // or if you want frontend URL:
        // string url = $"https://yourwebsite.tj/student/{id}";

        // 3. Generate QR as PNG (in-memory, no file saving)
        var qrBytes = _qrCodeService.GenerateQrWithLogo(url, logoPath, 600);

        // 4. Return QR image
        return File(qrBytes, "image/png");
    }

    [HttpGet("exception-test")]
    public IActionResult TestException()
    {
        throw new Exception("Custom made exception to test exception handling middleware");
    }
}