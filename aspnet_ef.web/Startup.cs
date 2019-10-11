using System;
using AutoMapper;
using System.IO;
using aspnet_ef.data;
using aspnet_ef.services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace aspnet_ef.web
{
  public class Startup
  {
    public Startup()
    {
      try
      {
        var builder = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json");
      
        builder.AddUserSecrets<Startup>();
        Configuration = builder.Build();

      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
      }
      
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      var connStr = Configuration.GetConnectionString("default");

      services.AddAutoMapper();
      
      services.AddRouting(x => x.LowercaseUrls = true);
      services.AddMvc()
        .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
        .AddRazorRuntimeCompilation();
      
      services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
      services.AddScoped<LogFilter>();
      services.AddScoped<IContext, Context>();

      var optionsBuilder = new DbContextOptionsBuilder();

      void OptionsRunner(DbContextOptionsBuilder builder)
      {
        builder.EnableSensitiveDataLogging(); // Parameter values will be displayed in logs
        builder.UseSqlServer(connStr);
      }
      
      OptionsRunner(optionsBuilder);

      services.AddScoped<IProductService, ProductService>();
      services.AddDbContextPool<Context>(OptionsRunner);

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
    
  }
  
}