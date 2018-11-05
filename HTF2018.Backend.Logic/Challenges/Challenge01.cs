using HTF2018.Backend.Common;
using HTF2018.Backend.Common.Model;
using HTF2018.Backend.Logic.Interfaces;
using System;
using System.Collections.Generic;

namespace HTF2018.Backend.Logic.Challenges
{
    /// <summary>
    /// CHALLENGE 01:
    ///   Identify yourself.
    /// </summary>
    public class Challenge01 : ChallengeBase, IChallenge
    {
        protected Challenge01(IHtfContext htfContext) : base(htfContext)
        {

        }

        public Challenge GetChallenge()
        {
            return new Challenge
            {
                Id = Guid.NewGuid(),
                Identifier = Identifier.Challenge01,
                Title = ChallengeTitles.Challenge01,
                Description = ChallengeDescriptions.Challenge01,
                Question = BuildQuestion(),
                Example = BuildExample()
            };
        }

        public Response ValidateChallenge(Answer answer, IHtfContext context)
        {
            return new Response
            {
                Identifier = Identifier.Challenge01,
                Overview = BuildOverview(),
                Status = Status.Unknown
            };
        }

        private Question BuildQuestion()
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

        private Example BuildExample()
        {
            return new Example
            {
                Question = BuildQuestion(),
                Answer = new Answer
                {
                    ChallengeId = Guid.NewGuid(),
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