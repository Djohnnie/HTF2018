using HTF2018.Backend.Api.Attributes;
using HTF2018.Backend.Common.Model;
using HTF2018.Backend.Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HTF2018.Backend.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ChallengesController : ControllerBase
    {
        private readonly IChallengeEngine _challengeEngine;

        public ChallengesController(IChallengeEngine challengeEngine)
        {
            _challengeEngine = challengeEngine;
        }

        /// <summary>
        /// Gets the first challenge.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetFirstChallenge()
        {
            Challenge challenge = _challengeEngine.GetChallenge(Identifier.Challenge01);
            return Ok(challenge);
        }

        [HttpPost]
        public IActionResult PostFirstChallengeSolution(Answer answer)
        {
            Response response = _challengeEngine.ValidateChallenge(answer);
            return Ok(response);
        }

        [HttpGet, Route("{challengeCode}")]
        [HtfIdentification]
        public IActionResult GetSubsequentChallenge(String challengeCode)
        {
            Challenge challenge = _challengeEngine.GetChallenge(challengeCode);
            return Ok(challenge);
        }

        [HttpPost, Route("{challengeCode}")]
        [HtfIdentification]
        public IActionResult PostSubsequentChallengeSolution(String challengeCode, Answer answer)
        {
            Response response = _challengeEngine.ValidateChallenge(answer);
            return Ok(response);
        }
    }
}