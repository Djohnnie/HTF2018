using HTF2018.Backend.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HTF2018.Backend.Api.Middleware
{
    public class ThrottlingMiddleware
    {
        private TimeSpan THROTTLE_SPAN = TimeSpan.FromSeconds(10);
        private readonly Dictionary<String, DateTime> _throttleIndex = new Dictionary<string, DateTime>();
        private readonly RequestDelegate _next;

        public ThrottlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IHtfContext htfContext)
        {
            if (!String.IsNullOrEmpty(htfContext.Identification))
            {
                if (_throttleIndex.ContainsKey(htfContext.Identification))
                {
                    DateTime lastRequest = _throttleIndex[htfContext.Identification];
                    if (DateTime.UtcNow - lastRequest < THROTTLE_SPAN)
                    {
                        context.Response.StatusCode = 429;
                        await context.Response.WriteAsync($"The Artifact does not allow multiple interactions without delaying for at least {THROTTLE_SPAN.Seconds} seconds!");
                        return;
                    }
                    _throttleIndex[htfContext.Identification] = DateTime.UtcNow;
                }
                else
                {
                    _throttleIndex.Add(htfContext.Identification, DateTime.UtcNow);
                }
            }
            await _next(context);
        }
    }
}