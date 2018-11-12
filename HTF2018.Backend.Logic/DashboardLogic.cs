using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HTF2018.Backend.Common.Model;
using HTF2018.Backend.DataAccess;
using HTF2018.Backend.Logic.Interfaces;
using Challenge = HTF2018.Backend.DataAccess.Entities.Challenge;
using HTF2018.Backend.Common.Exceptions;

namespace HTF2018.Backend.Logic
{
    public class DashboardLogic : IDashboardLogic
    {
        private readonly TheArtifactDbContext _dbContext;

        public DashboardLogic(TheArtifactDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TeamStatus> GetTeamStatus(Guid teamId)
        {
            var team = await _dbContext.Teams.SingleOrDefaultAsync(x => x.Id == teamId);
            if (team != null)
            {
                return new TeamStatus
                {
                    TeamId = teamId,
                    TeamName = team.Name,
                    Challenge01Status = await GetChallengeStatus(Identifier.Challenge01),
                    Challenge02Status = await GetChallengeStatus(Identifier.Challenge02),
                    Challenge03Status = await GetChallengeStatus(Identifier.Challenge03),
                    Challenge04Status = await GetChallengeStatus(Identifier.Challenge04),
                    Challenge05Status = await GetChallengeStatus(Identifier.Challenge05),
                    Challenge06Status = await GetChallengeStatus(Identifier.Challenge06),
                    Challenge07Status = await GetChallengeStatus(Identifier.Challenge07),
                    Challenge08Status = await GetChallengeStatus(Identifier.Challenge08),
                    Challenge09Status = await GetChallengeStatus(Identifier.Challenge09),
                    Challenge10Status = await GetChallengeStatus(Identifier.Challenge10),
                    Challenge11Status = await GetChallengeStatus(Identifier.Challenge11),
                    Challenge12Status = await GetChallengeStatus(Identifier.Challenge12),
                    Challenge13Status = await GetChallengeStatus(Identifier.Challenge13),
                    Challenge14Status = await GetChallengeStatus(Identifier.Challenge14),
                    Challenge15Status = await GetChallengeStatus(Identifier.Challenge15),
                    Challenge16Status = await GetChallengeStatus(Identifier.Challenge16),
                    Challenge17Status = await GetChallengeStatus(Identifier.Challenge17),
                    Challenge18Status = await GetChallengeStatus(Identifier.Challenge18),
                    Challenge19Status = await GetChallengeStatus(Identifier.Challenge19),
                    Challenge20Status = await GetChallengeStatus(Identifier.Challenge20),
                };
            }
            throw new UnknownTeamException();
        }

        private async Task<Status> GetChallengeStatus(Identifier identifier)
        {
            Challenge challenge = await _dbContext.Challenges.Where(x => x.Status == Status.Successful).OrderByDescending(x => x.SolvedOn).FirstOrDefaultAsync(x => x.Identifier == identifier);
            return challenge != null && (identifier == Identifier.Challenge01 || challenge.SolvedOn >= DateTime.UtcNow.Subtract(TimeSpan.FromMinutes(5))) ? Status.Successful : Status.Unsuccessful;
        }

        public Task<OverallStatus> GetOverallPendingStatus()
        {
            return Task.FromResult(new OverallStatus());
        }
    }
}