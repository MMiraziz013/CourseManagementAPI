namespace Clean.Domain.Models;

public class StudentGroup
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int GroupId { get; set; }
    public DateTime JoinedAt { get; set; }
}