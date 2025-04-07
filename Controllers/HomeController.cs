using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCProject.Models;

namespace MVCProject.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly DatabaseContext _context;
    private readonly IConfiguration _configuration;

    public HomeController(ILogger<HomeController> logger, DatabaseContext context, IConfiguration configuration)
    {
        _logger = logger;
        _context = context;
        _configuration = configuration;
    }

    public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
    {
        var clientUrl = _configuration["Url:ClientURL"];
        var list = await _context.Employees.FromSqlRaw("EXEC Sp_GetAllEmployees").ToListAsync();
        var totalCount=await _context.Employees.CountAsync();
        ViewBag.ClientURL = clientUrl;
        ViewBag.CurrentPage = pageNumber;
        ViewBag.PageSize = pageSize;
        ViewBag.TotalCount = totalCount;

        return View(list);
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
