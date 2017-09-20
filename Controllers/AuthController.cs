using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;

namespace AspnetOkta.Controllers
{
  public class AuthController : Controller
  {
    public IActionResult Login()
    {
      if (!HttpContext.User.Identity.IsAuthenticated)
      {
        return Challenge(OpenIdConnectDefaults.AuthenticationScheme);
      }
      return RedirectToAction("Index", "User");
    }

    public IActionResult Logout()
    {
      return SignOut(CookieAuthenticationDefaults.AuthenticationScheme, OpenIdConnectDefaults.AuthenticationScheme);
    }
  }
}