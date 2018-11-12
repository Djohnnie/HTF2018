using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using HTF2018.Backend.Logic.Interfaces;
using Team = HTF2018.Backend.Common.Model.Team;
using TeamStatus = HTF2018.Backend.Common.Model.TeamStatus;
using OverallStatus = HTF2018.Backend.Common.Model.OverallStatus;

namespace HTF2018.Backend.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly ITeamLogic _teamLogic;
        private readonly IDashboardLogic _dashboardLogic;

        public DashboardController(ITeamLogic teamLogic, IDashboardLogic dashboardLogic)
        {
            _teamLogic = teamLogic;
            _dashboardLogic = dashboardLogic;
        }

        /// <summary>
        /// Gets an alphabetical list of all teams.
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("teams")]
        public async Task<IActionResult> GetAllTeams()
        {
            List<Team> teams = await _teamLogic.GetAllTeams();
            return Ok(teams);
        }

        /// <summary>
        /// Gets the team-challenges status for a specific team.
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("teams/{teamId}/status")]
        public async Task<IActionResult> GetTeamStatus(Guid teamId)
        {
            TeamStatus teamStatus = await _dashboardLogic.GetTeamStatus(teamId);
            return Ok(teamStatus);
        }

        /// <summary>
        /// Gets the team-challenges status for a specific team.
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("teams/overall/status")]
        public async Task<IActionResult> GetOverallPendingStatus(Guid teamId)
        {
            OverallStatus overallStatus = await _dashboardLogic.GetOverallPendingStatus();
            return Ok(overallStatus);
        }
    }
}