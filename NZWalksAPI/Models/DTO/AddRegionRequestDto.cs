using System.ComponentModel.DataAnnotations;

namespace NZWalksAPI.Models.DTO
{
    public class AddRegionRequestDto
    {
        [Required]
        [MinLength(3,ErrorMessage = "Code must be at least 3 characters long.")]
        [MaxLength(3, ErrorMessage = "Code must not exceed 3 characters.")]
        public string Code { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Name must not exceed 100 characters.")]

        public string Name { get; set; }

        //not using data annotations as return type cld be nullable
        public string? RegionImageUrl { get; set; }
    }
}
