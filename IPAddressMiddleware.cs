using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Imtiaz_API.App_Data;

namespace Imtiaz_API.Middleware
{

    public class IPAddressMiddleware
    {
        private readonly RequestDelegate _next;
        public IPAddressMiddleware(RequestDelegate next)
        {
            _next = next;
        
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Get the IP address of the client
            var ipAddress = context.Connection.RemoteIpAddress.MapToIPv4().ToString();

            // Get the navigation header of the client
            var userAgent = context.Request.Headers["User-Agent"];

            context.Items["ipAddress"] = ipAddress;
            context.Items["userAgent"] = userAgent;
            // Call the next middleware in the pipeline
            await _next(context);
        }
    }
}