using HTF2018.Backend.Common;
using HTF2018.Backend.Common.Exceptions;
using HTF2018.Backend.Common.Model;
using HTF2018.Backend.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HTF2018.Backend.Logic.Challenges
{
    /// <summary>
    /// CHALLENGE 08:
    ///   Geocoding
    /// </summary>
    public class Challenge13 : ChallengeBase, IChallenge13
    {

        public Challenge13(IHtfContext htfContext, ITeamLogic teamLogic, IChallengeLogic challengeLogic, IDashboardLogic dashboardLogic, IHistoryLogic historyLogic)
            : base(htfContext, teamLogic, challengeLogic, dashboardLogic, historyLogic) { }


        private readonly Random _randomGenerator = new Random();
        private readonly List<Coordinate> _artifactLocations = new List<Coordinate>
        {
            new Coordinate{ Latitude = 0, Longitude = 0}
        };
        public async Task<Challenge> GetChallenge()
        {
            var challenge = await BuildChallenge(Identifier.Challenge13);
            return challenge;
        }

        protected override Task<Question> BuildQuestion()
        {
            var random = _randomGenerator.Next(_artifactLocations.Count);
            var question = new Question
            {
                InputValues = new List<Value>
                {
                    new Value
                    {
                        Name = "double",
                        Data = $"{_artifactLocations[random].Latitude}"
                    },new Value
                    {
                        Name = "double",
                        Data = $"{_artifactLocations[random].Longitude}"
                    },

                    //add multiple values to make it more difficult
                }
            };

            return Task.FromResult(question);
        }

        protected override Task<Answer> BuildAnswer(Question question, Guid challengeId)
        {

            //var drawingId = int.Parse(question.InputValues);



            return Task.FromResult(new Answer
            {
                ChallengeId = challengeId,
                Values = new List<Value>
                {
                    //new Value{Name = "rectangles", Data = $"{Rectangles.Count(_rectangleStrings[drawingId])}"}
                }
            });
        }

        protected override async Task<Example> BuildExample(Guid challengeId)
        {
            Question question = new Question
            {
                // TODO: Add name-data pairs containing an example question based on the actual question!
            };

            return new Example
            {
                Question = question,
                Answer = await BuildAnswer(question, challengeId)
            };
        }

        protected override void ValidateAnswer(Answer answer)
        {
            Boolean invalid = false;

            // TODO: Do a basic validation of the answer object!
            // (Null-checks, are properties correct, but no actual functional checks)

            if (invalid)
            {
                throw new InvalidAnswerException();
            }
        }
    }

    public class Coordinate
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}