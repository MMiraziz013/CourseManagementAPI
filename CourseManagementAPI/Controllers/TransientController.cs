using Clean.Application.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagementAPI.Controllers;

public class TransientController : Controller
{
    private readonly ITransientService _transientService;
    private readonly ITransientService _transientService1;

    public TransientController(ITransientService transientService, ITransientService transientService1)
    {
        _transientService = transientService;
        _transientService1 = transientService1;
    }
    // GET
    [HttpGet("transient")]
    public List<string> Index()
    {
        return new List<string>
        {
            _transientService.GetTransient().ToString(),
            _transientService1.GetTransient().ToString()
        };
    }
}