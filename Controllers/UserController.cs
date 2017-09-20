using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using System.Net.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Http;
using Okta.Sdk;
using Okta.Sdk.Configuration;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;
using AspnetOkta.Mapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AspnetOkta.Controllers
{
  [Authorize]
  public class UserController : Controller
  {
    private OktaClient client;
    //private string userId;

    public UserController()
    {
      client = new OktaClient(new OktaClientConfiguration
      {
        OrgUrl = "https://dev-613050.oktapreview.com",
        Token = "00irV9xpiSe5upDotwfq6l4wb40JAiL61GdW8ZAV9f"
      });

    }
    public async Task<IActionResult> Index()
    {
      var idClaim = User.FindFirst(x => x.Type == @"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
      if (idClaim != null)
      {
        var userId = idClaim.Value;
        var user = await client.Users.GetUserAsync(userId);
        var profile = await Map.ToMyUser(user);
        return View(profile);
      }
      else
      {
        return View();
      }
    }
  }
}