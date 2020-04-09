using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TreasureSweepGame.Models;

namespace TreasureSweepGame
{
  public class Startup
  {
    public Startup(IHostingEnvironment env, IConfiguration configuration)
    {
      Configuration = configuration;

      var builder = new ConfigurationBuilder()
          .SetBasePath(env.ContentRootPath)
          .AddJsonFile("appsettings.json");
      Configuration = builder.Build();
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      services.Configure<CookiePolicyOptions>(options =>
      {
        options.CheckConsentNeeded = context => true;
        options.MinimumSameSitePolicy = SameSiteMode.None;
      });


      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

      // services.AddEntityFrameworkMySql()
      //   .AddDbContext<TreasureSweepGameContext>(options => options
      //   .UseMySql(Configuration["ConnectionStrings:DefaultConnection"]));
      services.AddEntityFrameworkSqlite()
        .AddDbContext<TreasureSweepGameContext>(options => options
        .UseSqlite(Configuration.GetConnectionString("DefaultConnection")
        ));

      services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<TreasureSweepGameContext>()
        .AddDefaultTokenProviders();

      services.Configure<IdentityOptions>(options =>
      {
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 0;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequiredUniqueChars = 0;
      });
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
      }

      //   app.UseHttpsRedirection();
      app.UseStaticFiles();
      app.UseAuthentication();
      app.UseCookiePolicy();

      app.UseMvc(routes =>
      {
        routes.MapRoute(
            name: "default",
            template: "{controller=Home}/{action=Index}/{id?}");
      });

      app.Run(async (context) =>
      {
        await context.Response.WriteAsync("Something went wrong!");
      });
    }
  }
}
