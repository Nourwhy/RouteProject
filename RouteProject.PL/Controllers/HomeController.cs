using System.Diagnostics;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using RouteProject.PL.Models;
using RouteProject.PL.Services;

namespace RouteProject.PL.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IScopedService scopedService1;
    private readonly IScopedService scopedService2;
    private readonly ITransetService transetService1;
    private readonly ITransetService transetService2;
    private readonly ISingletonService singletonService1;
    private readonly ISingletonService singletonService2;

    public HomeController(
        ILogger<HomeController> logger,
        IScopedService scopedService1,
        IScopedService scopedService2,
        ITransetService transetService1,
        ITransetService transetService2,
        ISingletonService singletonService1,
        ISingletonService singletonService2
        )
    {
        _logger = logger;
        this.scopedService1 = scopedService1;
        this.scopedService2 = scopedService2;
        this.transetService1 = transetService1;
        this.transetService2 = transetService2;
        this.singletonService1 = singletonService1;
        this.singletonService2 = singletonService2;
    }

    public string TestLifeTime() 
    {
    StringBuilder builder = new StringBuilder();

        builder.Append($"scopedService1 ::{scopedService1.GetGuid()}\n");
        builder.Append($"scopedService2 ::{scopedService2.GetGuid()}\n\n");
        builder.Append($"transetService1 ::{transetService1.GetGuid()}\n\n");
        builder.Append($"transetService2 ::{transetService2.GetGuid()}\n\n");
        builder.Append($"singletonService1 ::{singletonService1.GetGuid()} \n\n");
        builder.Append($"singletonService2 ::{singletonService2.GetGuid()} \n\n");
    
        return builder.ToString();
    }
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
