using System;
using HTF2018.Backend.Common.Model;
using System.Threading.Tasks;

namespace HTF2018.Backend.Example.Challenges
{
    public interface IChallenge
    {
        Status Status { get; }

        Task Fetch(String endpoint);

        Task Solve();
    }
}