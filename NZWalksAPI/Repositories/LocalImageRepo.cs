using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repositories
{
    public class LocalImageRepo : IImageRespository
    {
        private readonly IWebHostEnvironment webhostenv;
        private readonly IHttpContextAccessor httpcontacc;
        private readonly NZWalksDBContext dbContext;

        public LocalImageRepo(IWebHostEnvironment webhostenv,
            IHttpContextAccessor httpcontacc,NZWalksDBContext dbContext)
        {
            this.webhostenv = webhostenv;
            this.httpcontacc = httpcontacc;
            this.dbContext = dbContext;
        }
        public async Task<Image> Upload(Image image)
        {

            var localFilePath = Path.Combine(webhostenv.ContentRootPath, "Images",
                $"{image.FileName}{image.FileExtension}");

            //upload images to local path

            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            // https://localhost:1234/images/image.jpg

            var urlFilePath = $"{httpcontacc.HttpContext.Request.Scheme}://{httpcontacc.HttpContext.Request.Host}{httpcontacc.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";

            image.FilePath= urlFilePath;

            //add image to the images table
            await dbContext.Images.AddAsync(image);
            await dbContext.SaveChangesAsync();

            return image;

        }
    }
}
