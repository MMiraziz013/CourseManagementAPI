namespace Clean.Application.Abstractions;

public class CourseDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int DurationMonths { get; set; }
    public decimal FullPrice { get; set; }
    public decimal ExpectedIncome { get; set; }
    public int StudentCount { get; set; }
    public decimal CurrentPaidAmount { get; set; }
}