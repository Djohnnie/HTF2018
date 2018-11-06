using HTF2018.Backend.Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeamModel = HTF2018.Backend.Common.Model.Team;

namespace HTF2018.Backend.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly ITeamLogic _teamLogic;

        public DashboardController(ITeamLogic teamLogic)
        {
            _teamLogic = teamLogic;
        }

        /// <summary>
        /// Gets an alphabetical list of all teams.
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("teams")]
        public async Task<IActionResult> GetAllTeams()
        {
            List<TeamModel> teams = await _teamLogic.GetAllTeams();
            return Ok(teams);
        }
    }
}