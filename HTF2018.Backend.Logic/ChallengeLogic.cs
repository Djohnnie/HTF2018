using HTF2018.Backend.Common.Model;
using HTF2018.Backend.DataAccess;
using HTF2018.Backend.Logic.Interfaces;
using System;
using System.Threading.Tasks;
using Challenge = HTF2018.Backend.DataAccess.Entities.Challenge;

namespace HTF2018.Backend.Logic
{
    public class ChallengeLogic : IChallengeLogic
    {
        private readonly TheArtifactDbContext _dbContext;

        public ChallengeLogic(TheArtifactDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Challenge> CreateChallenge(Guid challengeId, Identifier identifier)
        {
            Challenge challenge = new Challenge
            {
                Id = challengeId,
                Identifier = identifier,
                Team = null,
                Question = null,
                Answer = null
            };
            await _dbContext.Challenges.AddAsync(challenge);
            await _dbContext.SaveChangesAsync();
            return challenge;
        }



        //private readonly ITeamLogic _teamLogic;
        //private readonly IChallengeEngine _challengeEngine;

        //public ChallengeLogic(ITeamLogic teamLogic, IChallengeEngine challengeEngine)
        //{
        //    _teamLogic = teamLogic;
        //    _challengeEngine = challengeEngine;
        //}

        //public Challenge GetFirstChallenge()
        //{
        //    return _challengeEngine.GetChallenge(Identifier.Challenge01);
        //}

        //public async Task<Response> ValidateFirstChallenge(Answer answer)
        //{
        //    if (answer.ChallengeId == Guid.Empty)
        //    {
        //        throw new ValidationException("The answer you have provided does not link to a valid challenge!");
        //    }

        //    if (answer.Values == null || answer.Values.Count == 0)
        //    {
        //        throw new ValidationException("You have provided no answer data!");
        //    }

        //    Value nameValue = answer.Values.SingleOrDefault(x => x.Name == "name");
        //    if (nameValue == null)
        //    {
        //        throw new ValidationException("Your answer does not contain the required 'name' field!");
        //    }

        //    Value secretValue = answer.Values.SingleOrDefault(x => x.Name == "secret");
        //    if (secretValue == null)
        //    {
        //        throw new ValidationException("Your answer does not contain the required 'secret' field!");
        //    }

        //    Team team = await _teamLogic.FindTeamByName(nameValue.Data);
        //    if (team == null)
        //    {
        //        team = await _teamLogic.CreateTeam(nameValue.Data, secretValue.Data);
        //    }

        //    return _challengeEngine.ValidateChallenge(answer);
        //}

        //public Challenge GetSubsequentChallenge(String challengeCode)
        //{
        //    Identifier identifier = GetChallengeIdentifierForChallengeCode(challengeCode);
        //    return _challengeEngine.GetChallenge(identifier);
        //}

        //public Response ValidateSubsequentChallenge(Answer answer)
        //{
        //    return _challengeEngine.ValidateChallenge(answer);
        //}

        //private String CalculateChallengeCodeForChallengeIdentifier(Identifier identifier)
        //{
        //    MD5 md5 = MD5.Create();
        //    Byte[] bytesToHash = Encoding.UTF8.GetBytes(identifier.ToString());
        //    Byte[] hashedBytes = md5.ComputeHash(bytesToHash);
        //    return BitConverter.ToString(hashedBytes).Replace("-", "");
        //}

    }
}