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
    public class Challenge04 : ChallengeBase, IChallenge04
    {
        /// <summary>
        /// CHALLENGE 04:
        ///   The artifact is sending us messages. See if you can decode them!
        /// </summary>
        public Challenge04(IHtfContext htfContext, ITeamLogic teamLogic, IChallengeLogic challengeLogic, IDashboardLogic dashboardLogic, IHistoryLogic historyLogic)
            : base(htfContext, teamLogic, challengeLogic, dashboardLogic, historyLogic) { }

        private readonly Random _randomGenerator = new Random();
        private readonly List<string> _artifactSentences = new List<string>
        {
            "The artifact has landed on a sacred place.",
            "We chose this location as the one with the biggest impact.",
            "The humans are trying to decipher our language!",
            "The artifact is getting breached, adapt!"
        };
        public async Task<Challenge> GetChallenge()
        {
            var challenge = await BuildChallenge(Identifier.Challenge04);
            return challenge;
        }

        protected override Question BuildQuestion()
        {
            var question = new Question
            {
                InputValues = new List<Value>()
            };

            question.InputValues.Add(new Value { Name = "encoded", Data = Encode(_artifactSentences[_randomGenerator.Next(_artifactSentences.Count)]) });

            return question;
        }

        protected override Answer BuildAnswer(Question question, Guid challengeId)
        {
            var answers = new List<Value>();
            foreach (var inputValue in question.InputValues)
            {
                answers.Add(
                    new Value { Name = "decoded", Data = Encode(inputValue.Data) });
            }
            return new Answer
            {
                ChallengeId = challengeId,
                Values = answers
            };
        }

        protected override Example BuildExample(Guid challengeId)
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
                Answer = BuildAnswer(question, challengeId)
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

        public static string Encode(string plainText)
        {
            return string.Concat(plainText.Where(char.IsLetterOrDigit).Select((c, i) => (i % 5 == 0 && i > 0 ? " " : "") + EncodeChar(c)));
        }

        private static char EncodeChar(char c)
        {
            return char.IsDigit(c) ? c : (char)('z' - char.ToLower(c) + 'a');
        }
    }
}