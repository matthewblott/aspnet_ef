using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
  
namespace aspnet_ef.web
{
  public class LogFilter : ActionFilterAttribute
  {
    private readonly ILogger<LogFilter> _logger;
 
    public LogFilter(ILogger<LogFilter> logger)
    {
      _logger = logger;
    }
 
    public override void OnActionExecuting(ActionExecutingContext context)
    {
      _logger.LogInformation("OnActionExecuting");
      base.OnActionExecuting(context);
    }
 
    public override void OnActionExecuted(ActionExecutedContext context)
    {
      _logger.LogInformation("OnActionExecuted");
      base.OnActionExecuted(context);
    }
 
    public override void OnResultExecuting(ResultExecutingContext context)
    {
      _logger.LogInformation("OnResultExecuting");
      base.OnResultExecuting(context);
    }
 
    public override void OnResultExecuted(ResultExecutedContext context)
    {
      _logger.LogInformation("OnResultExecuted");
      base.OnResultExecuted(context);
    }
  }
}