﻿namespace NZWalksAPI.Models.DTO
{
    public class WalkDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }

        public string? WalkImageUrl { get; set; }

        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }

        //to pass region and difficulty details
        public RegionDto Region { get; set; }

        public DifficultyDto Difficulty { get; set; }
    }
}
