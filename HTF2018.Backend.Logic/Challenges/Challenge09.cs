using HTF2018.Backend.Common;
using HTF2018.Backend.Common.Exceptions;
using HTF2018.Backend.Common.Model;
using HTF2018.Backend.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NCalc;

namespace HTF2018.Backend.Logic.Challenges
{
    public class Challenge09 : ChallengeBase, IChallenge09
    {
        /// <summary>
        /// CHALLENGE 08:
        /// Variable parameters
        /// </summary>
        public Challenge09(IHtfContext htfContext, ITeamLogic teamLogic, IChallengeLogic challengeLogic,
            IDashboardLogic dashboardLogic, IHistoryLogic historyLogic)
            : base(htfContext, teamLogic, challengeLogic, dashboardLogic, historyLogic)
        {
        }

        private readonly Random _randomGenerator = new Random();

        private readonly string[] _operator = {
            "+", "-", "*", "/",
            "+", "-", "*", "-",
            "+", "-", "*", "+",
            "+", "-", "*", "*"
        };

        public async Task<Challenge> GetChallenge()
        {
            var challenge = await BuildChallenge(Identifier.Challenge09);
            return challenge;
        }

        protected override Task<Question> BuildQuestion()
        {
            var question = new Question
            {
                InputValues = new List<Value>()
            };

            for (var j = 0; j < _randomGenerator.Next(5, 50); j++)
            {
                question.InputValues.Add(new Value
                {
                    Name = $"{(j == 0 ? "" : _operator[_randomGenerator.Next(_operator.Length)])}",
                    Data = $"{_randomGenerator.Next(1, 20)}"
                });
            }

            return Task.FromResult(question);
        }

        protected override Task<Answer> BuildAnswer(Question question, Guid challengeId)
        {
            var formula = "";
            foreach (var inputValue in question.InputValues)
            {
                formula += $"{inputValue.Name}{inputValue.Data}";
            }

            var e = new Expression(formula);
            return Task.FromResult(new Answer
            {
                ChallengeId = challengeId,
                Values = new List<Value>
                {
                    new Value
                    {
                        Name = "answer",
                        Data = $"{e.Evaluate()}"
                    }, new Value
                    {
                        Name = "formula",
                        Data = $"{formula}"
                    }
                }
            });
        }

        protected override async Task<Example> BuildExample(Guid challengeId)
        {
            var question = new Question
            {
                InputValues = new List<Value>
                {
                    new Value
                    {
                        Name = "",
                        Data = "4"
                    },
                    new Value
                    {
                        Name = "+",
                        Data = "6"
                    }
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

            if (answer.Values != null)
            {
                throw new InvalidAnswerException();
            }

            if (!answer.Values.Any(x => x.Name == "answer" || x.Name == "formula"))
            {
                throw new InvalidAnswerException();
            }

            if (answer.Values.Count(x => x.Name == "answer") != 1)
            {
                throw new InvalidAnswerException();
            }
            if (answer.Values.Count(x => x.Name == "formula") != 1)
            {
                throw new InvalidAnswerException();
            }

            if (string.IsNullOrEmpty(answer.Values.Single(x => x.Name == "answer").Data))
            {
                throw new InvalidAnswerException();
            }

            if (string.IsNullOrEmpty(answer.Values.Single(x => x.Name == "formula").Data))
            {
                throw new InvalidAnswerException();
            }
        }
    }
}