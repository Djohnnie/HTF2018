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

            var planetArray = new[] {"earth", "jupiter", "venus", "uranus", "saturn", "neptune", "mercury", "mars"};
            var question = new Question
            {
                InputValues = new List<Value>
                {
                    new Value {Name = "age", Data = $"{seconds}"},
                    new Value {Name = "planet", Data = planetArray[_randomGenerator.Next(planetArray.Length)]},
                    new Value {Name = "EarthSolarYear", Data = $"{SpaceAge.EarthSolarYear}"},
                    new Value {Name = "JupiterYearsPerEarthYear", Data = $"{SpaceAge.JupiterYearsPerEarthYear}"},
                    new Value {Name = "MarsYearsPerEarthYear", Data = $"{SpaceAge.MarsYearsPerEarthYear}"},
                    new Value {Name = "MercuryYearsPerEarthYear", Data = $"{SpaceAge.MercuryYearsPerEarthYear}"},
                    new Value {Name = "NeptuneYearsPerEarthYear", Data = $"{SpaceAge.NeptuneYearsPerEarthYear}"},
                    new Value {Name = "SaturnYearsPerEarthYear", Data = $"{SpaceAge.SaturnYearsPerEarthYear}"},
                    new Value {Name = "UranusYearsPerEarthYear", Data = $"{SpaceAge.UranusYearsPerEarthYear}"},
                    new Value {Name = "VenusYearsPerEarthYear", Data = $"{SpaceAge.VenusYearsPerEarthYear}"},
                }
            };


            return Task.FromResult(question);
        }

        protected override Task<Answer> BuildAnswer(Question question, Guid challengeId)
        {
            var preferredPlanetYears = question.InputValues.Find(e => e.Name.Equals("planet"));
            var ageInSeconds = int.Parse(question.InputValues.Find(e => e.Name.Equals("age")).Data);
            var answer = "";
            var spaceAge = new SpaceAge(ageInSeconds);
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

            answer = answer + " years";
            return Task.FromResult(new Answer
            {
                ChallengeId = challengeId,
                Values = new List<Value>
                {
                    new Value {Name = "age", Data = answer}
                }
            });
        }

        protected override async Task<Example> BuildExample(Guid challengeId)
        {
            var question = new Question
            {
                InputValues = new List<Value>
                {
                    new Value {Name = "age", Data = "1000000"},
                    new Value {Name = "planet", Data = "earth"},
                    new Value {Name = "EarthSolarYear", Data = $"{SpaceAge.EarthSolarYear}"},
                    new Value {Name = "JupiterYearsPerEarthYear", Data = $"{SpaceAge.JupiterYearsPerEarthYear}"},
                    new Value {Name = "MarsYearsPerEarthYear", Data = $"{SpaceAge.MarsYearsPerEarthYear}"},
                    new Value {Name = "MercuryYearsPerEarthYear", Data = $"{SpaceAge.MercuryYearsPerEarthYear}"},
                    new Value {Name = "NeptuneYearsPerEarthYear", Data = $"{SpaceAge.NeptuneYearsPerEarthYear}"},
                    new Value {Name = "SaturnYearsPerEarthYear", Data = $"{SpaceAge.SaturnYearsPerEarthYear}"},
                    new Value {Name = "UranusYearsPerEarthYear", Data = $"{SpaceAge.UranusYearsPerEarthYear}"},
                    new Value {Name = "VenusYearsPerEarthYear", Data = $"{SpaceAge.VenusYearsPerEarthYear}"},
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
            if (answer.Values != null)
            {
                invalid = true;
            }

            if (!answer.Values.Any(x => x.Name == "age"))
            {
                invalid = true;
            }

            if (answer.Values.Count(x => x.Name == "age") != 1)
            {
                invalid = true;
            }

            foreach (var answerValue in answer.Values.Where(x => x.Name.Equals("age")))
            {
                if (string.IsNullOrEmpty(answerValue.Data))
                    invalid = true;
            }

            if (invalid)
            {
                throw new InvalidAnswerException();
            }
        }
    }

    public class SpaceAge
    {
        #region Constants

        public const double EarthSolarYear = 31557600;
        public const double MercuryYearsPerEarthYear = 0.2408467;
        public const double VenusYearsPerEarthYear = 0.61519726;
        public const double MarsYearsPerEarthYear = 1.8808158;
        public const double JupiterYearsPerEarthYear = 11.862615;
        public const double SaturnYearsPerEarthYear = 29.447498;
        public const double UranusYearsPerEarthYear = 84.016846;
        public const double NeptuneYearsPerEarthYear = 164.79132;

        #endregion Constants

        public double Seconds { get; private set; }

        public SpaceAge(double seconds)
        {
            Seconds = seconds;
        }

        public double OnEarth()
        {
            return GetYears();
        }

        public double OnMercury()
        {
            return GetYears(MercuryYearsPerEarthYear);
        }

        public double OnVenus()
        {
            return GetYears(VenusYearsPerEarthYear);
        }

        public double OnMars()
        {
            return GetYears(MarsYearsPerEarthYear);
        }

        public double OnJupiter()
        {
            return GetYears(JupiterYearsPerEarthYear);
        }

        public double OnSaturn()
        {
            return GetYears(SaturnYearsPerEarthYear);
        }

        public double OnUranus()
        {
            return GetYears(UranusYearsPerEarthYear);
        }

        public double OnNeptune()
        {
            return GetYears(NeptuneYearsPerEarthYear);
        }

        private double GetYears(double divisor = 1.0)
        {
            return Math.Round(Seconds / EarthSolarYear / divisor, 2);
        }
    }
}