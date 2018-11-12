using HTF2018.Backend.Common.Model;
using System;
using System.Threading.Tasks;
using Challenge = HTF2018.Backend.DataAccess.Entities.Challenge;

namespace HTF2018.Backend.Logic.Interfaces
{
    public interface IChallengeLogic
    {
        Task<Challenge> GetChallengeById(Guid challengeId);
        Task<Answer> GetAnswerByChallengeId(Guid challengeId);
        Task<Challenge> CreateChallenge(Guid challengeId, Question question, Answer answer, Identifier identifier);
        Task<Challenge> SolveChallenge(Guid challengeId, Guid teamId);
        Task<Challenge> FailChallenge(Guid challengeId, Guid teamId);
    }
}