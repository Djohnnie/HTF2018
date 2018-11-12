using HTF2018.Backend.Common;
using HTF2018.Backend.Common.Model;
using HTF2018.Backend.Logic.Interfaces;
using System;
using System.Threading.Tasks;

namespace HTF2018.Backend.Logic.Challenges
{
    public class Challenge03 : ChallengeBase, IChallenge03
    {
        public Challenge03(IHtfContext htfContext, IChallengeLogic challengeLogic, ITeamLogic teamLogic, IDashboardLogic dashboardLogic)
            : base(htfContext, challengeLogic, dashboardLogic)
        {

        }

        public Task<Challenge> GetChallenge()
        {
            throw new NotImplementedException();
        }

        public Task<Response> ValidateChallenge(Answer answer)
        {
            throw new NotImplementedException();
        }

        protected override Answer BuildAnswer(Question question, Guid challengeId)
        {
            throw new NotImplementedException();
        }

        protected override Example BuildExample(Guid challengeId)
        {
            throw new NotImplementedException();
        }

        protected override Question BuildQuestion()
        {
            throw new NotImplementedException();
        }

        protected override void ValidateAnswer(Answer answer)
        {
            throw new NotImplementedException();
        }
    }
}