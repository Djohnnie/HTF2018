using HTF2018.Backend.Common;
using HTF2018.Backend.Common.Exceptions;
using HTF2018.Backend.Common.Model;
using HTF2018.Backend.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Serializers;

namespace HTF2018.Backend.Logic.Challenges
{
    /// <summary>
    /// CHALLENGE 13:
    ///   Pokemon
    /// </summary>
    public class Challenge18 : ChallengeBase, IChallenge18
    {
        public Challenge18(IHtfContext htfContext, ITeamLogic teamLogic, IChallengeLogic challengeLogic,
            IDashboardLogic dashboardLogic, IHistoryLogic historyLogic)
            : base(htfContext, teamLogic, challengeLogic, dashboardLogic, historyLogic)
        {
            _client = new RestClient("https://pokeapi.co/api/v2/ ");
        }

        private readonly RestClient _client;
        private readonly Random _randomGenerator = new Random();

        private readonly List<Coordinate> _artifactLocations = new List<Coordinate>
        {
            //todo: ADD COORDINATES
            new Coordinate {Latitude = "51.168716", Longitude = "4.432200",}
        };

        public async Task<Challenge> GetChallenge()
        {
            var challenge = await BuildChallenge(Identifier.Challenge18);
            return challenge;
        }

        protected override Task<Question> BuildQuestion()
        {
            var question = new Question
            {
                InputValues = new List<Value>
                {
                    new Value
                    {
                        Name = "#",
                        Data = $"{_randomGenerator.Next(1, 802)}"
                    }
                }
            };

            return Task.FromResult(question);
        }

        protected override Task<Answer> BuildAnswer(Question question, Guid challengeId)
        {
            var pokemonNr = question.InputValues.Single(e => e.Name.Equals("#")).Data;
            var request = new RestRequest("pokemon/{pokemonNr}");
            request.AddUrlSegment("pokemonNr", pokemonNr);

            var response = _client.Execute<Pokemon>(request);


            return Task.FromResult(new Answer
            {
                ChallengeId = challengeId,
                Values = new List<Value>
                {
                    new Value
                    {
                        Name = "name",
                        Data =
                            response.Data.Name
                    }
                }
            });
        }

        protected override async Task<Example> BuildExample(Guid challengeId)
        {
            var question = new Question
            {
                InputValues = new List<Value>
                {
                    new Value
                    {
                        Name = "#",
                        Data = $"1"
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

            if (!answer.Values.Any(x => x.Name == "#"))
            {
                throw new InvalidAnswerException();
            }

            if (string.IsNullOrEmpty(answer.Values.Single(x => x.Name == "#").Data))
            {
                throw new InvalidAnswerException();
            }
        }
    }

    public class Pokemon
    {
        [SerializeAs(Name = "name")] public string Name { get; set; }
    }
}