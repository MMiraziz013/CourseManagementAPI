namespace Clean.Domain.Models;

public class Payment
{
    public int Id { get; set; }
    public int StudentGroupId { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaidAt { get; set; }
}