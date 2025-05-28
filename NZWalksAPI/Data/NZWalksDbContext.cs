using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Data
{
    public class NZWalksDBContext : DbContext
    {
        //create constructor
        public NZWalksDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        //dbset is a ppty of dbcontextclass that represnets
        //a collection of entities in the database
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }

    }
}
