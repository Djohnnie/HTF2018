using HTF2018.Backend.Common.Model;
using System;
using System.Threading.Tasks;

namespace HTF2018.Backend.Logic.Interfaces
{
    public interface IChallengeEngine
    {
        Task<Challenge> GetChallenge(Identifier challengeIdentifier);
        Task<Challenge> GetChallenge(String challengeCode);
        Task<Response> ValidateChallenge(Answer answer);
    }
}