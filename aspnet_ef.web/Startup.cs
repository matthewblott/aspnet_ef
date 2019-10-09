using System;
using AutoMapper;
using System.IO;
using aspnet_ef.data;
using aspnet_ef.services;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace aspnet_ef.web
{
  public class Startup
  {
    public IWebHostEnvironment WebHostEnvironment { get; }
    public Startup(IWebHostEnvironment environment)
    {
      var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json");

      WebHostEnvironment = environment;
      
      builder.AddUserSecrets<Startup>();
      Configuration = builder.Build();
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      var connStr = Configuration.GetConnectionString("default");
      
      var path = WebHostEnvironment.ContentRootPath;
      var info = Directory.GetParent(path);
      
      var fullPath = Path.Combine(info.FullName, "db");
      var connectionString = string.Format(connStr, fullPath);
      
      services.AddAutoMapper();
      services.AddRouting(x => x.LowercaseUrls = true);
      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
        .AddRazorRuntimeCompilation();

      services.AddScoped<IContext, Context>();
      services.AddDbContextPool<Context>(x => x.UseSqlite(connectionString));
      services.AddScoped<IProductService, ProductService>();
      
    }

    public void Configure(IApplicationBuilder app)
    {
      app.UseDeveloperExceptionPage();
      app.UseStaticFiles();
      app.UseRouting();
      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
      });      
      
    }
    
    public static void Main(string[] args) =>
      WebHost.CreateDefaultBuilder(args).UseStartup<Startup>().Build().Run();
    
  }
  
}