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
    /// CHALLENGE 02:
    ///   Calculate the sum of all given integer values.
    /// </summary>
    public class Challenge02 : ChallengeBase, IChallenge02
    {
        private readonly Random _randomGenerator = new Random();
        private readonly IChallengeLogic _challengeLogic;
        private readonly ITeamLogic _teamLogic;

        public Challenge02(IHtfContext htfContext, IChallengeLogic challengeLogic, ITeamLogic teamLogic) : base(htfContext)
        {
            _challengeLogic = challengeLogic;
            _teamLogic = teamLogic;
        }

        public async Task<Challenge> GetChallenge()
        {
            Challenge challenge = BuildChallenge(Identifier.Challenge02);
            await _challengeLogic.CreateChallenge(challenge.Id, Identifier.Challenge02);
            return challenge;
        }

        public Task<Response> ValidateChallenge(Answer answer)
        {
            throw new NotImplementedException();
        }

        protected override Question BuildQuestion()
        {
            var question = new Question
            {
                InputValues = new List<Value>()
            };

            for (int i = 0; i < _randomGenerator.Next(5, 10); i++)
            {
                question.InputValues.Add(new Value { Name = "i", Data = $"{_randomGenerator.Next(1, 100)}" });
            }

            return question;
        }

        protected override Example BuildExample(Guid challengeId)
        {
            return new Example
            {
                Question = new Question
                {
                    InputValues = new List<Value>
                    {
                        new Value{ Name = "i", Data = "385" },
                        new Value{ Name = "i", Data = "8269" },
                        new Value{ Name = "i", Data = "52" },
                    }
                },
                Answer = new Answer
                {
                    ChallengeId = challengeId,
                    Values = new List<Value>
                    {
                        new Value{ Name = "sum", Data = "8706" }
                    }
                }
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
    }
}