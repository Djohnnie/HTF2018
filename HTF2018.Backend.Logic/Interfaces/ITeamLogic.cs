using HTF2018.Backend.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeamModel = HTF2018.Backend.Common.Model.Team;

namespace HTF2018.Backend.Logic.Interfaces
{
    public interface ITeamLogic
    {
        Task<List<TeamModel>> GetAllTeams();

        Task<Team> FindTeamByName(string name);

        Task<Team> FindTeamByIdentification(string identification);

        Task<Team> CreateTeam(string name, string secret);
    }
}