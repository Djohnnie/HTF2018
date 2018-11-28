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
    public class Challenge03 : ChallengeBase, IChallenge03
    {
        /// <summary>
        /// CHALLENGE 03:
        ///   The artifact is sending us messages. See if you can decode them!
        /// </summary>
        public Challenge03(IHtfContext htfContext, ITeamLogic teamLogic, IChallengeLogic challengeLogic, IDashboardLogic dashboardLogic, IHistoryLogic historyLogic)
            : base(htfContext, teamLogic, challengeLogic, dashboardLogic, historyLogic) { }

        private readonly Random _randomGenerator = new Random();
       
        public async Task<Challenge> GetChallenge()
        {
            var challenge = await BuildChallenge(Identifier.Challenge03);
            return challenge;
        }

        protected override Task<Question> BuildQuestion()
        {
            var question = new Question
            {
                InputValues = new List<Value>()
            };

            question.InputValues.Add(new Value { Name = "encoded", Data = Encode(RandomStrings.ArtifactSentences[_randomGenerator.Next(RandomStrings.ArtifactSentences.Count)]) });

            return Task.FromResult(question);
        }

        protected override Task<Answer> BuildAnswer(Question question, Guid challengeId)
        {
            var answers = new List<Value>();
            foreach (var inputValue in question.InputValues)
            {
                answers.Add(
                    new Value { Name = "decoded", Data = Encode(inputValue.Data) });
            }
            return Task.FromResult(new Answer
            {
                ChallengeId = challengeId,
                Values = answers
            });
        }

        protected override async Task<Example> BuildExample(Guid challengeId)
        {
            var question = new Question
            {
                InputValues = new List<Value> {
                    new Value{Name = "encoded", Data = Encode("Are you trying to crack this?")},
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
            if(answer.Values == null)
            {
                throw new InvalidAnswerException();
            }
            if (answer.Values != null)
            {
                throw new InvalidAnswerException();
            }
            if (answer.Values.All(x => x.Name != "decoded"))
            {
                throw new InvalidAnswerException();
            }
            if (answer.Values.Count(x => x.Name == "decoded") != 1)
            {
                throw new InvalidAnswerException();
            }
            if (string.IsNullOrEmpty(answer.Values.Single(x => x.Name == "decoded").Data))
            {
                throw new InvalidAnswerException();
            }
        }

        private static string Encode(string plainText)
        {
            return string.Concat(plainText.Where(c=> char.IsLetterOrDigit(c)||char.IsWhiteSpace(c)).Select(EncodeChar));
        }

        private static char EncodeChar(char c)
        {
            return char.IsWhiteSpace(c) ? c : char.IsDigit(c) ? c : (char)('z' - char.ToLower(c) + 'a');
        }
    }
}