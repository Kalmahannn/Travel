using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

public class AccountController : Controller
{
    [HttpGet]
    public IActionResult Register() => View();

    [HttpPost]
    public IActionResult Register(UserModel model)
    {
        var exists = UserStore.Users.FirstOrDefault(u => u.Username == model.Username);
        if (exists != null)
        {
            ViewBag.Error = "❌ Бұл логин бұрыннан бар.";
            return View(model);
        }

        UserStore.Users.Add(model);
        TempData["Success"] = "✅ Тіркелу сәтті өтті!";
        return RedirectToAction("Login");
    }

    [HttpGet]
    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login(UserModel model)
    {
        var user = UserStore.Users.FirstOrDefault(u =>
            u.Username == model.Username && u.Password == model.Password);

        if (user != null)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username)
            };

            var identity = new ClaimsIdentity(claims, "MyCookieAuth");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("MyCookieAuth", principal);

            return RedirectToAction("Index", "Home");
        }

        ViewBag.Error = "❌ Қате логин немесе құпия сөз!";
        return View(model);
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync("MyCookieAuth");
        return RedirectToAction("Login");
    }

    public IActionResult AccessDenied()
    {
        return Content("🔒 Бұл бетке кіруге рұқсат жоқ.");
    }
}
