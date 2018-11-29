using HTF2018.Backend.Common;
using HTF2018.Backend.Common.Exceptions;
using HTF2018.Backend.Common.Model;
using HTF2018.Backend.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HTF2018.Backend.Logic.Challenges.Helpers;
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

            
            question.InputValues.Add(new Value { Name = "encoded", Data = Encode(RandomStrings.ArtifactSentences[_randomGenerator.Next(RandomStrings.ArtifactSentences.Count)]) });

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
            if (answer.Values == null)
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
        public string Encode(string text)
        {
            var binary = "";
            foreach (var c in text)
            {
                binary += Convert.ToString(c, 2).PadLeft(8, '0');
            }
            return binary;
        }
        private static string Decode(string bytes)
        {
            var list = new List<byte>();

            for (var i = 0; i < bytes.Length; i += 8)
            {
                var t = bytes.Substring(i, 8);

                list.Add(Convert.ToByte(t, 2));
            }

            return Encoding.ASCII.GetString(list.ToArray());
        }
    }

}