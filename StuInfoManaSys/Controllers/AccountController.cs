using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StuInfoManaSys.ViewModels;

namespace StuInfoManaSys.Controllers;

public class AccountController(SignInManager<IdentityUser> signInManager) : Controller
{
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
    {
        if (ModelState.IsValid)
        {
            // 第一个参数是用户名，第二个参数是密码，第三个参数表示是否记住登录，第四个参数表示如果用户名或密码输入错误是否锁定用户
            var result = await signInManager.PasswordSignInAsync(model.UserName, model.Password, true, false);
            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                // 如果没有指定返回地址，则重定向到默认的首页
                return RedirectToAction("Index", "Home");
            }
        }
        ModelState.AddModelError(string.Empty, "用户名或密码错误");
        return View(model);
    }

    public IActionResult Logout()
    {
        signInManager.SignOutAsync().Wait();
        // 重定向到登录页面
        return RedirectToAction("Login");
    }

    public IActionResult AccessDenied()
    {
        return View();
    }
}
