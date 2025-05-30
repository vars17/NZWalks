using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NZWalksDBContext dbContext;
        //private readonly IMapper mapper;

        public SQLWalkRepository(NZWalksDBContext dbContext)
        {
            this.dbContext = dbContext;
            //this.mapper = mapper;
        }
        public async Task<Walk> CreateAsync(Walk walk)
        {
            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;
            
        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var deletedWalk = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (deletedWalk == null)
            {
                return null;
            }
            dbContext.Walks.Remove(deletedWalk);
            await dbContext.SaveChangesAsync();
            return deletedWalk;
        }

        public async Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null)
        {
            return await dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();

        }

        public async Task<Walk?> GetAsync(Guid id)
        {
            return await dbContext.Walks
                .Include("Difficulty")
                .Include("Region")
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk?> UpdateAsync(Guid id,Walk walk)
        {
            //get walk by id
            var existingWalk = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingWalk != null)
            {
                //update the existing walk with new values
                existingWalk.Name = walk.Name;
                existingWalk.LengthInKm = walk.LengthInKm;
                existingWalk.RegionId = walk.RegionId;
                existingWalk.DifficultyId = walk.DifficultyId;
                existingWalk.Description = walk.Description;
                existingWalk.WalkImageUrl = walk.WalkImageUrl;
                //save changes to the database
                await dbContext.SaveChangesAsync();
                return existingWalk;
            }
            return null; //if not found, return null
        }
    }
}
