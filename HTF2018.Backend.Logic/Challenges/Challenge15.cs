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
    public class Challenge15 : ChallengeBase, IChallenge15
    {
        /// <summary>
        /// CHALLENGE 05:
        /// Variable parameters
        /// </summary>
        public Challenge15(IHtfContext htfContext, ITeamLogic teamLogic, IChallengeLogic challengeLogic,
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

        private readonly string[] _formulas =
        {
            "180 * PI", "Pow(6,2)", "Sin(35)","Cos(25)","Tan(22)","Max(15,16)", "Min(5,9)", "Sqrt(49)",
            "240 * PI", "Pow(1,1)", "Sin(35)","Cos(25)","Tan(22)","Max(15,16)", "Min(5,9)", "Sqrt(24)",
            "360 * PI", "Pow(9,10)", "Sin(35)","Cos(25)","Tan(22)","Max(15,16)", "Min(5,9)", "Sqrt(36)",
            "90 * PI", "Pow(4,3)", "Sin(35)","Cos(25)","Tan(22)","Max(15,16)", "Min(5,9)", "Sqrt(116)"
        };

        public async Task<Challenge> GetChallenge()
        {
            
            var challenge = await BuildChallenge(Identifier.Challenge15);
            return challenge;
        }

        protected override Task<Question> BuildQuestion()
        {
            var question = new Question
            {
                InputValues = new List<Value>()
            };

            for (var j = 0; j < _randomGenerator.Next(10, 25); j++)
            {
                question.InputValues.Add(new Value
                {
                    Name = $"{(j == 0 ? "" : _operator[_randomGenerator.Next(_operator.Length)])}",
                    Data = $"{((_randomGenerator.Next(100) < 51) ? $"{_randomGenerator.Next(1, 20)}" : _formulas[_randomGenerator.Next(_formulas.Length)])}"
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
            e.Parameters.Add("PI", Math.PI);
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
                    },
                    new Value
                    {
                        Name = "+",
                        Data = "Min(10,21)"
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