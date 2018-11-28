using HTF2018.Backend.Common;
using HTF2018.Backend.Common.Exceptions;
using HTF2018.Backend.Common.Model;
using HTF2018.Backend.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using RestSharp;
using RestSharp.Serializers;

namespace HTF2018.Backend.Logic.Challenges
{
    /// <summary>
    /// CHALLENGE 08:
    ///   Geocoding
    /// </summary>
    public class Challenge13 : ChallengeBase, IChallenge13
    {
        private readonly IMemoryCache _cache;

        public Challenge13(IHtfContext htfContext, ITeamLogic teamLogic, IChallengeLogic challengeLogic,
            IDashboardLogic dashboardLogic, IHistoryLogic historyLogic, IMemoryCache memoryCache)
            : base(htfContext, teamLogic, challengeLogic, dashboardLogic, historyLogic)
        {
            _cache = memoryCache;
            _client = new RestClient("https://api.mapbox.com/geocoding/v5/mapbox.places/");
        }

        private readonly string _mapboxToken = Environment.GetEnvironmentVariable("MAPBOXKEY");
        private readonly RestClient _client;
        private readonly Random _randomGenerator = new Random();

        private readonly List<Coordinate> _artifactLocations = new List<Coordinate>
        {
            new Coordinate {Latitude = "51.168716", Longitude = "4.432200"},
            new Coordinate{Latitude = "51.142513", Longitude = "4.442339"},
            new Coordinate{Latitude = "51.203465", Longitude = "4.494959"},
            new Coordinate{Latitude = "51.059353",Longitude = "4.514054"},
            new Coordinate{Latitude = "51.186176",Longitude = "4.480791"},
            new Coordinate{Latitude = "51.198418",Longitude = "4.490397"},
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
                    }
                }
            };

            return Task.FromResult(question);
        }

        protected override Task<Answer> BuildAnswer(Question question, Guid challengeId)
        {
            var latitude = question.InputValues.Single(e => e.Name.Equals("latitude")).Data;
            var longitude = question.InputValues.Single(e => e.Name.Equals("longitude")).Data;
            if (!_cache.TryGetValue("${latitude}_{longitude}", out CoordinateResponse coordinateResponse))
            {
                var request = new RestRequest("{longitude},{latitude}.json?types=poi&access_token={token}");
                request.AddUrlSegment("longitude", longitude);
                request.AddUrlSegment("latitude", latitude);
                request.AddUrlSegment("token", _mapboxToken);

                var response = _client.Execute<CoordinateResponse>(request);
                coordinateResponse = response.Data;
            }

            var answer = new Answer
            {
                ChallengeId = challengeId,
                Values = new List<Value>()
            };
            foreach (var feature in coordinateResponse.Features)
            {
                answer.Values.Add(new Value
                {
                    Name = "target",
                    Data = feature.text
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

            foreach (var answerValue in answer.Values.Where(e => e.Name.Equals("target")))
            {
                if (string.IsNullOrEmpty(answerValue.Data)) throw new InvalidAnswerException();
            }
        }
    }

    public class Coordinate
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }

    public class Feature
    {public string text { get; set; }
        [SerializeAs(Name = "place_name")] public string PlaceName { get; set; }
    }

    public class CoordinateResponse
    {
        [SerializeAs(Name = "features")] public List<Feature> Features { get; set; }
    }
}