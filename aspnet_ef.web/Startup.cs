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
    public IConfiguration Configuration { get; }
    
    public Startup(IHostingEnvironment env)
    {
      var builder = new ConfigurationBuilder()
        .SetBasePath(env.ContentRootPath)
        .AddJsonFile("appsettings.json")
        .AddEnvironmentVariables();

      Configuration = builder.Build();
      
    }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddScoped<IProductService, ProductService>();

      var connStr = Configuration.GetConnectionString("default");
      var path = Directory.GetCurrentDirectory();
      var info = Directory.GetParent(path);
      var fullPath = Path.Combine(info.FullName, "db");
      var connectionString = string.Format(connStr, fullPath);

      services.AddDbContextPool<IContext, Context>(x => x.UseSqlite(connectionString));
      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
      
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      app.UseDeveloperExceptionPage();
      app.UseStaticFiles();
      app.UseMvcWithDefaultRoute();

    }
    
  }
  
}