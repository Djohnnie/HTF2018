using HTF2018.Backend.Common;
using HTF2018.Backend.Common.Exceptions;
using HTF2018.Backend.Common.Model;
using HTF2018.Backend.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HTF2018.Backend.Logic.Challenges
{
    public class Challenge17 : ChallengeBase, IChallenge17
    {
        public Challenge17(IHtfContext htfContext, ITeamLogic teamLogic, IChallengeLogic challengeLogic, IDashboardLogic dashboardLogic, IHistoryLogic historyLogic)
            : base(htfContext, teamLogic, challengeLogic, dashboardLogic, historyLogic) { }

        private readonly string[] _leaders = {"Seppe", "Tim", "Johnny"};
        public async Task<Challenge> GetChallenge()
        {
            var challenge = await BuildChallenge(Identifier.Challenge17);
            return challenge;
        }

        protected override Task<Question> BuildQuestion()
        {
            return Task.FromResult(new Question
            {
                InputValues = new List<Value>()
                {
                    new Value{Name = "leader", Data = "challenge"}
                }
            });
        }

        protected override Task<Answer> BuildAnswer(Question question, Guid challengeId)
        {
            var leaderOfWhat = question.InputValues.Single(e => e.Name.Equals("leader")).Data;
            var answer = new Answer
            {
                ChallengeId = challengeId,
                Values = new List<Value>()
            };
            if (leaderOfWhat.Equals("challenge"))
            {
                foreach (var leader in _leaders)
                {
                    answer.Values.Add(new Value{Name = "name",Data = leader});
                }
            }
            else
            {
                answer.Values.Add(new Value { Name = "name", Data = "Jan" });
            }
            return Task.FromResult(answer);
        }

        protected override async Task<Example> BuildExample(Guid challengeId)
        {
            var question = new Question
            {
              InputValues = new List<Value> {
                  new Value{Name = "leader", Data = "Hack The Future"}}
            };

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

            if (!answer.Values.Any(x => x.Name == "leader"))
            {
                throw new InvalidAnswerException();
            }

            foreach (var value in answer.Values)
            {
                if (string.IsNullOrEmpty(value.Data))
                {
                    throw new InvalidAnswerException();
                }
            }
        }
    }
}