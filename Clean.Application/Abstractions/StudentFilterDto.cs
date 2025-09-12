namespace Clean.Application.Abstractions;

public class StudentFilterDto
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Phone { get; set; }
    public string CourseName { get; set; }
    public decimal TotalPaid { get; set; }
}