using HTF2018.Backend.Common.Model;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace HTF2018.Backend.Example.Challenges
{
    public class Challenge01 : IChallenge
    {
        private Status _status;
        private Challenge _challenge;

        public Status Status => _status;

        public async Task Fetch(String endpoint)
        {
            RestClient client = new RestClient(endpoint);
            RestRequest request = new RestRequest(Method.GET);
            var response = await client.ExecuteTaskAsync<Challenge>(request);
            if (response.IsSuccessful)
            {
                _challenge = response.Data;
            }
            else
            {
                _challenge = null;
            }
        }

        public Task Solve()
        {
            var answer = BuildAnswer();
            return Task.CompletedTask;
        }

        private Answer BuildAnswer()
        {
            throw new NotImplementedException();
        }
    }
}