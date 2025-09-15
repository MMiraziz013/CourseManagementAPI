using Clean.Application.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagementAPI.Controllers;

public class SingletonController : Controller
{
    private readonly ISingletonService _singletonService;

    public SingletonController(ISingletonService singletonService)
    {
        _singletonService = singletonService;
    }
    
    // GET
    [HttpGet("singleton")]
    public List<string> Index()
    {
        List<string> check = [_singletonService.GetSingleton().ToString(), _singletonService.GetSingleton().ToString()];
        return check;
    }
}