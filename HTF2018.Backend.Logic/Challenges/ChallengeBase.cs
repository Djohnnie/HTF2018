using HTF2018.Backend.Common;
using HTF2018.Backend.Common.Extensions;
using HTF2018.Backend.Common.Model;
using HTF2018.Backend.Logic.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HTF2018.Backend.Logic.Challenges
{
    public abstract class ChallengeBase
    {
        private readonly IHtfContext _htfContext;
        private readonly IChallengeLogic _challengeLogic;
        private readonly IDashboardLogic _dashboardLogic;

        protected ChallengeBase(IHtfContext htfContext, IChallengeLogic challengeLogic, IDashboardLogic dashboardLogic)
        {
            _htfContext = htfContext;
            _challengeLogic = challengeLogic;
            _dashboardLogic = dashboardLogic;
        }

        protected async Task<Challenge> BuildChallenge(Identifier identifier)
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

            Answer answer = BuildAnswer(challenge.Question, challengeId);

            await _challengeLogic.CreateChallenge(challengeId, challenge.Question, answer, identifier);

            return challenge;
        }

        protected abstract Question BuildQuestion();

        protected abstract Answer BuildAnswer(Question question, Guid challengeId);

        protected abstract Example BuildExample(Guid challengeId);

        protected abstract void ValidateAnswer(Answer answer);

        protected Boolean CheckAnswer(Answer answer, Answer storedAnswer)
        {
            // The ChallengeId and number of values should be equal.
            Boolean result = answer.ChallengeId == storedAnswer.ChallengeId && answer.Values.Count == storedAnswer.Values.Count;

            // All keys should exist and all values should be equal.
            foreach (Value storedValue in storedAnswer.Values)
            {
                Value value = answer.Values.SingleOrDefault(x => x.Name == storedValue.Name);
                if (value == null) { result = false; break; }
                if (storedValue.Data != value.Data) { result = false; break; }
            }

            return result;
        }

        protected async Task<Overview> BuildOverview(Guid teamId)
        {
            var teamStatus = await _dashboardLogic.GetTeamStatus(teamId);

            return new Overview
            {
                Challenge01 = new Progress { Entry = BuildEntry(Identifier.Challenge01), Status = teamStatus.Challenge01Status },
                Challenge02 = new Progress { Entry = BuildEntry(Identifier.Challenge02), Status = teamStatus.Challenge02Status },
                Challenge03 = new Progress { Entry = BuildEntry(Identifier.Challenge03), Status = teamStatus.Challenge03Status },
                Challenge04 = new Progress { Entry = BuildEntry(Identifier.Challenge04), Status = teamStatus.Challenge04Status },
                Challenge05 = new Progress { Entry = BuildEntry(Identifier.Challenge05), Status = teamStatus.Challenge05Status },
                Challenge06 = new Progress { Entry = BuildEntry(Identifier.Challenge06), Status = teamStatus.Challenge06Status },
                Challenge07 = new Progress { Entry = BuildEntry(Identifier.Challenge07), Status = teamStatus.Challenge07Status },
                Challenge08 = new Progress { Entry = BuildEntry(Identifier.Challenge08), Status = teamStatus.Challenge08Status },
                Challenge09 = new Progress { Entry = BuildEntry(Identifier.Challenge09), Status = teamStatus.Challenge09Status },
                Challenge10 = new Progress { Entry = BuildEntry(Identifier.Challenge10), Status = teamStatus.Challenge10Status },
                Challenge11 = new Progress { Entry = BuildEntry(Identifier.Challenge11), Status = teamStatus.Challenge11Status },
                Challenge12 = new Progress { Entry = BuildEntry(Identifier.Challenge12), Status = teamStatus.Challenge12Status },
                Challenge13 = new Progress { Entry = BuildEntry(Identifier.Challenge13), Status = teamStatus.Challenge13Status },
                Challenge14 = new Progress { Entry = BuildEntry(Identifier.Challenge14), Status = teamStatus.Challenge14Status },
                Challenge15 = new Progress { Entry = BuildEntry(Identifier.Challenge15), Status = teamStatus.Challenge15Status },
                Challenge16 = new Progress { Entry = BuildEntry(Identifier.Challenge16), Status = teamStatus.Challenge16Status },
                Challenge17 = new Progress { Entry = BuildEntry(Identifier.Challenge17), Status = teamStatus.Challenge17Status },
                Challenge18 = new Progress { Entry = BuildEntry(Identifier.Challenge18), Status = teamStatus.Challenge18Status },
                Challenge19 = new Progress { Entry = BuildEntry(Identifier.Challenge19), Status = teamStatus.Challenge19Status },
                Challenge20 = new Progress { Entry = BuildEntry(Identifier.Challenge20), Status = teamStatus.Challenge20Status },
            };
        }

        private String BuildEntry(Identifier identifier)
        {
            if (identifier == Identifier.Challenge01)
            {
                return $"{_htfContext.RequestUri}";
            }
            else
            {
                return $"{_htfContext.RequestUri}/{identifier.ToString().Md5Hash()}";
            }
        }
    }
}