using HTF2018.Backend.Common;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;
using HTF2018.Backend.Common.Exceptions;

namespace HTF2018.Backend.Api.Filters
{
    public class IdentificationFilter : IAsyncActionFilter
    {
        private readonly IHtfContext _htfContext;

        public IdentificationFilter(IHtfContext htfContext)
        {
            _htfContext = htfContext;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (_htfContext.IsIdentified)
            {
                await next();
            }
            else
            {
                throw new NotIdentifiedException();
            }
        }
    }
}