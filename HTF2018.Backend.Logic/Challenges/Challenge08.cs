using HTF2018.Backend.Common;
using HTF2018.Backend.Common.Exceptions;
using HTF2018.Backend.Common.Model;
using HTF2018.Backend.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing;

namespace HTF2018.Backend.Logic.Challenges
{
    public class Challenge08 : ChallengeBase, IChallenge08
    {
        /// <summary>
        /// CHALLENGE 08:
        ///  Binary to ASCII
        /// </summary>
        
        public Challenge08(IHtfContext htfContext, ITeamLogic teamLogic, IChallengeLogic challengeLogic, IDashboardLogic dashboardLogic, IHistoryLogic historyLogic)
            : base(htfContext, teamLogic, challengeLogic, dashboardLogic, historyLogic) { }

        private readonly Random _randomGenerator = new Random();

        // TODO: Add strings to decode
        private readonly List<string> _artifactSentences = new List<string>
        {
            "The artifact has landed on a sacred place.",
            "We chose this location as the one with the biggest impact.",
            "The humans are trying to decipher our language!",
            "The artifact is getting breached, adapt!"
        };
        public async Task<Challenge> GetChallenge()
        {
            var challenge = await BuildChallenge(Identifier.Challenge08);
            return challenge;
        }

        protected override Task<Question> BuildQuestion()
        {
            var question = new Question
            {
                InputValues = new List<Value>()
            };

            question.InputValues.Add(new Value { Name = "encoded", Data = Encode(_artifactSentences[_randomGenerator.Next(_artifactSentences.Count)]) });

            return Task.FromResult(question);
        }

        protected override Task<Answer> BuildAnswer(Question question, Guid challengeId)
        {
            var answers = new List<Value>();
            foreach (var inputValue in question.InputValues)
            {
                answers.Add(
                    new Value { Name = "decoded", Data = Decode(inputValue.Data) });
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
                    new Value{Name = "encoded", Data = Encode("Artifact")},
                    new Value{Name = "encoded", Data = Encode("Aliens")}
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
            var invalid = answer.Values == null;
            if (answer.Values != null) { invalid = true; }
            if (!answer.Values.Any(x => x.Name == "decoded")) { invalid = true; }
            foreach (var answerValue in answer.Values.Where(x => x.Name.Equals("decoded")))
            {
                if (string.IsNullOrEmpty(answerValue.Data))
                    invalid = true;
            }
            if (invalid)
            {
                throw new InvalidAnswerException();
            }
        }

        private string Encode(string text)
        {
            return Encoding.ASCII.GetString(Encoding.ASCII.GetBytes(text));
        }
        private string Decode(string bytes)
        {
            return Encoding.ASCII.GetString(Encoding.ASCII.GetBytes(bytes));
        }
    }

}