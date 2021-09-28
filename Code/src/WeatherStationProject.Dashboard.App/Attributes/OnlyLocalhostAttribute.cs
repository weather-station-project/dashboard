using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WeatherStationProject.Dashboard.App.Attributes
{
    public class AllowOnlyFromLocalhostLocalhostAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //var remoteIp = context.HttpContext.Connection.RemoteIpAddress;
            // if (null != remoteIp && !IPAddress.IsLoopback(remoteIp))
            // {
            //     context.Result = new UnauthorizedResult();
            //     return;
            // }
            
            base.OnActionExecuting(context);
        }
    }
}