namespace Clean.Application.Abstractions;

public interface IPaymentService
{
    Task<Response<List<PaymentDto>>> GetPaymentsAsync();
}