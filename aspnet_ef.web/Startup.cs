using AutoMapper;
using System.IO;
using aspnet_ef.data;
using aspnet_ef.services;
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
    public Startup()
    {
      var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json");

      builder.AddUserSecrets<Startup>();
      Configuration = builder.Build();
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      var connStr = Configuration.GetConnectionString("default");
      var path = Directory.GetCurrentDirectory();
      var info = Directory.GetParent(path);
      var fullPath = Path.Combine(info.FullName, "db");
      var connectionString = string.Format(connStr, fullPath);

      services.AddAutoMapper();
      services.AddRouting(x => x.LowercaseUrls = true);
      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

      services.AddScoped<IContext, Context>();
      services.AddDbContextPool<Context>(x => x.UseSqlite(connectionString));
      services.AddScoped<IProductService, ProductService>();
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      app.UseDeveloperExceptionPage();
      app.UseStaticFiles();
      app.UseMvcWithDefaultRoute();
      
    }
    
  }
  
}