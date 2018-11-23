using HTF2018.Backend.Common;
using HTF2018.Backend.Common.Exceptions;
using HTF2018.Backend.Common.Model;
using HTF2018.Backend.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;

namespace HTF2018.Backend.Logic.Challenges
{
    /// <summary>
    /// CHALLENGE 08:
    ///   Geocoding
    /// </summary>
    public class Challenge13 : ChallengeBase, IChallenge13
    {
        public Challenge13(IHtfContext htfContext, ITeamLogic teamLogic, IChallengeLogic challengeLogic,
            IDashboardLogic dashboardLogic, IHistoryLogic historyLogic)
            : base(htfContext, teamLogic, challengeLogic, dashboardLogic, historyLogic)
        {
            _client = new RestClient("https://api.mapbox.com/geocoding/v5/mapbox.places/");
        }

        private readonly string _mapboxToken = Environment.GetEnvironmentVariable("mapboxkey");
        private readonly RestClient _client;
        private readonly Random _randomGenerator = new Random();

        private readonly List<Coordinate> _artifactLocations = new List<Coordinate>
        {
            //todo: ADD COORDINATES
            new Coordinate {Latitude = "51.168716", Longitude = "4.432200",}
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
                        Name = "latitude",
                        Data = $"{_artifactLocations[random].Latitude}"
                    },
                    new Value
                    {
                        Name = "longitude",
                        Data = $"{_artifactLocations[random].Longitude}"
                    },

                    //add multiple values to make it more difficult
                }
            };

            return Task.FromResult(question);
        }

        protected override Task<Answer> BuildAnswer(Question question, Guid challengeId)
        {
            var latitude = question.InputValues.Single(e => e.Name.Equals("latitude")).Data;
            var longitude = question.InputValues.Single(e => e.Name.Equals("longitude")).Data;
            var request = new RestRequest("{longitude},{latitude}.json?types=poi&access_token={token}");
            request.AddUrlSegment("longitude", longitude);
            request.AddUrlSegment("latitude", latitude);
            request.AddUrlSegment("token", _mapboxToken);

            var response = _client.Execute<CoordinateResponse>(request);

            var answer = new Answer
            {
                ChallengeId = challengeId,
                Values = new List<Value>
                {
                    
                }
            };
            foreach (var dataFeature in response.Data.features)
            {
                answer.Values.Add(new Value
                {
                    Name = "target",
                    Data =
                        dataFeature.text
                });
            }

            return Task.FromResult(answer);
        }

        protected override async Task<Example> BuildExample(Guid challengeId)
        {
            var question = new Question
            {
               InputValues = new List<Value>
               {
                   new Value
                   {
                       Name = "latitude",
                       Data = $"{_artifactLocations[0].Latitude}"
                   },
                   new Value
                   {
                       Name = "longitude",
                       Data = $"{_artifactLocations[0].Longitude}"
                   },
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
            if (!answer.Values.Any(x => x.Name == "target"))
            {
                throw new InvalidAnswerException();
            }

            foreach (var answerValue in answer.Values.Where(e=>e.Name.Equals("target")))
            {
                if(string.IsNullOrEmpty(answerValue.Data)) throw new InvalidAnswerException();
            }
        }
    }

    public class Coordinate
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }

    public class Properties
    {
        public string tel { get; set; }
        public string address { get; set; }
        public string category { get; set; }
        public bool landmark { get; set; }
    }

    public class Geometry
    {
        public string type { get; set; }
        public List<double> coordinates { get; set; }
    }

    public class Context
    {
        public string id { get; set; }
        public string text { get; set; }
        public string wikidata { get; set; }
        public string short_code { get; set; }
    }

    public class Feature
    {
        public string id { get; set; }
        public string type { get; set; }
        public List<string> place_type { get; set; }
        public int relevance { get; set; }
        public Properties properties { get; set; }
        public string text { get; set; }
        public string place_name { get; set; }
        public List<double> center { get; set; }
        public Geometry geometry { get; set; }
        public List<Context> context { get; set; }
    }

    public class CoordinateResponse
    {
        public string type { get; set; }
        public List<double> query { get; set; }
        public List<Feature> features { get; set; }
    }
}