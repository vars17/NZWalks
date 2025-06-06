using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repositories
{
    public interface IImageRespository
    {
        Task<Image> Upload(Image image);
    }
}
