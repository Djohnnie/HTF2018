using System;
using System.Threading.Tasks;
using HTF2018.Backend.Common.Model;

namespace HTF2018.Backend.Logic.Interfaces
{
    public interface IDashboardLogic
    {
        Task<TeamStatus> GetTeamStatus(Guid teamId);

        Task<OverallStatus> GetOverallPendingStatus();
    }
}