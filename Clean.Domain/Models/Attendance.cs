namespace Clean.Domain.Models;

public class Attendance
{
    public int Id { get; set; }
    public int StudentGroupId { get; set; }
    public DateTime LessonDate { get; set; }
    public bool IsPresent { get; set; }
}