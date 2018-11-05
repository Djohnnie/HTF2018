using HTF2018.Backend.Common;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace HTF2018.Backend.Api.Middleware
{
    public class ThrottlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ThrottlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IHtfContext htfContext)
        {
            if (context.Request.Headers.ContainsKey("htf-identification"))
            {
                await Task.Delay(1000);
                context.Response.StatusCode = 429;
                await context.Response.WriteAsync("The Artifact does not allow multiple interactions without delaying for at least 10 seconds!");
                return;
            }
            else
            {
                await Task.Delay(5000);
            }
            await _next(context);
        }
    }
}