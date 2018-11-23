using HTF2018.Backend.Common;
using HTF2018.Backend.Common.Exceptions;
using HTF2018.Backend.Common.Model;
using HTF2018.Backend.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HTF2018.Backend.Logic.Challenges.Helpers;

namespace HTF2018.Backend.Logic.Challenges
{
    public class Challenge07 : ChallengeBase, IChallenge07
    {

        public Challenge07(IHtfContext htfContext, ITeamLogic teamLogic, IChallengeLogic challengeLogic, IDashboardLogic dashboardLogic, IHistoryLogic historyLogic)
            : base(htfContext, teamLogic, challengeLogic, dashboardLogic, historyLogic) { }

        private readonly Random _randomGenerator = new Random();
        public async Task<Challenge> GetChallenge()
        {
            var challenge = await BuildChallenge(Identifier.Challenge07);
            return challenge;
        }

        protected override Task<Question> BuildQuestion()
        {
            var number = _randomGenerator.Next(int.MaxValue);
            var value = (_randomGenerator.Next(100) < 51) ? $"{number}" : NumberToEnglish.ChangeNumericToWords(number);
           var question = new Question
            {
                InputValues = new List<Value>()
                {
                    new Value
                    {
                        Name = "number",
                        Data = $"{value}"
                    },
                    new Value
                    {
                        Name = "hash",
                        Data = $"{StringEncrypt.Encrypt($"{number}","seppeisthebest")}"
                    },
                }
            };
            return Task.FromResult(question);
        }

        protected override Task<Answer> BuildAnswer(Question question, Guid challengeId)
        {

            var hash = question.InputValues.Single(e => e.Name.Equals("hash")).Data;

            return Task.FromResult(new Answer
            {
                ChallengeId = challengeId,
                Values = new List<Value>
                {
                    new Value
                    {
                        Name = "number",
                        Data = $"{StringEncrypt.Decrypt(hash,"seppeisthebest")}"
                  
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
                        Data = $"{1}"
                    },
                    new Value
                    {
                        Name = "hash",
                        Data = $"{StringEncrypt.Encrypt($"{1}","seppeisthebest")}"
                    },
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
    }
}