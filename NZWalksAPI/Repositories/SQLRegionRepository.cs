using NZWalksAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;

namespace NZWalksAPI.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NZWalksDBContext dbContext;
        public SQLRegionRepository(NZWalksDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<Region>> GetAllAsync()
        {
            //throw new NotImplementedException();
            await dbContext.Regions.ToListAsync();
        }
    }
}
