using System;
using System.Linq;
using System.Threading.Tasks;
using HTF2018.Backend.DataAccess;
using HTF2018.Backend.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;
using History = HTF2018.Backend.Common.Model.History;
using HTF2018.Backend.Common.Model;

namespace HTF2018.Backend.Logic
{
    public class HistoryLogic : IHistoryLogic
    {
        private readonly TheArtifactDbContext _dbContext;

        public HistoryLogic(TheArtifactDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<History> Pop()
        {
            var history = await _dbContext.History.OrderBy(x => x.SysId).FirstOrDefaultAsync();
            Status? status = history?.Status;
            if (history != null)
            {
                _dbContext.History.Remove(history);
            }
            await _dbContext.SaveChangesAsync();
            return new History { Status = status };
        }

        public async Task Push(Status status)
        {
            await _dbContext.History.AddAsync(new DataAccess.Entities.History { Id = Guid.NewGuid(), Status = status });
            await _dbContext.SaveChangesAsync();
        }
    }
}