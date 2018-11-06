using HTF2018.Backend.Common;
using HTF2018.Backend.Common.Extensions;
using HTF2018.Backend.Common.Model;
using System;

namespace HTF2018.Backend.Logic.Challenges
{
    public abstract class ChallengeBase
    {
        private readonly IHtfContext _htfContext;

        protected ChallengeBase(IHtfContext htfContext)
        {
            _htfContext = htfContext;
        }

        protected Challenge BuildChallenge(Identifier identifier)
        {
            Guid challengeId = Guid.NewGuid();
            Challenge challenge = new Challenge
            {
                Id = challengeId,
                Identifier = identifier,
                Title = Challenges.Titles[identifier],
                Description = Challenges.Descriptions[identifier],
                Question = BuildQuestion(),
                Example = BuildExample(challengeId)
            };
            return challenge;
        }

        protected abstract Question BuildQuestion();

        protected abstract Example BuildExample(Guid challengeId);

        protected Overview BuildOverview()
        {
            return new Overview
            {
                Challenge01 = new Progress { Entry = BuildEntry(Identifier.Challenge01), Status = Status.Unsuccessful },
                Challenge02 = new Progress { Entry = BuildEntry(Identifier.Challenge02), Status = Status.Unsuccessful },
                Challenge03 = new Progress { Entry = BuildEntry(Identifier.Challenge03), Status = Status.Unsuccessful },
                Challenge04 = new Progress { Entry = BuildEntry(Identifier.Challenge04), Status = Status.Unsuccessful },
                Challenge05 = new Progress { Entry = BuildEntry(Identifier.Challenge05), Status = Status.Unsuccessful },
                Challenge06 = new Progress { Entry = BuildEntry(Identifier.Challenge06), Status = Status.Unsuccessful },
                Challenge07 = new Progress { Entry = BuildEntry(Identifier.Challenge07), Status = Status.Unsuccessful },
                Challenge08 = new Progress { Entry = BuildEntry(Identifier.Challenge08), Status = Status.Unsuccessful },
                Challenge09 = new Progress { Entry = BuildEntry(Identifier.Challenge09), Status = Status.Unsuccessful },
                Challenge10 = new Progress { Entry = BuildEntry(Identifier.Challenge10), Status = Status.Unsuccessful },
                Challenge11 = new Progress { Entry = BuildEntry(Identifier.Challenge11), Status = Status.Unsuccessful },
                Challenge12 = new Progress { Entry = BuildEntry(Identifier.Challenge12), Status = Status.Unsuccessful },
                Challenge13 = new Progress { Entry = BuildEntry(Identifier.Challenge13), Status = Status.Unsuccessful },
                Challenge14 = new Progress { Entry = BuildEntry(Identifier.Challenge14), Status = Status.Unsuccessful },
                Challenge15 = new Progress { Entry = BuildEntry(Identifier.Challenge15), Status = Status.Unsuccessful },
                Challenge16 = new Progress { Entry = BuildEntry(Identifier.Challenge16), Status = Status.Unsuccessful },
                Challenge17 = new Progress { Entry = BuildEntry(Identifier.Challenge17), Status = Status.Unsuccessful },
                Challenge18 = new Progress { Entry = BuildEntry(Identifier.Challenge18), Status = Status.Unsuccessful },
                Challenge19 = new Progress { Entry = BuildEntry(Identifier.Challenge19), Status = Status.Unsuccessful },
                Challenge20 = new Progress { Entry = BuildEntry(Identifier.Challenge20), Status = Status.Unsuccessful },
            };
        }

        private String BuildEntry(Identifier identifier)
        {
            return $"{_htfContext.RequestUri}/{identifier.ToString().Md5Hash()}";
        }
    }
}