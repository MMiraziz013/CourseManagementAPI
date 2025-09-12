namespace Clean.Domain.Models;

public class Course
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int DurationMonths { get; set; }
    public decimal Price { get; set; }
}