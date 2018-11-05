using HTF2018.Backend.Common;
using HTF2018.Backend.Common.Model;

namespace HTF2018.Backend.Logic.Interfaces
{
    public interface IChallenge
    {
        Challenge GetChallenge();

        Response ValidateChallenge(Answer answer, IHtfContext context);
    }
}