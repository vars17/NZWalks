using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;
using NZWalksAPI.Repositories;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]   //route:whenever user enters this route 
    [ApiController]               //along with appl.n url,it will be pointing to
                                  //this controller
                                  // :https://localhost:port/api/controller
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDBContext dbContext;

        public IRegionRepository RegionRepository { get; }

        public RegionsController(NZWalksDBContext dbContext, IRegionRepository regionRepository)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //get data from DB- domain models
            var regionsDomain = await RegionRepository.GetAllAsync();
            //convert domain models to dto's
            var regionDto = new List<RegionDto>();
            foreach (var regionDomain in regionsDomain)
            {
                regionDto.Add(new RegionDto()
                {
                    Id = regionDomain.Id,
                    Code = regionDomain.Code,
                    Name = regionDomain.Name,
                    RegionImageUrl = regionDomain.RegionImageUrl
                });
            }


            //return dtos

            return Ok(regionDto);
        }

        //GET SINGLE REGION(get Region by ID)
        //GET : https://localhost:port/api/regions/{id}

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //var region = dbContext.Regions.Find(id);
            //Get region domain model from DB

            var regionDomain = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (regionDomain == null)
            {
                return NotFound();
            }
            //map/convert region model -> region dto
            var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl

            };
            return Ok(regionDto);
        }

        //POST to create new region
        //POST:https://localhost:port/api/regions

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addregionreqDto)
        {
            //Map or Convert DTO to Domain model

            var regionDomainModel = new Region
            {
                Code = addregionreqDto.Code,
                Name = addregionreqDto.Name,
                RegionImageUrl = addregionreqDto.RegionImageUrl
            };

            //use domain model to create region
            await dbContext.Regions.AddAsync(regionDomainModel);
            await dbContext.SaveChangesAsync();

            //map domain model back to dto
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl

            };
            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        //Update region
        //PUT :URL
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateregreqdto)


        {
            var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            //map dto to domain model

            regionDomainModel.Code = updateregreqdto.Code;
            regionDomainModel.Name = updateregreqdto.Name;
            regionDomainModel.RegionImageUrl = updateregreqdto.RegionImageUrl;

            dbContext.SaveChanges();

            //convert domain model to dto

            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
            return Ok(regionDto);

        }

        //DELETE region
        //DELETE :URL
        [HttpDelete]
        [Route("{id:guid}")]

        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (regionDomainModel == null)
            {
                return NotFound();

            }
            //delete region if found
            dbContext.Regions.Remove(regionDomainModel);  //no asynchronous version for remove
            await dbContext.SaveChangesAsync();

            //return deleted region back
            //map domain model to dto

            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl

            };
            return Ok(regionDto);



        }

    }
}
