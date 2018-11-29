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
    public class Challenge05 : ChallengeBase, IChallenge05
    {
        /// <summary>
        /// CHALLENGE 05:
        ///  The artifact is trying to guess ages in on different planets. Can you help out which one it actually means?
        /// </summary>
        private readonly Random _randomGenerator = new Random();

        public Challenge05(IHtfContext htfContext, ITeamLogic teamLogic, IChallengeLogic challengeLogic,
            IDashboardLogic dashboardLogic, IHistoryLogic historyLogic)
            : base(htfContext, teamLogic, challengeLogic, dashboardLogic, historyLogic)
        {
        }

        public async Task<Challenge> GetChallenge()
        {
            var challenge = await BuildChallenge(Identifier.Challenge05);
            return challenge;
        }

        protected override Task<Question> BuildQuestion()
        {
            //Want to let them expierence the pain of a developer with exceptions! (Greater then maxint )
            double seconds = _randomGenerator.Next(1000000000, int.MaxValue);
            if (_randomGenerator.Next(3) == 1)
            {
                for (var i = 0; i < _randomGenerator.Next(5); i++)
                {
                    seconds += _randomGenerator.Next(1000000000, int.MaxValue);
                }
            }

            seconds = seconds * 1000;

            var planetArray = new[] {"earth", "jupiter", "venus", "uranus", "saturn", "neptune", "mercury", "mars"};
            var question = new Question
            {
                InputValues = new List<Value>
                {
                    new Value {Name = "ageInUniversalSeconds", Data = $"{seconds}"},
                    new Value {Name = "destinationPlanet", Data = planetArray[_randomGenerator.Next(planetArray.Length)]},
                    new Value {Name = "EarthSolarYear", Data = $"{SpaceAgeHelper.EarthSolarYear}"},
                    new Value {Name = "JupiterYearsPerEarthYear", Data = $"{SpaceAgeHelper.JupiterYearsPerEarthYear}"},
                    new Value {Name = "MarsYearsPerEarthYear", Data = $"{SpaceAgeHelper.MarsYearsPerEarthYear}"},
                    new Value {Name = "MercuryYearsPerEarthYear", Data = $"{SpaceAgeHelper.MercuryYearsPerEarthYear}"},
                    new Value {Name = "NeptuneYearsPerEarthYear", Data = $"{SpaceAgeHelper.NeptuneYearsPerEarthYear}"},
                    new Value {Name = "SaturnYearsPerEarthYear", Data = $"{SpaceAgeHelper.SaturnYearsPerEarthYear}"},
                    new Value {Name = "UranusYearsPerEarthYear", Data = $"{SpaceAgeHelper.UranusYearsPerEarthYear}"},
                    new Value {Name = "VenusYearsPerEarthYear", Data = $"{SpaceAgeHelper.VenusYearsPerEarthYear}"},
                }
            };


            return Task.FromResult(question);
        }

        protected override Task<Answer> BuildAnswer(Question question, Guid challengeId)
        {
            var preferredPlanetYears = question.InputValues.Find(e => e.Name.Equals("destinationPlanet"));
            var ageInSeconds = long.Parse(question.InputValues.Find(e => e.Name.Equals("ageInUniversalSeconds")).Data);
            var answer = "";
            var spaceAge = new SpaceAgeHelper(ageInSeconds);
            switch (preferredPlanetYears.Data)
            {
                case "earth":
                    answer = $"{spaceAge.OnEarth()}";
                    break;
                case "jupiter":
                    answer = $"{spaceAge.OnJupiter()}";
                    break;
                case "mars":
                    answer = $"{spaceAge.OnMars()}";
                    break;
                case "mercury":
                    answer = $"{spaceAge.OnMercury()}";
                    break;
                case "neptune":
                    answer = $"{spaceAge.OnNeptune()}";
                    break;
                case "saturn":
                    answer = $"{spaceAge.OnSaturn()}";
                    break;
                case "uranus":
                    answer = $"{spaceAge.OnUranus()}";
                    break;
                case "venus":
                    answer = $"{spaceAge.OnVenus()}";
                    break;
            }

            return Task.FromResult(new Answer
            {
                ChallengeId = challengeId,
                Values = new List<Value>
                {
                    new Value {Name = "ageInYears", Data = answer}
                }
            });
        }

        protected override async Task<Example> BuildExample(Guid challengeId)
        {
            var question = new Question
            {
                InputValues = new List<Value>
                {
                    new Value {Name = "ageInUniversalSeconds", Data = "10000000000"},
                    new Value {Name = "destinationPlanet", Data = "earth"},
                    new Value {Name = "EarthSolarYear", Data = $"{SpaceAgeHelper.EarthSolarYear}"},
                    new Value {Name = "JupiterYearsPerEarthYear", Data = $"{SpaceAgeHelper.JupiterYearsPerEarthYear}"},
                    new Value {Name = "MarsYearsPerEarthYear", Data = $"{SpaceAgeHelper.MarsYearsPerEarthYear}"},
                    new Value {Name = "MercuryYearsPerEarthYear", Data = $"{SpaceAgeHelper.MercuryYearsPerEarthYear}"},
                    new Value {Name = "NeptuneYearsPerEarthYear", Data = $"{SpaceAgeHelper.NeptuneYearsPerEarthYear}"},
                    new Value {Name = "SaturnYearsPerEarthYear", Data = $"{SpaceAgeHelper.SaturnYearsPerEarthYear}"},
                    new Value {Name = "UranusYearsPerEarthYear", Data = $"{SpaceAgeHelper.UranusYearsPerEarthYear}"},
                    new Value {Name = "VenusYearsPerEarthYear", Data = $"{SpaceAgeHelper.VenusYearsPerEarthYear}"},
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

            if (!answer.Values.Any(x => x.Name == "age"))
            {
                throw new InvalidAnswerException();
            }

            if (answer.Values.Count(x => x.Name == "age") != 1)
            {
                throw new InvalidAnswerException();
            }

            if (string.IsNullOrEmpty(answer.Values.Single(x => x.Name == "age").Data))
            {
                throw new InvalidAnswerException();
            }
        }
    }
}