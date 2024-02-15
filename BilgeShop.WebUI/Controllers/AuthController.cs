using BilgeShop.Business.Dtos.User;
using BilgeShop.Business.Services;
using BilgeShop.WebUI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BilgeShop.WebUI.Controllers;

public class AuthController : Controller
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }


    [HttpGet]
    [Route("KayitOl")]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [Route("KayitOl")]

    public IActionResult Register(RegisterViewModel formData)
    {
        if (!ModelState.IsValid)
        {
            return View(formData);
        }

        var userAddDto = new UserAddDto()
        {
            FirstName = formData.FirstName.Trim(),
            LastName = formData.LastName.Trim(),
            Email = formData.Email.Trim(),
            Password = formData.Password
        };

        _userService.AddUser(userAddDto);


        return RedirectToAction("Index", "Home");
    }



    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel formData)
      {

        if (!ModelState.IsValid)
        {
            return RedirectToAction("Index", "Home");

        }

        var loginDto = new UserLoginDto()
        {
            Email = formData.Email.Trim(),
            Password = formData.Password,
        };

        var userInfo = _userService.Login(loginDto);

        if (userInfo is not null)
        {
            var claims = new List<Claim>();

            claims.Add(new Claim("id", userInfo.Id.ToString()));
            claims.Add(new Claim("email", userInfo.Email));
            claims.Add(new Claim("firstName", userInfo.FirstName));
            claims.Add(new Claim("lastName", userInfo.LastName));
            claims.Add(new Claim("userType", userInfo.UserType.ToString()));


            //Yetkinlendirme için

            claims.Add(new Claim(ClaimTypes.Role, userInfo.UserType.ToString()));


            var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);


            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = new DateTimeOffset(DateTime.Now.AddHours(48))
            };


            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity), authProperties);

            TempData["SuccessMessage"] = "Kullanıcı girişi yapıldı";
            return RedirectToAction("Index", "Home");

        }

        else
        {
            TempData["SuccessMessage"] = "Kullanıcı yok";
            return RedirectToAction("Index,Home");
        }



    }


    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        TempData["SuccessMessage"] = "Oturum Kapatıldı";
        return RedirectToAction("Index", "Home");
    }
}
