namespace Clean.Application.Abstractions;

public class AttendanceDto
{
    public string Id { get; set; }
    public string StudentName { get; set; }
    public string GroupName { get; set; }
    public string CourseTitle { get; set; }
    public DateOnly LessonDate { get; set; }
}