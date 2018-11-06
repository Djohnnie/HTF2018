using HTF2018.Backend.Common;
using HTF2018.Backend.Common.Model;
using HTF2018.Backend.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HTF2018.Backend.Logic.Challenges
{
    /// <summary>
    /// CHALLENGE 01:
    ///   Identify yourself.
    /// </summary>
    public class Challenge01 : ChallengeBase, IChallenge01
    {
        private readonly IChallengeLogic _challengeLogic;

        public Challenge01(IHtfContext htfContext, IChallengeLogic challengeLogic) : base(htfContext)
        {
            _challengeLogic = challengeLogic;
        }

        public async Task<Challenge> GetChallenge()
        {
            Guid challengeId = Guid.NewGuid();
            Challenge challenge = BuildChallenge(Identifier.Challenge01);
            await _challengeLogic.CreateChallenge(challengeId, Identifier.Challenge01);
            return challenge;
        }

        public Task<Response> ValidateChallenge(Answer answer, IHtfContext context)
        {
            
            return Task.FromResult(new Response
            {
                Identifier = Identifier.Challenge01,
                Overview = BuildOverview(),
                Status = Status.Unsuccessful
            });
        }

        protected override Question BuildQuestion()
        {
            return new Question
            {
                InputValues = new List<Value>
                {
                    new Value{ Name = "name", Data = "Use this parameter to provide a name for your team." },
                    new Value{ Name = "secret", Data = "Use this parameter to provide a secret for your team to authenticate with." }
                }
            };
        }

        protected override Example BuildExample(Guid challengeId)
        {
            return new Example
            {
                Question = BuildQuestion(),
                Answer = new Answer
                {
                    ChallengeId = challengeId,
                    Values = new List<Value>
                    {
                        new Value{ Name = "name", Data = "we_will_hack_the_future" },
                        new Value{ Name = "secret", Data = "twinkle, twinkle, little star!" }
                    }
                }
            };
        }
    }
}