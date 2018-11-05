using HTF2018.Backend.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System.Threading.Tasks;

namespace HTF2018.Backend.Api.Middleware
{
    public class RequestUriMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestUriMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IHtfContext htfContext)
        {
            htfContext.RequestUri = context.Request.GetDisplayUrl();
            await _next(context);
        }
    }
}