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
    public class Challenge06 : ChallengeBase, IChallenge06
    {
        /// <summary>
        /// CHALLENGE 06:
        ///   The artifact is sending us messages again. See if you can decode them! This time they are sending a number with it, migt it be a key to decipher it?
        /// </summary>
        public Challenge06(IHtfContext htfContext, ITeamLogic teamLogic, IChallengeLogic challengeLogic, IDashboardLogic dashboardLogic, IHistoryLogic historyLogic)
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
            var challenge = await BuildChallenge(Identifier.Challenge06);
            return challenge;
        }

        protected override Question BuildQuestion()
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

            return question;
        }

        protected override Answer BuildAnswer(Question question, Guid challengeId)
        {
            var answers = new List<Value>();

            var cipherValue = question.InputValues.Find(e => e.Name.Equals(Cipher));
            var cipher = Convert.ToInt32(cipherValue.Data);
            foreach (var inputValue in question.InputValues)
            {
                answers.Add(
                    new Value { Name = "decoded", Data = Encode(inputValue.Data,-cipher) });
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
                    new Value{Name = Cipher, Data = "16"},
                    new Value{Name = "encoded", Data = Encode("Artifact",16)},
                    new Value{Name = "encoded", Data = Encode("Aliens",16)}
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