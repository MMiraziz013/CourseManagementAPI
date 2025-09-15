using Clean.Application.Abstractions;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class QrController : Controller
{
    private readonly IQrCodeService _qrCodeService;

    public QrController(IQrCodeService qrCodeService)
    {
        _qrCodeService = qrCodeService;
    }

    [HttpPost("qrcode")]
    public IActionResult GenerateQrCode(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
            return BadRequest("URL is required");
        

        string logoPath = Path.Combine(AppContext.BaseDirectory, "Assets", "My_Logo.png");
        var qrBytes = _qrCodeService.GenerateQrWithLogo(url, logoPath);
        return File(qrBytes, "image/png");
    }
}