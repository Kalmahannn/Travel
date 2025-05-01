using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Travel.Data;
using Travel.Models;

public class AccountController : Controller
{

	private TokenService _tokenService;
	private UserManager<AppUser> _accountManager;
	private SignInManager<AppUser> _singInManager;
	private readonly UserManager<AppUser> _userManager;


    public AccountController(UserManager<AppUser> accountManager, SignInManager<AppUser> singInManager, UserManager<AppUser> userManager, TokenService tokenService)
	{
		_accountManager = accountManager;
		_singInManager = singInManager;
		_userManager = userManager;
		_tokenService = tokenService;
	}

	[HttpGet]
    public IActionResult Register() => View();








    [HttpPost]
    public async Task<IActionResult> RegisterAsync(UserModel model)
    {

		if (ModelState.IsValid)
		{

			var user = new AppUser { UserName = model.Username, Email = model.Email };
			var result = await _accountManager.CreateAsync(user, model.Password);

			if (result.Succeeded)
			{
				
				await _singInManager.SignInAsync(user, isPersistent: false);
				return RedirectToAction("Index", "Home");
			}

			foreach (var error in result.Errors)
			{
				ModelState.AddModelError("", error.Description);
			}
		}


		TempData["Success"] = "✅ Тіркелу сәтті өтті!";
        return View(model);
    }

    [HttpGet]
    public IActionResult Login() => View();








    [HttpPost]
    public async Task<IActionResult> Login(UserModel model)
    {
		AppUser appUser = await _accountManager.FindByEmailAsync(model.Email);
		if (appUser != null)
		{
			var result = await _singInManager.PasswordSignInAsync(appUser, model.Password, false, false);
			if (result.Succeeded)
			{
				var token = await _tokenService.GenerateAccessToken(appUser);
				Response.Cookies.Append("token", token);
				
				return RedirectToAction("Index", "Home");
			}
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
