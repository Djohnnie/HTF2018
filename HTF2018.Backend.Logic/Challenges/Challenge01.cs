using HTF2018.Backend.Common;
using HTF2018.Backend.Common.Exceptions;
using HTF2018.Backend.Common.Model;
using HTF2018.Backend.Common.Extensions;
using HTF2018.Backend.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HTF2018.Backend.Logic.Challenges
{
    /// <summary>
    /// CHALLENGE 01:
    ///   Identify yourself.
    /// </summary>
    public class Challenge01 : ChallengeBase, IChallenge01
    {
        private readonly IChallengeLogic _challengeLogic;
        private readonly ITeamLogic _teamLogic;

        public Challenge01(IHtfContext htfContext, IChallengeLogic challengeLogic, ITeamLogic teamLogic) : base(htfContext)
        {
            _challengeLogic = challengeLogic;
            _teamLogic = teamLogic;
        }

        public async Task<Challenge> GetChallenge()
        {
            Challenge challenge = BuildChallenge(Identifier.Challenge01);
            await _challengeLogic.CreateChallenge(challenge.Id, Identifier.Challenge01);
            return challenge;
        }

        public async Task<Response> ValidateChallenge(Answer answer)
        {
            ValidateAnswer(answer);

            String teamName = answer.Values.Single(x => x.Name == "name").Data;
            String teamSecret = answer.Values.Single(x => x.Name == "secret").Data;
            var team = await _teamLogic.FindTeamByName(teamName);
            if (team == null)
            {
                team = await _teamLogic.CreateTeam(teamName, teamSecret);
                await _challengeLogic.SolveChallenge(answer.ChallengeId, team.Id);
            }
            else
            {
                if (team.HashedSecret != teamSecret.Md5Hash())
                {
                    throw new InvalidTeamException();
                }
                else
                {
                    await _challengeLogic.SolveChallenge(answer.ChallengeId, team.Id);
                }
            }

            return new Response
            {
                Identifier = Identifier.Challenge01,
                Status = Status.Successful,
                Identification = team.Identification,
                Overview = BuildOverview()
            };
        }

        protected override Question BuildQuestion()
        {
            return new Question
            {
                InputValues = new List<Value>
                {
                    new Value{ Name = "name", Data = "Use this parameter to provide a name for your team." },
                    new Value{ Name = "secret", Data = "Use this parameter to provide a secret for your team to authenticate with." }
                }
            };
        }

        protected override Example BuildExample(Guid challengeId)
        {
            return new Example
            {
                Question = BuildQuestion(),
                Answer = new Answer
                {
                    ChallengeId = challengeId,
                    Values = new List<Value>
                    {
                        new Value{ Name = "name", Data = "we_will_hack_the_future" },
                        new Value{ Name = "secret", Data = "twinkle, twinkle, little star!" }
                    }
                }
            };
        }

        protected override void ValidateAnswer(Answer answer)
        {
            Boolean invalid = false;
            if (answer.Values == null) { invalid = true; }
            if (answer.Values.Count != 2) { invalid = true; }
            if (!answer.Values.Any(x => x.Name == "name")) { invalid = true; }
            if (!answer.Values.Any(x => x.Name == "secret")) { invalid = true; }
            if (String.IsNullOrEmpty(answer.Values.Single(x => x.Name == "name").Data)) { invalid = true; }
            if (String.IsNullOrEmpty(answer.Values.Single(x => x.Name == "secret").Data)) { invalid = true; }

            if (invalid)
            {
                throw new InvalidAnswerException();
            }
        }
    }
}