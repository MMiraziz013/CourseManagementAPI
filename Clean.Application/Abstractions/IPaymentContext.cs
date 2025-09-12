namespace Clean.Application.Abstractions;

public interface IPaymentContext
{
    Task<List<PaymentDto>> GetPaymentsAsync();
}