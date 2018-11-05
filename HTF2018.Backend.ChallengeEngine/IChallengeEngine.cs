using HTF2018.Backend.ChallengeEngine.Model;

namespace HTF2018.Backend.ChallengeEngine
{
    public interface IChallengeEngine
    {
        Challenge GetChallenge(Identifier challengeIdentifier);

        Response ValidateChallenge(Answer answer);
    }
}