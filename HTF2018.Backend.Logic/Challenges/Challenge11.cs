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
    public class Challenge11 : ChallengeBase, IChallenge11
    {
        /// <summary>
        /// CHALLENGE 11:
        ///   The artifact is sending us messages again. See if you can decode them! This time they are sending a number with it, migt it be a key to decipher it?
        /// </summary>
        public Challenge11(IHtfContext htfContext, ITeamLogic teamLogic, IChallengeLogic challengeLogic, IDashboardLogic dashboardLogic, IHistoryLogic historyLogic)
            : base(htfContext, teamLogic, challengeLogic, dashboardLogic, historyLogic) { }

        private const string Cipher = "cipher";
        private readonly Random _randomGenerator = new Random();
        private readonly List<string> _artifactSentences = new List<string>
        {
            //To Change
            "The artifact has landed on a sacred place.",
            "We chose this location as the one with the biggest impact.",
            "The humans are trying to decipher our language!",
            "The artifact is getting breached, adapt!"
        };
        public async Task<Challenge> GetChallenge()
        {
            var challenge = await BuildChallenge(Identifier.Challenge11);
            return challenge;
        }

        protected override Task<Question> BuildQuestion()
        {

            var cipher = _randomGenerator.Next(1, 27);
            var question = new Question
            {
                InputValues = new List<Value>
                {
                    new Value { Name = "encoded", Data = Encode(_artifactSentences[_randomGenerator.Next(_artifactSentences.Count)],cipher)},
                    new Value { Name = Cipher, Data = $"{cipher}"}
                }
            };

            return Task.FromResult(question);
        }

        protected override Task<Answer> BuildAnswer(Question question, Guid challengeId)
        {
            var answers = new List<Value>();

            var cipherValue = question.InputValues.Find(e => e.Name.Equals(Cipher));
            var cipher = Convert.ToInt32(cipherValue.Data);
            foreach (var inputValue in question.InputValues.Where(e=>e.Name=="encoded"))
            {
                answers.Add(
                    new Value { Name = "decoded", Data = Encode(inputValue.Data, -cipher) });
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
                    new Value{Name = Cipher, Data = "16"},
                    new Value{Name = "encoded", Data = Encode("Artifact",16)},
                    new Value{Name = "encoded", Data = Encode("Aliens",16)}
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
            if (!answer.Values.Any(x => x.Name == "decoded"))
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

        private static string Encode(string value, int shift)
        {
            var buffer = value.ToCharArray();
            for (var i = 0; i < buffer.Length; i++)
            {
                char letter = buffer[i];
                letter = (char)(letter + shift);
                if (letter > 'z')
                {
                    letter = (char)(letter - 26);
                }
                else if (letter < 'a')
                {
                    letter = (char)(letter + 26);
                }
                buffer[i] = letter;
            }
            return new string(buffer);
        }
    }
}