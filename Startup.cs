using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Serialization;

namespace AspnetOkta
{
  public class Startup
  {
    public Startup(IHostingEnvironment env)
    {
      var builder = new ConfigurationBuilder()
          .SetBasePath(env.ContentRootPath)
          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
          .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
          .AddEnvironmentVariables();
      Configuration = builder.Build();
    }

    public IConfigurationRoot Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddAuthentication(sharedOptions =>
          sharedOptions.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme
        );
      // Add framework services.
      services.AddMvc();

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
    {
      loggerFactory.AddConsole(Configuration.GetSection("Logging"));
      loggerFactory.AddDebug();

      app.UseDeveloperExceptionPage();

      if (env.IsDevelopment())
      {
        app.UseBrowserLink();
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
      }

      app.UseStaticFiles();

      app.UseCookieAuthentication(new CookieAuthenticationOptions()
      {
        AuthenticationScheme = CookieAuthenticationDefaults.AuthenticationScheme,
        AutomaticChallenge = true
      });

      app.UseOpenIdConnectAuthentication(new OpenIdConnectOptions()
      {
        ClientId = "ZKiRtUGt5d87UMBQTxVw",
        ClientSecret = "5WKvIqD1FrbsC-vBMEl-rqLK-zK-Men3rwSsERAf",
        Authority = "https://dev-613050.oktapreview.com/oauth2/ausaw1du8sQJUCGIk0h7",
        ResponseType = OpenIdConnectResponseType.CodeIdToken,
        GetClaimsFromUserInfoEndpoint = true,
        SaveTokens = true,
        TokenValidationParameters = new TokenValidationParameters
        {
          ValidateIssuer = true
        }
      });

      app.UseMvc(routes =>
      {
        routes.MapRoute(
                  name: "default",
                  template: "{controller=Home}/{action=Index}/{id?}");
      });
    }
  }
}
