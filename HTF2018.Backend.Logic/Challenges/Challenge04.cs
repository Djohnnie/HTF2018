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
    /// <summary>
    /// CHALLENGE 04:
    ///   Calculate all the prime numbers between the given range.
    /// </summary>
    public class Challenge04 : ChallengeBase, IChallenge04
    {
        private readonly Random _randomGenerator = new Random();
        public Challenge04(IHtfContext htfContext, ITeamLogic teamLogic, IChallengeLogic challengeLogic, IDashboardLogic dashboardLogic, IHistoryLogic historyLogic)
            : base(htfContext, teamLogic, challengeLogic, dashboardLogic, historyLogic) { }

        public async Task<Challenge> GetChallenge()
        {
            var challenge = await BuildChallenge(Identifier.Challenge04);
            return challenge;
        }

        protected override Task<Question> BuildQuestion()
        {
            var question = new Question
            {
                InputValues = new List<Value>()
            };


            var primeStart = _randomGenerator.Next(1000, 1000000);
            var primeRange = _randomGenerator.Next(1000, 5000);
            question.InputValues.Add(new Value { Name = "start", Data = $"{primeStart}" });
            question.InputValues.Add(new Value { Name = "end", Data = $"{primeStart + primeRange}" });
            return Task.FromResult(question);
        }

        protected override Task<Answer> BuildAnswer(Question question, Guid challengeId)
        {
            var primes = CalculatePrimes(question.InputValues);

            return Task.FromResult(new Answer
            {
                ChallengeId = challengeId,
                Values = primes
            });
        }

        protected override async Task<Example> BuildExample(Guid challengeId)
        {
            Question question = new Question
            {
                InputValues = new List<Value>
                {
                    new Value{Name = "start", Data = "0"},
                    new Value{Name = "end", Data = "100"}
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
            if (!answer.Values.Any(x => x.Name == "prime"))
            {
                throw new InvalidAnswerException();
            }

            if (answer.Values.Where(x => x.Name.Equals("prime")).Any(answerValue => string.IsNullOrEmpty(answerValue.Data)))
            {
                throw new InvalidAnswerException();
            }
        }

        #region Helpers

        private static List<Value> CalculatePrimes(List<Value> inputValues)
        {
            var start = Convert.ToInt32(inputValues.Find(e => e.Name.Equals("start")).Data);
            var end = Convert.ToInt32(inputValues.Find(e => e.Name.Equals("end")).Data);
            var primes = new List<Value>();
            for (var i = start; i < end; i++)
            {
                if (IsPrime(i))
                {
                    primes.Add(new Value { Name = "prime", Data = $"{i}" });
                }
            }

            return primes;
        }
        private static bool IsPrime(int number)
        {
            if ((number & 1) == 0)
            {
                return number == 2;
            }

            for (var i = 3; i * i <= number; i += 2)
            {
                if (number % i == 0)
                {
                    return false;
                }

            }

            return number != 1;
        }

        #endregion

    }
}