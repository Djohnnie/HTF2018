using HTF2018.Backend.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace HTF2018.Backend.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (InvalidChallengeCodeException)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync("Your request does not map to a known Artifact challenge!");
            }
            catch (AnswerToUnknownChallengeException)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Your answer is submitted for a non-existing challenge!");
            }
            catch (InvalidAnswerException)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Your submitted answer has an invalid markup!");
            }
            catch (InvalidTeamException)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Your team already exists, but you have provided the wrong secret!");
            }
        }
    }
}