using HTF2018.Backend.ChallengeEngine.Model;
using HTF2018.Backend.Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HTF2018.Backend.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ChallengesController : ControllerBase
    {
        private readonly IChallengeLogic _challengeLogic;

        public ChallengesController(IChallengeLogic challengeLogic)
        {
            _challengeLogic = challengeLogic;
        }

        /// <summary>
        /// Gets the first challenge.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetFirstChallenge()
        {
            Challenge challenge = _challengeLogic.GetFirstChallenge();
            return Ok(challenge);
        }

        [HttpGet, Route("{challengeCode}")]
        public IActionResult GetSubsequentChallenge(String challengeCode)
        {
            Challenge challenge = _challengeLogic.GetSubsequentChallenge(challengeCode);
            return Ok(challenge);
        }

        [HttpPost]
        public IActionResult PostChallengeSolution(Answer answer)
        {
            Response response = _challengeLogic.ValidateChallenge(answer);
            return Ok(response);
        }
    }
}