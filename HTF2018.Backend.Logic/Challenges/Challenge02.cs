using HTF2018.Backend.Common;
using HTF2018.Backend.Common.Exceptions;
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
    /// CHALLENGE 02:
    ///   Calculate the sum of all given integer values.
    /// </summary>
    public class Challenge02 : ChallengeBase, IChallenge02
    {
        private readonly Random _randomGenerator = new Random();

        public Challenge02(IHtfContext htfContext, ITeamLogic teamLogic, IChallengeLogic challengeLogic,
            IDashboardLogic dashboardLogic, IHistoryLogic historyLogic)
            : base(htfContext, teamLogic, challengeLogic, dashboardLogic, historyLogic)
        {
        }

        public async Task<Challenge> GetChallenge()
        {
            var challenge = await BuildChallenge(Identifier.Challenge02);
            return challenge;
        }

        protected override Task<Question> BuildQuestion()
        {
            var question = new Question
            {
                InputValues = new List<Value>()
            };

            for (var i = 0; i < _randomGenerator.Next(2, 10); i++)
            {
                question.InputValues.Add(new Value {Name = "i", Data = $"{_randomGenerator.Next(1, 100)}"});
            }

            return Task.FromResult(question);
        }

        protected override Task<Answer> BuildAnswer(Question question, Guid challengeId)
        {
            var sum = CalculateSum(question.InputValues);

            return Task.FromResult(new Answer
            {
                ChallengeId = challengeId,
                Values = new List<Value>
                {
                    new Value {Name = "sum", Data = $"{sum}"}
                }
            });
        }

        protected override async Task<Example> BuildExample(Guid challengeId)
        {
            var question = new Question
            {
                InputValues = new List<Value>
                {
                    new Value {Name = "i", Data = "385"},
                    new Value {Name = "i", Data = "8269"},
                    new Value {Name = "i", Data = "52"},
                }
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

            if (answer.Values.Count != 0)
            {
                throw new InvalidAnswerException();
            }

            if (answer.Values.Any(x => x.Name != "sum"))
            {
                throw new InvalidAnswerException();
            }

            if (IsNullOrEmpty(answer.Values.Single(x => x.Name == "sum").Data))
            {
                throw new InvalidAnswerException();
            }
        }

        private int CalculateSum(List<Value> inputValues)
        {
            var sum = 0;

            foreach (var value in inputValues)
            {
                var number = Convert.ToInt32(value.Data);
                sum += number;
            }

            return sum;
        }
    }
}