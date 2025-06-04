using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Data
{
    public class NZWalksDBContext : DbContext
    {
        //create constructor
        public NZWalksDBContext(DbContextOptions <NZWalksDBContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        //dbset is a ppty of dbcontextclass that represnets
        //a collection of entities in the database
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //seed data for difficulties
            //hard,medium,easy

            var difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id =Guid.Parse("521a848c-bc36-4214-8bf9-eda6e28495c7"),
                    Name="Easy"

                },
                new Difficulty()
                {
                    Id =Guid.Parse("dfb11fe1-66c0-4fff-b99f-d30ffa72bd16"),
                    Name="Medium"

                },
                new Difficulty()
                {
                    Id =Guid.Parse("45ebfa2f-371f-4879-9370-e5cf7effb71a"),
                    Name="Diffcult"

                },


            };

            modelBuilder.Entity<Difficulty>().HasData(difficulties);   //default values are automatically added by this method

            //seed data for regions
            var regions = new List<Region>()
            {
                new Region()
                {
                    Id = Guid.Parse("02fa0853-7bca-4d19-9f53-dc0a5e9ddc91"),
                    Name = "Auckland",
                    Code = "AKL",
                    RegionImageUrl = "https://images.pexels.com/photos/5169056/pexels-photo-5169056.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region
                {
                    Id = Guid.Parse("421cc33d-34b9-49c6-a76b-6f4bfb1344ff"),
                    Name = "Northland",
                    Code = "NTL",
                    RegionImageUrl = null
                },
                new Region
                {
                    Id = Guid.Parse("ca085225-39e9-4c98-87ba-8fc1ed57c791"),
                    Name = "Bay Of Plenty",
                    Code = "BOP",
                    RegionImageUrl = null
                },
                new Region
                {
                    Id = Guid.Parse("4848ae00-78e7-4418-93fe-58bc87944056"),
                    Name = "Wellington",
                    Code = "WGN",
                    RegionImageUrl = "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region
                {
                    Id = Guid.Parse("d8dbfada-d782-41bf-96fb-6690145ba2b5"),
                    Name = "Nelson",
                    Code = "NSN",
                    RegionImageUrl = "https://images.pexels.com/photos/13918194/pexels-photo-13918194.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region
                {
                    Id = Guid.Parse("d5d77a79-0af8-43f1-a59c-66183b51b3a9"),
                    Name = "Southland",
                    Code = "STL",
                    RegionImageUrl = null
                },
            };

            modelBuilder.Entity<Region>().HasData(regions); //default values are automatically added by this method
        }
    }
}
