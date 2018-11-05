using HTF2018.Backend.Common.Extensions;
using HTF2018.Backend.DataAccess;
using HTF2018.Backend.DataAccess.Entities;
using HTF2018.Backend.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace HTF2018.Backend.Logic
{
    public class TeamLogic : ITeamLogic
    {
        private readonly TheArtifactDbContext _dbContext;

        public TeamLogic(TheArtifactDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Team> FindTeamByName(string name)
        {
            return _dbContext.Teams.SingleOrDefaultAsync(x => x.Name == name);
        }

        public Task<Team> FindTeamByIdentification(string identification)
        {
            return _dbContext.Teams.SingleOrDefaultAsync(x => x.Identification == identification);
        }

        public async Task<Team> CreateTeam(string name, string secret)
        {
            Guid id = Guid.NewGuid();
            Team team = new Team
            {
                Id = id,
                Name = name,
                HashedSecret = secret.Md5Hash(),
                Identification = $"{id}".Base64Encode()
            };
            await _dbContext.Teams.AddAsync(team);
            await _dbContext.SaveChangesAsync();
            return team;
        }
    }
}