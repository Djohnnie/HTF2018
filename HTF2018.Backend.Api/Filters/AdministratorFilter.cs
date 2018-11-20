using HTF2018.Backend.Common;
using HTF2018.Backend.Common.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace HTF2018.Backend.Api.Filters
{
    public class AdministratorFilter : IAsyncActionFilter
    {
        private readonly IHtfContext _htfContext;

        public AdministratorFilter(IHtfContext htfContext)
        {
            _htfContext = htfContext;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (_htfContext.IsIdentifiedAsAdmin)
            {
                await next();
            }
            else
            {
                throw new NotIdentifiedAsAdminException();
            }
        }
    }
}