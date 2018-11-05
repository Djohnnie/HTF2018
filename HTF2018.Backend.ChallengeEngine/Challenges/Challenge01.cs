using HTF2018.Backend.ChallengeEngine.Model;
using HTF2018.Backend.Common;
using System;
using System.Collections.Generic;

namespace HTF2018.Backend.ChallengeEngine.Challenges
{
    /// <summary>
    /// CHALLENGE 01:
    ///   Identify yourself.
    /// </summary>
    public class Challenge01 : IChallenge
    {
        public Challenge GetChallenge()
        {
            return new Challenge
            {
                Id = Guid.NewGuid(),
                Identifier = Identifier.Challenge01,
                Title = "Identify yourself!",
                Description = "",
                Question = BuildQuestion(),
                Example = BuildExample()
            };
        }

        public Response ValidateChallenge(Answer answer, IHtfContext context)
        {
            return new Response
            {
                Identifier = Identifier.Challenge01,
                Overview = BuildOverview(context),
                Status = Status.Unknown
            };
        }

        private Question BuildQuestion()
        {
            return new Question();
        }

        private Example BuildExample()
        {
            return new Example
            {
                Question = null,
                Answer = new Answer
                {
                    Values = new List<Value>
                    {
                        new Value{ Name = "name", Data = "we_will_hack_the_future" },
                        new Value{ Name = "secret", Data = "twinkle, twinkle, little star!" }
                    }
                }
            };
        }

        private Overview BuildOverview(IHtfContext context)
        {
            return new Overview
            {
                Challenge01 = new Progress { Entry = context.RequestUri, Status = Status.Unknown },
                Challenge02 = new Progress { Entry = context.RequestUri, Status = Status.Unknown },
                Challenge03 = new Progress { Entry = context.RequestUri, Status = Status.Unknown },
                Challenge04 = new Progress { Entry = context.RequestUri, Status = Status.Unknown },
                Challenge05 = new Progress { Entry = context.RequestUri, Status = Status.Unknown },
                Challenge06 = new Progress { Entry = context.RequestUri, Status = Status.Unknown },
                Challenge07 = new Progress { Entry = context.RequestUri, Status = Status.Unknown },
                Challenge08 = new Progress { Entry = context.RequestUri, Status = Status.Unknown },
                Challenge09 = new Progress { Entry = context.RequestUri, Status = Status.Unknown },
                Challenge10 = new Progress { Entry = context.RequestUri, Status = Status.Unknown },
                Challenge11 = new Progress { Entry = context.RequestUri, Status = Status.Unknown },
                Challenge12 = new Progress { Entry = context.RequestUri, Status = Status.Unknown },
                Challenge13 = new Progress { Entry = context.RequestUri, Status = Status.Unknown },
                Challenge14 = new Progress { Entry = context.RequestUri, Status = Status.Unknown },
                Challenge15 = new Progress { Entry = context.RequestUri, Status = Status.Unknown },
                Challenge16 = new Progress { Entry = context.RequestUri, Status = Status.Unknown },
                Challenge17 = new Progress { Entry = context.RequestUri, Status = Status.Unknown },
                Challenge18 = new Progress { Entry = context.RequestUri, Status = Status.Unknown },
                Challenge19 = new Progress { Entry = context.RequestUri, Status = Status.Unknown },
                Challenge20 = new Progress { Entry = context.RequestUri, Status = Status.Unknown },
            };
        }
    }
}