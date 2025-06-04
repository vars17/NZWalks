using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalksAPI.Data
{
    public class NZWalksAuthDbContext : IdentityDbContext
    {
        public NZWalksAuthDbContext(DbContextOptions <NZWalksAuthDbContext>options) : base(options)
        {
   

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var readerRoleId = "82d9c2cb-e8b3-400c-95e4-c47324a558e2";
            var writerRoleId = "6a88f428-aefa-431b-9be9-0280406dd04b";

            var roles=new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = readerRoleId,
                    ConcurrencyStamp=readerRoleId,
                    Name = "Reader",
                    NormalizedName = "READER"
                },
                new IdentityRole
                {
                    Id = writerRoleId,
                    ConcurrencyStamp=writerRoleId,
                    Name = "Writer",
                    NormalizedName = "WRITER"
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }


    }
}
