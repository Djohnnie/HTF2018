using System;
using System.Threading.Tasks;
using HTF2018.Backend.Common;
using HTF2018.Backend.Common.Model;
using HTF2018.Backend.Logic.Interfaces;

namespace HTF2018.Backend.Logic.Challenges
{
    public class Challenge11 : IChallenge11
    {
        public Task<Challenge> GetChallenge()
        {
            throw new NotImplementedException();
        }

        public Task<Response> ValidateChallenge(Answer answer, IHtfContext context)
        {
            throw new NotImplementedException();
        }
    }
}