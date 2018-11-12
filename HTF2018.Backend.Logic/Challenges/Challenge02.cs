using HTF2018.Backend.Common;
using HTF2018.Backend.Common.Exceptions;
using HTF2018.Backend.Common.Model;
using HTF2018.Backend.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team = HTF2018.Backend.DataAccess.Entities.Team;

namespace HTF2018.Backend.Logic.Challenges
{
    /// <summary>
    /// CHALLENGE 02:
    ///   Calculate the sum of all given integer values.
    /// </summary>
    public class Challenge02 : ChallengeBase, IChallenge02
    {
        private readonly Random _randomGenerator = new Random();
        private readonly IHtfContext _htfContext;
        private readonly IChallengeLogic _challengeLogic;
        private readonly ITeamLogic _teamLogic;

        public Challenge02(IHtfContext htfContext, IChallengeLogic challengeLogic, ITeamLogic teamLogic, IDashboardLogic dashboardLogic)
            : base(htfContext, challengeLogic, dashboardLogic)
        {
            _htfContext = htfContext;
            _challengeLogic = challengeLogic;
            _teamLogic = teamLogic;
        }

        public async Task<Challenge> GetChallenge()
        {
            Challenge challenge = await BuildChallenge(Identifier.Challenge02);
            return challenge;
        }

        public async Task<Response> ValidateChallenge(Answer answer)
        {
            ValidateAnswer(answer);

            Team team = await _teamLogic.FindTeamByIdentification(_htfContext.Identification);
            Answer storedAnswer = await _challengeLogic.GetAnswerByChallengeId(answer.ChallengeId);

            Status status = Status.Unsuccessful;
            if (CheckAnswer(answer, storedAnswer))
            {
                await _challengeLogic.SolveChallenge(answer.ChallengeId, team.Id);
                status = Status.Successful;
            }
            else
            {
                await _challengeLogic.FailChallenge(answer.ChallengeId, team.Id);
            }

            return new Response
            {
                Identifier = Identifier.Challenge02,
                Status = status,
                Identification = team.Identification,
                Overview = await BuildOverview(team.Id)
            };
        }

        protected override Question BuildQuestion()
        {
            var question = new Question
            {
                InputValues = new List<Value>()
            };

            for (int i = 0; i < _randomGenerator.Next(2, 10); i++)
            {
                question.InputValues.Add(new Value { Name = "i", Data = $"{_randomGenerator.Next(1, 100)}" });
            }

            return question;
        }

        protected override Answer BuildAnswer(Question question, Guid challengeId)
        {
            int sum = CalculateSum(question.InputValues);

            return new Answer
            {
                ChallengeId = challengeId,
                Values = new List<Value>
                {
                    new Value{ Name = "sum", Data = $"{sum}" }
                }
            };
        }

        protected override Example BuildExample(Guid challengeId)
        {
            Question question = new Question
            {
                InputValues = new List<Value>
                {
                    new Value{ Name = "i", Data = "385" },
                    new Value{ Name = "i", Data = "8269" },
                    new Value{ Name = "i", Data = "52" },
                }
            };

            return new Example
            {
                Question = question,
                Answer = BuildAnswer(question, challengeId)
            };
        }

        protected override void ValidateAnswer(Answer answer)
        {
            Boolean invalid = false;
            if (answer.Values == null) { invalid = true; }
            if (answer.Values.Count != 1) { invalid = true; }
            if (!answer.Values.Any(x => x.Name == "sum")) { invalid = true; }
            if (String.IsNullOrEmpty(answer.Values.Single(x => x.Name == "sum").Data)) { invalid = true; }

            if (invalid)
            {
                throw new InvalidAnswerException();
            }
        }

        private int CalculateSum(List<Value> inputValues)
        {
            int sum = 0;

            foreach (Value value in inputValues)
            {
                int number = Convert.ToInt32(value.Data);
                sum += number;
            }

            return sum;
        }
    }
}