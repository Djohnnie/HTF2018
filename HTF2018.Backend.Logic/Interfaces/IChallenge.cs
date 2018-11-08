using System.Threading.Tasks;
using HTF2018.Backend.Common.Model;

namespace HTF2018.Backend.Logic.Interfaces
{
    public interface IChallenge
    {
        Task<Challenge> GetChallenge();

        Task<Response> ValidateChallenge(Answer answer);
    }

    public interface IChallenge01 : IChallenge { }
    public interface IChallenge02 : IChallenge { }
    public interface IChallenge03 : IChallenge { }
    public interface IChallenge04 : IChallenge { }
    public interface IChallenge05 : IChallenge { }
    public interface IChallenge06 : IChallenge { }
    public interface IChallenge07 : IChallenge { }
    public interface IChallenge08 : IChallenge { }
    public interface IChallenge09 : IChallenge { }
    public interface IChallenge10 : IChallenge { }
    public interface IChallenge11 : IChallenge { }
    public interface IChallenge12 : IChallenge { }
    public interface IChallenge13 : IChallenge { }
    public interface IChallenge14 : IChallenge { }
    public interface IChallenge15 : IChallenge { }
    public interface IChallenge16 : IChallenge { }
    public interface IChallenge17 : IChallenge { }
    public interface IChallenge18 : IChallenge { }
    public interface IChallenge19 : IChallenge { }
    public interface IChallenge20 : IChallenge { }
}