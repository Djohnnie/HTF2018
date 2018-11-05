using HTF2018.Backend.Common;
using HTF2018.Backend.Common.Extensions;
using HTF2018.Backend.Common.Model;
using HTF2018.Backend.Logic.Challenges;
using HTF2018.Backend.Logic.Interfaces;
using System;
using System.Collections.Generic;

namespace HTF2018.Backend.Logic
{
    public class ChallengeEngine : IChallengeEngine
    {
        private readonly IHtfContext _context;

        private readonly Dictionary<Identifier, IChallenge> _challengeLibrary;

        public ChallengeEngine(IHtfContext context,
            Challenge01 challenge01)
        {
            _context = context;
            _challengeLibrary = new Dictionary<Identifier, IChallenge>
            {
                { Identifier.Challenge01, challenge01 },
                { Identifier.Challenge02, new Challenge02() },
                { Identifier.Challenge03, new Challenge03() },
                { Identifier.Challenge04, new Challenge04() },
                { Identifier.Challenge05, new Challenge05() },
                { Identifier.Challenge06, new Challenge06() },
                { Identifier.Challenge07, new Challenge07() },
                { Identifier.Challenge08, new Challenge08() },
                { Identifier.Challenge09, new Challenge09() },
                { Identifier.Challenge10, new Challenge10() },
                { Identifier.Challenge11, new Challenge11() },
                { Identifier.Challenge12, new Challenge12() },
                { Identifier.Challenge13, new Challenge13() },
                { Identifier.Challenge14, new Challenge14() },
                { Identifier.Challenge15, new Challenge15() },
                { Identifier.Challenge16, new Challenge16() },
                { Identifier.Challenge17, new Challenge17() },
                { Identifier.Challenge18, new Challenge18() },
                { Identifier.Challenge19, new Challenge19() },
                { Identifier.Challenge20, new Challenge20() },
            };
        }

        public Challenge GetChallenge(Identifier challengeIdentifier)
        {
            IChallenge challenge = _challengeLibrary[challengeIdentifier];
            return challenge.GetChallenge();
        }

        public Challenge GetChallenge(String challengeCode)
        {
            Identifier identifier = GetChallengeIdentifierForChallengeCode(challengeCode);
            IChallenge challenge = _challengeLibrary[identifier];
            return challenge.GetChallenge();
        }

        public Response ValidateChallenge(Answer answer)
        {
            IChallenge challenge = _challengeLibrary[Identifier.Challenge01];
            return challenge.ValidateChallenge(answer, _context);
        }

        private Identifier GetChallengeIdentifierForChallengeCode(String challengeCode)
        {
            Dictionary<String, Identifier> identifiers = new Dictionary<String, Identifier>
            {
                { $"{Identifier.Challenge01}".Base64Encode(), Identifier.Challenge01 },
                { $"{Identifier.Challenge02}".Base64Encode(), Identifier.Challenge02 },
                { $"{Identifier.Challenge03}".Base64Encode(), Identifier.Challenge03 },
                { $"{Identifier.Challenge04}".Base64Encode(), Identifier.Challenge04 },
                { $"{Identifier.Challenge05}".Base64Encode(), Identifier.Challenge05 },
                { $"{Identifier.Challenge06}".Base64Encode(), Identifier.Challenge06 },
                { $"{Identifier.Challenge07}".Base64Encode(), Identifier.Challenge07 },
                { $"{Identifier.Challenge08}".Base64Encode(), Identifier.Challenge08 },
                { $"{Identifier.Challenge09}".Base64Encode(), Identifier.Challenge09 },
                { $"{Identifier.Challenge10}".Base64Encode(), Identifier.Challenge10 },
                { $"{Identifier.Challenge10}".Base64Encode(), Identifier.Challenge11 },
                { $"{Identifier.Challenge10}".Base64Encode(), Identifier.Challenge12 },
                { $"{Identifier.Challenge10}".Base64Encode(), Identifier.Challenge13 },
                { $"{Identifier.Challenge10}".Base64Encode(), Identifier.Challenge14 },
                { $"{Identifier.Challenge10}".Base64Encode(), Identifier.Challenge15 },
                { $"{Identifier.Challenge10}".Base64Encode(), Identifier.Challenge16 },
                { $"{Identifier.Challenge10}".Base64Encode(), Identifier.Challenge17 },
                { $"{Identifier.Challenge10}".Base64Encode(), Identifier.Challenge18 },
                { $"{Identifier.Challenge10}".Base64Encode(), Identifier.Challenge19 },
                { $"{Identifier.Challenge10}".Base64Encode(), Identifier.Challenge20 }
            };
            return identifiers[challengeCode];
        }
    }
}