using HTF2018.Backend.Common;
using HTF2018.Backend.Common.Exceptions;
using HTF2018.Backend.Common.Extensions;
using HTF2018.Backend.Common.Model;
using HTF2018.Backend.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.String;

namespace HTF2018.Backend.Logic.Challenges
{
    /// <summary>
    /// CHALLENGE 01:
    ///   Identify yourself.
    /// </summary>
    public class Challenge01 : ChallengeBase, IChallenge01
    {
        private readonly ITeamLogic _teamLogic;
        private readonly IChallengeLogic _challengeLogic;
        private readonly IHistoryLogic _historyLogic;

        public Challenge01(IHtfContext htfContext, ITeamLogic teamLogic, IChallengeLogic challengeLogic,
            IDashboardLogic dashboardLogic, IHistoryLogic historyLogic)
            : base(htfContext, teamLogic, challengeLogic, dashboardLogic, historyLogic)
        {
            _historyLogic = historyLogic;
            _teamLogic = teamLogic;
            _challengeLogic = challengeLogic;
        }

        public async Task<Challenge> GetChallenge()
        {
            var challenge = await BuildChallenge(Identifier.Challenge01);
            return challenge;
        }

        public override async Task<Response> ValidateChallenge(Answer answer, Identifier identifier)
        {
            ValidateAnswer(answer);

            var teamName = answer.Values.Single(x => x.Name == "name").Data;
            var teamSecret = answer.Values.Single(x => x.Name == "secret").Data;
            var team = await _teamLogic.FindTeamByName(teamName);
            if (team == null)
            {
                team = await _teamLogic.CreateTeam(teamName, teamSecret);
                await _challengeLogic.SolveChallenge(answer.ChallengeId, team.Id);
            }
            else
            {
                if (team.HashedSecret != teamSecret.Md5Hash())
                {
                    throw new InvalidTeamException();
                }

                await _challengeLogic.SolveChallenge(answer.ChallengeId, team.Id);
                await _historyLogic.Push(Status.Successful);
            }

            return new Response
            {
                Identifier = identifier,
                Status = Status.Successful,
                Identification = team.Identification,
                Overview = await BuildOverview(team.Id)
            };
        }

        protected override Task<Question> BuildQuestion()
        {
            return Task.FromResult(new Question
            {
                InputValues = new List<Value>
                {
                    new Value {Name = "name", Data = "Use this parameter to provide a name for your team."},
                    new Value
                    {
                        Name = "secret",
                        Data = "Use this parameter to provide a secret for your team to authenticate with."
                    }
                }
            });
        }

        protected override Task<Answer> BuildAnswer(Question question, Guid challengeId)
        {
            return Task.FromResult(new Answer
            {
                ChallengeId = challengeId,
                Values = new List<Value>
                {
                    new Value {Name = "name", Data = "we_will_hack_the_future"},
                    new Value {Name = "secret", Data = "twinkle, twinkle, little star!"}
                }
            });
        }

        protected override async Task<Example> BuildExample(Guid challengeId)
        {
            Question question = await BuildQuestion();
            return new Example
            {
                Question = question,
                Answer = await BuildAnswer(question, challengeId)
            };
        }

        protected override void ValidateAnswer(Answer answer)
        {
            if (answer.Values == null)
            {
                throw new InvalidAnswerException();
            }

            if (answer.Values != null && answer.Values.Count != 2)
            {
                throw new InvalidAnswerException();
            }

            if (!answer.Values.Any(x => x.Name == "name"))
            {
                throw new InvalidAnswerException();
            }

            if (!answer.Values.Any(x => x.Name == "secret"))
            {
                throw new InvalidAnswerException();
            }

            if (IsNullOrEmpty(answer.Values.Single(x => x.Name == "name").Data))
            {
                throw new InvalidAnswerException();
            }

            if (IsNullOrEmpty(answer.Values.Single(x => x.Name == "secret").Data))
            {
                throw new InvalidAnswerException();
            }
        }
    }
}