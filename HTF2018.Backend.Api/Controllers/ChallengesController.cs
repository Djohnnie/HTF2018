using HTF2018.Backend.Api.Attributes;
using HTF2018.Backend.Common.Model;
using HTF2018.Backend.Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using HTF2018.Backend.Api.Filters;
using Swashbuckle.AspNetCore.Annotations;

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
        /// <remarks>This will be your starting point. Request the first challenge and get started with unlocking the artifact!</remarks>
        /// <returns></returns>
        [HttpGet]
        [SwaggerResponse(200, Type = typeof(Challenge))]
        public async Task<IActionResult> GetFirstChallenge()
        {
            Challenge challenge = await _challengeEngine.GetChallenge(Identifier.Challenge01);
            return Ok(challenge);
        }

        /// <summary>
        /// Posts the solution for the first challenge.
        /// </summary>
        /// <param name="answer">The answer.</param>
        /// <remarks>After you have requested the first challenge, use this endpoint to POST your answer. Use the example data to format your answer correctly!</remarks>
        /// <returns></returns>
        [HttpPost]
        [SwaggerResponse(200, Type = typeof(Response))]
        public async Task<IActionResult> PostFirstChallengeSolution(Answer answer)
        {
            Response response = await _challengeEngine.ValidateChallenge(answer);
            return Ok(response);
        }

        /// <summary>
        /// Gets any of the subsequent challenges identified by a challenge code.
        /// </summary>
        /// <param name="challengeCode">The challenge code to identify a challenge.</param>
        /// <remarks>If you have successfully answered the first challenge, you will receive the information needed to request a subsequent challenge with that response.</remarks>
        /// <returns></returns>
        [HttpGet, Route("{challengeCode}")]
        [HtfIdentification, ServiceFilter(typeof(IdentificationFilter))]
        [SwaggerResponse(200, Type = typeof(Challenge))]
        public async Task<IActionResult> GetSubsequentChallenge(String challengeCode)
        {
            Challenge challenge = await _challengeEngine.GetChallenge(challengeCode);
            return Ok(challenge);
        }

        /// <summary>
        /// Posts the solution for any of the subsequent challenges identified by a challenge code.
        /// </summary>
        /// <param name="challengeCode">The challenge code to identify a challenge.</param>
        /// <param name="answer">The answer.</param>
        /// <remarks>After you have requested a challenge, use this endpoint to POST your answer. Use the example data to format your answer correctly!</remarks>
        /// <returns></returns>
        [HttpPost, Route("{challengeCode}")]
        [HtfIdentification, ServiceFilter(typeof(IdentificationFilter))]
        [SwaggerResponse(200, Type = typeof(Response))]
        public async Task<IActionResult> PostSubsequentChallengeSolution(String challengeCode, Answer answer)
        {
            Response response = await _challengeEngine.ValidateChallenge(answer);
            return Ok(response);
        }
    }
}