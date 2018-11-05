using HTF2018.Backend.ChallengeEngine.Model;
using HTF2018.Backend.Common;

namespace HTF2018.Backend.ChallengeEngine
{
    public interface IChallenge
    {
        Challenge GetChallenge();

        Response ValidateChallenge(Answer answer, IHtfContext context);
    }
}