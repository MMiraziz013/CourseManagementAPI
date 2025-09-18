using Microsoft.AspNetCore.Http;

namespace Clean.Application.Abstractions;

public class AddStudentProfileDto
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Phone { get; set; }
    public IFormFile? ProfilePicture { get; set; }
}