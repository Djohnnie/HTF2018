using HTF2018.Backend.Common;
using HTF2018.Backend.Common.Exceptions;
using HTF2018.Backend.Common.Extensions;
using HTF2018.Backend.Common.Model;
using HTF2018.Backend.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HTF2018.Backend.Logic
{
    public class ChallengeEngine : IChallengeEngine
    {
        private readonly IHtfContext _context;
        private readonly ITeamLogic _teamLogic;
        private readonly IChallengeLogic _challengeLogic;

        private readonly Dictionary<Identifier, IChallenge> _challengeLibrary;

        public ChallengeEngine(IHtfContext context, ITeamLogic teamLogic, IChallengeLogic challengeLogic,
            IChallenge01 challenge01, IChallenge02 challenge02, IChallenge03 challenge03, IChallenge04 challenge04, IChallenge05 challenge05,
            IChallenge06 challenge06, IChallenge07 challenge07, IChallenge08 challenge08, IChallenge09 challenge09, IChallenge10 challenge10,
            IChallenge11 challenge11, IChallenge12 challenge12, IChallenge13 challenge13, IChallenge14 challenge14, IChallenge15 challenge15,
            IChallenge16 challenge16, IChallenge17 challenge17, IChallenge18 challenge18, IChallenge19 challenge19, IChallenge20 challenge20)
        {
            _context = context;
            _teamLogic = teamLogic;
            _challengeLogic = challengeLogic;
            _challengeLibrary = new Dictionary<Identifier, IChallenge>
            {
                { Identifier.Challenge01, challenge01 }, { Identifier.Challenge02, challenge02 },
                { Identifier.Challenge03, challenge03 }, { Identifier.Challenge04, challenge04 },
                { Identifier.Challenge05, challenge05 }, { Identifier.Challenge06, challenge06 },
                { Identifier.Challenge07, challenge07 }, { Identifier.Challenge08, challenge08 },
                { Identifier.Challenge09, challenge09 }, { Identifier.Challenge10, challenge10 },
                { Identifier.Challenge11, challenge11 }, { Identifier.Challenge12, challenge12 },
                { Identifier.Challenge13, challenge13 }, { Identifier.Challenge14, challenge14 },
                { Identifier.Challenge15, challenge15 }, { Identifier.Challenge16, challenge16 },
                { Identifier.Challenge17, challenge17 }, { Identifier.Challenge18, challenge18 },
                { Identifier.Challenge19, challenge19 }, { Identifier.Challenge20, challenge20 }
            };
        }

        public Task<Challenge> GetChallenge(Identifier challengeIdentifier)
        {
            IChallenge challenge = _challengeLibrary[challengeIdentifier];
            return challenge.GetChallenge();
        }

        public Task<Challenge> GetChallenge(String challengeCode)
        {
            Identifier identifier = GetChallengeIdentifierForChallengeCode(challengeCode);
            IChallenge challenge = _challengeLibrary[identifier];
            return challenge.GetChallenge();
        }

        public async Task<Response> ValidateChallenge(Answer answer)
        {
            var storedChallenge = await _challengeLogic.GetChallengeById(answer.ChallengeId);
            if (storedChallenge == null)
            {
                throw new AnswerToUnknownChallengeException();
            }
            IChallenge challenge = _challengeLibrary[storedChallenge.Identifier];
            return await challenge.ValidateChallenge(answer, _context);
        }

        private Identifier GetChallengeIdentifierForChallengeCode(String challengeCode)
        {
            Dictionary<String, Identifier> identifiers = new Dictionary<String, Identifier>
            {
                { $"{Identifier.Challenge01}".Md5Hash(), Identifier.Challenge01 },
                { $"{Identifier.Challenge02}".Md5Hash(), Identifier.Challenge02 },
                { $"{Identifier.Challenge03}".Md5Hash(), Identifier.Challenge03 },
                { $"{Identifier.Challenge04}".Md5Hash(), Identifier.Challenge04 },
                { $"{Identifier.Challenge05}".Md5Hash(), Identifier.Challenge05 },
                { $"{Identifier.Challenge06}".Md5Hash(), Identifier.Challenge06 },
                { $"{Identifier.Challenge07}".Md5Hash(), Identifier.Challenge07 },
                { $"{Identifier.Challenge08}".Md5Hash(), Identifier.Challenge08 },
                { $"{Identifier.Challenge09}".Md5Hash(), Identifier.Challenge09 },
                { $"{Identifier.Challenge10}".Md5Hash(), Identifier.Challenge10 },
                { $"{Identifier.Challenge11}".Md5Hash(), Identifier.Challenge11 },
                { $"{Identifier.Challenge12}".Md5Hash(), Identifier.Challenge12 },
                { $"{Identifier.Challenge13}".Md5Hash(), Identifier.Challenge13 },
                { $"{Identifier.Challenge14}".Md5Hash(), Identifier.Challenge14 },
                { $"{Identifier.Challenge15}".Md5Hash(), Identifier.Challenge15 },
                { $"{Identifier.Challenge16}".Md5Hash(), Identifier.Challenge16 },
                { $"{Identifier.Challenge17}".Md5Hash(), Identifier.Challenge17 },
                { $"{Identifier.Challenge18}".Md5Hash(), Identifier.Challenge18 },
                { $"{Identifier.Challenge19}".Md5Hash(), Identifier.Challenge19 },
                { $"{Identifier.Challenge20}".Md5Hash(), Identifier.Challenge20 }
            };
            if (identifiers.ContainsKey(challengeCode))
            {
                return identifiers[challengeCode];
            }

            throw new InvalidChallengeCodeException();
        }
    }
}