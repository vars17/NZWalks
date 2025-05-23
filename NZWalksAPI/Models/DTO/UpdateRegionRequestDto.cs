namespace NZWalksAPI.Models.DTO
{
    public class UpdateRegionRequestDto
    {
        //id shouldn't be updated by user
        //all the below ppties are allowed to be modified
        public string Code { get; set; }

        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
