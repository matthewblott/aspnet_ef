using System;
using AutoMapper;
using System.IO;
using aspnet_ef.data;
using aspnet_ef.services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace aspnet_ef.web
{
  public class Startup
  {
    private readonly ILoggerFactory _loggerFactory;
    
    public Startup(ILoggerFactory loggerFactory)
    {
      var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json");

      _loggerFactory = loggerFactory;
      
      builder.AddUserSecrets<Startup>();
      Configuration = builder.Build();
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

      services.AddScoped<IContext, Context>();

      var optionsBuilder = new DbContextOptionsBuilder();

      void OptionsRunner(DbContextOptionsBuilder builder)
      {
//        var logger = _loggerFactory.CreateLogger(DbLoggerCategory.Database.Connection.Name);
//
//        logger.Log(LogLevel.Information, "Hello World!");
        
        builder.UseSqlServer(connStr);
        builder.EnableSensitiveDataLogging();
        builder.UseLoggerFactory(_loggerFactory);
        
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