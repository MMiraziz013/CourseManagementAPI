using Clean.Application.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagementAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PaymentController : Controller
{
    private readonly IPaymentService _paymentService;

    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }
    
    // GET
    [HttpGet(Name = "GetPaymentInfo")]
    public async Task<IActionResult> GetPaymentInfo()
    {
        var response = await _paymentService.GetPaymentsAsync();
        return Ok(response);
    }
}