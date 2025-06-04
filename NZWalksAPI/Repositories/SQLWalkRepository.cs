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

        public async Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy=null,bool isAscending=true,int pageNumber=1,int pageSize=1000)
        {
            //return await dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
            var walks = dbContext.Walks
                .Include("Difficulty")
                .Include("Region")
                .AsQueryable();

            //Filtering
            if(string.IsNullOrWhiteSpace(filterOn)==false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if(filterOn.Equals("Name",StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
                //else if (filteron.equals("region", stringcomparison.ordinalignorecase))
                //{
                //    walks = walks.where(x => x.region.name.contains(filterquery, stringcomparison.ordinalignorecase));
                //}
                //else if (filteron.equals("difficulty", stringcomparison.ordinalignorecase))
                //{
                //    walks = walks.where(x => x.difficulty.name.contains(filterquery, stringcomparison.ordinalignorecase));
                //}

            }

            //Sorting
            if (string.IsNullOrWhiteSpace(sortBy)==false)
            {
                if(sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }
                else if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
                }
                //else if (sortBy.Equals("Region", StringComparison.OrdinalIgnoreCase))
                //{
                //    walks = isAscending ? walks.OrderBy(x => x.Region.Name) : walks.OrderByDescending(x => x.Region.Name);
                //}
                //else if (sortBy.Equals("Difficulty", StringComparison.OrdinalIgnoreCase))
                //{
                //    walks = isAscending ? walks.OrderBy(x => x.Difficulty.Name) : walks.OrderByDescending(x => x.Difficulty.Name);
                //}
                // Add more sorting options as needed
            }

            //Pagination
            var skip = (pageNumber - 1) * pageSize;
            return await walks.Skip(skip).Take(pageSize).ToListAsync();
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
