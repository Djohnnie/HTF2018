using System;
using HTF2018.Backend.ChallengeEngine.Model;

namespace HTF2018.Backend.Logic.Interfaces
{
    public interface IChallengeLogic
    {
        Challenge GetFirstChallenge();

        Challenge GetSubsequentChallenge(String challengeCode);

        Response ValidateChallenge(Answer answer);
    }
}