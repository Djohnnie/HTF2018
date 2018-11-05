using HTF2018.Backend.ChallengeEngine;
using HTF2018.Backend.ChallengeEngine.Model;
using HTF2018.Backend.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace HTF2018.Backend.Logic
{
    public class ChallengeLogic : IChallengeLogic
    {
        private readonly IChallengeEngine _challengeEngine;

        public ChallengeLogic(IChallengeEngine challengeEngine)
        {
            _challengeEngine = challengeEngine;
        }

        public Challenge GetFirstChallenge()
        {
            return _challengeEngine.GetChallenge(Identifier.Challenge01);
        }

        public Challenge GetSubsequentChallenge(String challengeCode)
        {
            Identifier identifier = GetChallengeIdentifierForChallengeCode(challengeCode);
            return _challengeEngine.GetChallenge(identifier);
        }

        public Response ValidateChallenge(Answer answer)
        {
            return _challengeEngine.ValidateChallenge(answer);
        }

        private Identifier GetChallengeIdentifierForChallengeCode(String challengeCode)
        {
            Dictionary<String, Identifier> identifiers = new Dictionary<String, Identifier>
            {
                { CalculateChallengeCodeForChallengeIdentifier(Identifier.Challenge01), Identifier.Challenge01 },
                { CalculateChallengeCodeForChallengeIdentifier(Identifier.Challenge02), Identifier.Challenge02 },
                { CalculateChallengeCodeForChallengeIdentifier(Identifier.Challenge03), Identifier.Challenge03 },
                { CalculateChallengeCodeForChallengeIdentifier(Identifier.Challenge04), Identifier.Challenge04 },
                { CalculateChallengeCodeForChallengeIdentifier(Identifier.Challenge05), Identifier.Challenge05 },
                { CalculateChallengeCodeForChallengeIdentifier(Identifier.Challenge06), Identifier.Challenge06 },
                { CalculateChallengeCodeForChallengeIdentifier(Identifier.Challenge07), Identifier.Challenge07 },
                { CalculateChallengeCodeForChallengeIdentifier(Identifier.Challenge08), Identifier.Challenge08 },
                { CalculateChallengeCodeForChallengeIdentifier(Identifier.Challenge09), Identifier.Challenge09 },
                { CalculateChallengeCodeForChallengeIdentifier(Identifier.Challenge10), Identifier.Challenge10 },
            };
            return identifiers[challengeCode];
        }

        private String CalculateChallengeCodeForChallengeIdentifier(Identifier identifier)
        {
            MD5 md5 = MD5.Create();
            Byte[] bytesToHash = Encoding.UTF8.GetBytes(Identifier.Challenge01.ToString());
            Byte[] hashedBytes = md5.ComputeHash(bytesToHash);
            return BitConverter.ToString(hashedBytes).Replace("-", "");
        }
    }
}