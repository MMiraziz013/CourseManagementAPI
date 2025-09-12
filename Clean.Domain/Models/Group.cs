namespace Clean.Domain.Models;

public class Group
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public string GroupName { get; set; }
    public DateTime StartDate { get; set; }
}