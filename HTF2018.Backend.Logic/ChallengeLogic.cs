using HTF2018.Backend.Common.Model;
using HTF2018.Backend.DataAccess;
using HTF2018.Backend.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Challenge = HTF2018.Backend.DataAccess.Entities.Challenge;

namespace HTF2018.Backend.Logic
{
    public class ChallengeLogic : IChallengeLogic
    {
        private readonly TheArtifactDbContext _dbContext;

        public ChallengeLogic(TheArtifactDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Challenge> GetChallengeById(Guid challengeId)
        {
            return _dbContext.Challenges.SingleOrDefaultAsync(x => x.Id == challengeId);
        }

        public async Task<Answer> GetAnswerByChallengeId(Guid challengeId)
        {
            Challenge challenge = await _dbContext.Challenges.SingleOrDefaultAsync(x => x.Id == challengeId);
            if (challenge != null)
            {
                return JsonConvert.DeserializeObject<Answer>(challenge.Answer);
            }
            return null;
        }

        public async Task<Challenge> CreateChallenge(Guid challengeId, Question question, Answer answer, Identifier identifier)
        {
            Challenge challenge = new Challenge
            {
                Id = challengeId,
                Identifier = identifier,
                Team = null,
                Question = JsonConvert.SerializeObject(question),
                Answer = JsonConvert.SerializeObject(answer)
            };
            await _dbContext.Challenges.AddAsync(challenge);
            await _dbContext.SaveChangesAsync();
            return challenge;
        }

        public Task<Challenge> SolveChallenge(Guid challengeId, Guid teamId)
        {
            return ChangeChallengeStatus(challengeId, teamId, Status.Successful);
        }

        public Task<Challenge> FailChallenge(Guid challengeId, Guid teamId)
        {
            return ChangeChallengeStatus(challengeId, teamId, Status.Unsuccessful);
        }

        private async Task<Challenge> ChangeChallengeStatus(Guid challengeId, Guid teamId, Status status)
        {
            var challenge = await _dbContext.Challenges.SingleOrDefaultAsync(x => x.Id == challengeId);
            challenge.Team = await _dbContext.Teams.SingleOrDefaultAsync(x => x.Id == teamId);
            challenge.SolvedOn = DateTime.UtcNow;
            challenge.Status = status;
            await _dbContext.SaveChangesAsync();
            return challenge;
        }
    }
}