using System.Threading.Tasks;
using HTF2018.Backend.DataAccess.Entities;

namespace HTF2018.Backend.Logic.Interfaces
{
    public interface ITeamLogic
    {
        Task<Team> FindTeamByName(string name);

        Task<Team> FindTeamByIdentification(string identification);

        Task<Team> CreateTeam(string name, string secret);
    }
}