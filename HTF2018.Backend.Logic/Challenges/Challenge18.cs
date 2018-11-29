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
    /// CHALLENGE 13:
    ///   Pokemon
    /// </summary>
    public class Challenge18 : ChallengeBase, IChallenge18
    {
        private readonly IMemoryCache _cache;

        public Challenge18(IHtfContext htfContext, ITeamLogic teamLogic, IChallengeLogic challengeLogic,
            IDashboardLogic dashboardLogic, IHistoryLogic historyLogic, IMemoryCache memoryCache)
            : base(htfContext, teamLogic, challengeLogic, dashboardLogic, historyLogic)
        {
            _cache = memoryCache;
            _client = new RestClient("https://pokeapi.co/api/v2/pokemon/");
        }

        private readonly RestClient _client;
        private readonly Random _randomGenerator = new Random();
        

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
            if (!_cache.TryGetValue($"pokemon_{pokemonNr}", out Pokemon pokemon))
            {

                var request = new RestRequest("{pokemonNr}/");
                request.AddUrlSegment("pokemonNr", pokemonNr);
                _client.UserAgent = "HTF2018 - Lookup";

                var response = _client.Execute<Pokemon>(request);
                pokemon = response.Data;
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromDays(1));
                
                _cache.Set($"pokemon_{pokemonNr}", pokemon, cacheEntryOptions);
            }
            

            return Task.FromResult(new Answer
            {
                ChallengeId = challengeId,
                Values = new List<Value>
                {
                    new Value
                    {
                        Name = "name",
                        Data = pokemon.Name
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

            if (!answer.Values.Any(x => x.Name == "name"))
            {
                throw new InvalidAnswerException();
            }
            if (answer.Values.Count(x => x.Name == "name")!=1)
            {
                throw new InvalidAnswerException();
            }

            if (string.IsNullOrEmpty(answer.Values.Single(x => x.Name == "name").Data))
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