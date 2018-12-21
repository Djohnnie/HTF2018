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
    public class Challenge16 : ChallengeBase, IChallenge16
    {
        public Challenge16(IHtfContext htfContext, ITeamLogic teamLogic, IChallengeLogic challengeLogic, IDashboardLogic dashboardLogic, IHistoryLogic historyLogic)
            : base(htfContext, teamLogic, challengeLogic, dashboardLogic, historyLogic) { }

        private readonly Random _randomGenerator = new Random();
        public async Task<Challenge> GetChallenge()
        {
            var challenge = await BuildChallenge(Identifier.Challenge16);
            return challenge;
        }

        protected override Task<Question> BuildQuestion()
        {
            var number = _randomGenerator.Next(1, 4000);
            var question = new Question
            {
                InputValues = new List<Value>()
            };

            question.InputValues.Add(new Value { Name = "number", Data = $"{number}" });

            return Task.FromResult(question);
        }

        protected override Task<Answer> BuildAnswer(Question question, Guid challengeId)
        {

            var number = question.InputValues.Single(e => e.Name.Equals("number")).Data;
            var parsedValue = int.Parse(number);
            return Task.FromResult(new Answer
            {
                ChallengeId = challengeId,
                Values = new List<Value>
                {
                    new Value
                    {
                        Name = "number",
                        Data = $"{ToRoman(parsedValue)}"

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
                        Name = "number",
                        Data = $"{1994}"
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
            if (!answer.Values.Any(x => x.Name == "number"))
            {
                throw new InvalidAnswerException();
            }
            if (answer.Values.Count(x => x.Name == "number") != 1)
            {
                throw new InvalidAnswerException();
            }
            if (string.IsNullOrEmpty(answer.Values.Single(x => x.Name == "number").Data))
            {
                throw new InvalidAnswerException();
            }
        }
        public static string ToRoman(int number)
        {
            if (number < 1) return string.Empty;
            if (number >= 1000) return "M" + ToRoman(number - 1000);
            if (number >= 900) return "CM" + ToRoman(number - 900);
            if (number >= 500) return "D" + ToRoman(number - 500);
            if (number >= 400) return "CD" + ToRoman(number - 400);
            if (number >= 100) return "C" + ToRoman(number - 100);
            if (number >= 90) return "XC" + ToRoman(number - 90);
            if (number >= 50) return "L" + ToRoman(number - 50);
            if (number >= 40) return "XL" + ToRoman(number - 40);
            if (number >= 10) return "X" + ToRoman(number - 10);
            if (number >= 9) return "IX" + ToRoman(number - 9);
            if (number >= 5) return "V" + ToRoman(number - 5);
            if (number >= 4) return "IV" + ToRoman(number - 4);
            if (number >= 1) return "I" + ToRoman(number - 1);
            return string.Empty;
        }
        private static readonly Dictionary<char, int> RomanMap = new Dictionary<char, int>()
        {
            {'I', 1},
            {'V', 5},
            {'X', 10},
            {'L', 50},
            {'C', 100},
            {'D', 500},
            {'M', 1000}
        };

        public static int RomanToInteger(string roman)
        {
            var number = 0;
            for (var i = 0; i < roman.Length; i++)
            {
                if (i + 1 < roman.Length && RomanMap[roman[i]] < RomanMap[roman[i + 1]])
                {
                    number -= RomanMap[roman[i]];
                }
                else
                {
                    number += RomanMap[roman[i]];
                }
            }
            return number;
        }
    }
}