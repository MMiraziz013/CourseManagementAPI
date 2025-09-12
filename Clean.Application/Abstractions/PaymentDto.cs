namespace Clean.Application.Abstractions;

public class PaymentDto
{
    public int Id { get; set; }
    public string StudentName { get; set; }
    public string GroupName { get; set; }
    public string CourseTitle { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaidAt { get; set; }
}