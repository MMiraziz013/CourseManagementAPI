using Clean.Application.Abstractions;

namespace Clean.Application.Services;

public class PaymentService : IPaymentService
{
    private readonly IPaymentContext _context;

    public PaymentService(IPaymentContext context)
    {
        _context = context;
    }
    
    public async Task<Response<List<PaymentDto>>> GetPaymentsAsync()
    {
        var response = await _context.GetPaymentsAsync();
        return new Response<List<PaymentDto>>(response);
    }
}