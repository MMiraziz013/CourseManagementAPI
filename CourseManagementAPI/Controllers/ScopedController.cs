using Clean.Application.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagementAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ScopedController : Controller
{
    private readonly IScopedService _scopedService;

    public ScopedController(IScopedService scopedService)
    {
        _scopedService = scopedService;
    }
    
    // GET
    [HttpGet("scoped")]
    public List<string> GetUniqueScoped()
    {
        List<string> check = [_scopedService.GetUnique().ToString(), _scopedService.GetUnique().ToString()];
        return check;
    }
}