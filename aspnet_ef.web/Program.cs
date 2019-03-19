using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace aspnet_ef.web
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var builder = WebHost.CreateDefaultBuilder(args).UseStartup<Startup>();
      var runner = builder.Build();

      runner.Run();

    }
    
  }
  
}