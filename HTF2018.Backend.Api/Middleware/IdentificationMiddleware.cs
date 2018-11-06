using HTF2018.Backend.Common;
using HTF2018.Backend.DataAccess.Entities;
using HTF2018.Backend.Logic.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading.Tasks;
using HTF2018.Backend.Common.Extensions;

namespace HTF2018.Backend.Api.Middleware
{
    public class IdentificationMiddleware
    {
        private const String IDENTIFICATION_HEADER = "htf-identification";
        private readonly RequestDelegate _next;

        public IdentificationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IHtfContext htfContext, ITeamLogic teamLogic)
        {
            if (context.Request.Headers.ContainsKey(IDENTIFICATION_HEADER) && context.Request.Headers[IDENTIFICATION_HEADER].Count == 1)
            {
                String identification = context.Request.Headers[IDENTIFICATION_HEADER].Single();
                htfContext.Identification = identification;
                Guid identificationGuid = Guid.Empty;
                if (Guid.TryParse(identification.Base64Decode(), out identificationGuid))
                {
                    Team team = await teamLogic.FindTeamByIdentification(identification);
                    if (team != null)
                    {
                        htfContext.IsIdentified = true;
                        htfContext.Team = new Common.Model.Team { Id = team.Id, Name = team.Name };
                    }
                }
            }
            await _next(context);
        }
    }
}