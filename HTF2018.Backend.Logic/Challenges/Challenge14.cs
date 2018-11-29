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
    public class Challenge14 : ChallengeBase, IChallenge14
    {
        public Challenge14(IHtfContext htfContext, ITeamLogic teamLogic, IChallengeLogic challengeLogic, IDashboardLogic dashboardLogic, IHistoryLogic historyLogic)
            : base(htfContext, teamLogic, challengeLogic, dashboardLogic, historyLogic) { }

        private readonly Dictionary<string,string> _challengesWithCompanies = new Dictionary<string, string>
        {
            {".NET", "Involed"},
            {"Javascript", "Involved" },
            {"BI", "Agiliz" },
            {"Java", "c4j" },
            {"Blockchain", "TheLedger" },
            {"Mobile", "Cozmos" },
            {"Security", "Nynox" },
            {"SAP", "Flexso" },
            {"IT Decathlon", "To The point" },
            {"IoT & AI", "Craftworkz" },
            {"Big Data", "Big Industries" },
            {"Geosolutions", "GEO Solutions" },
            {"Hackaton", "Cronos" }
        };
        private readonly Random _randomGenerator = new Random();
        public async Task<Challenge> GetChallenge()
        {
            var challenge = await BuildChallenge(Identifier.Challenge14);
            return challenge;
        }

        protected override Task<Question> BuildQuestion()
        {
            var keys = _challengesWithCompanies.Keys.ToList();
            return Task.FromResult(new Question
            {
                InputValues = new List<Value>
                {
                    new Value{Name = "challenge", Data = $"{keys[_randomGenerator.Next(keys.Count-1)]}"}
                }
            });
        }

        protected override Task<Answer> BuildAnswer(Question question, Guid challengeId)
        {
            var challenge = question.InputValues.Single(e => e.Name.Equals("challenge")).Data;
            var answer = new Answer
            {
                ChallengeId = challengeId,
                Values = new List<Value>
                {
                    new Value{Name = "special ops", Data = _challengesWithCompanies[challenge].ToLower()}
                }
            };

            return Task.FromResult(answer);
        }

        protected override async Task<Example> BuildExample(Guid challengeId)
        {
            var question = new Question
            {
                InputValues = new List<Value> {
                  new Value{Name = "challenge", Data ="Hackaton"}}
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

            if (!answer.Values.Any(x => x.Name == "special ops"))
            {
                throw new InvalidAnswerException();
            }
            if (answer.Values.Count(x => x.Name == "special ops") != 1)
            {
                throw new InvalidAnswerException();
            }
            if (string.IsNullOrEmpty(answer.Values.Single(x => x.Name == "special ops").Data))
            {
                throw new InvalidAnswerException();
            }


        }
    }
}