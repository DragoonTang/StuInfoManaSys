using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StuInfoManaSys.Data;
using StuInfoManaSys.Services;
using StuInfoManaSys.ViewModels;

namespace StuInfoManaSys.Controllers;

[Authorize(Roles = $"{nameof(UserRole.Admin)},{nameof(UserRole.Teacher)}")]
public class HomeController(SearchService searchService) : Controller
{
    public IActionResult Index(SearchViewModel model)
    {
        return View(searchService.GetList(model));
    }

    [Route("/error/404")]
    public IActionResult Error404()
    {
        return View();
    }
}
