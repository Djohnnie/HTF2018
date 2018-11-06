using HTF2018.Backend.Common.Extensions;
using HTF2018.Backend.DataAccess;
using HTF2018.Backend.DataAccess.Entities;
using HTF2018.Backend.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamModel = HTF2018.Backend.Common.Model.Team;

namespace HTF2018.Backend.Logic
{
    public class TeamLogic : ITeamLogic
    {
        private readonly TheArtifactDbContext _dbContext;

        public TeamLogic(TheArtifactDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<TeamModel>> GetAllTeams()
        {
            return _dbContext.Teams.OrderBy(x => x.Name)
                .Select(x => new TeamModel { Id = x.Id, Name = x.Name }).ToListAsync();
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