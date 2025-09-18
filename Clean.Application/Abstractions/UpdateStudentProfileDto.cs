namespace Clean.Application.Abstractions;

public class UpdateStudentProfileDto
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Phone { get; set; }
    public string? ProfilePicture { get; set; }
}