using HTF2018.Backend.Common.Model;
using System;

namespace HTF2018.Backend.Logic.Interfaces
{
    public interface IChallengeEngine
    {
        Challenge GetChallenge(Identifier challengeIdentifier);
        Challenge GetChallenge(String challengeCode);
        Response ValidateChallenge(Answer answer);
    }
}