using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Data;
using NZWalksAPI.Models;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]   //route:whenever user enters this route 
    [ApiController]               //along with appl.n url,it will be pointing to
                                  //this controller
                                  // :https://localhost:port/api/controller
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDBContext dbContext;

        public RegionsController(NZWalksDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            //get data from DB- domain models
            var regionsDomain = dbContext.Regions.ToList();
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
        public IActionResult GetById([FromRoute] Guid id)
        {
            //var region = dbContext.Regions.Find(id);
            //Get region domain model from DB

            var regionDomain = dbContext.Regions.FirstOrDefault(x => x.Id == id);
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
        public IActionResult Create([FromBody] AddRegionRequestDto addregionreqDto)
        {
            //Map or Convert DTO to Domain model

            var regionDomainModel = new Region
            {
                Code = addregionreqDto.Code,
                Name = addregionreqDto.Name,
                RegionImageUrl = addregionreqDto.RegionImageUrl
            };

            //use domain model to create region
            dbContext.Regions.Add(regionDomainModel);
            dbContext.SaveChanges();

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
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateregreqdto)


        {
            var regionDomainModel = dbContext.Regions.FirstOrDefault(x => x.Id == id);

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

        public IActionResult Delete([FromRoute] Guid id)
        {
            var regionDomainModel = dbContext.Regions.FirstOrDefault(x => x.Id == id);
            if(regionDomainModel==null)
            {
                return NotFound();

            }
            //delete region if found
            dbContext.Regions.Remove(regionDomainModel);
            dbContext.SaveChanges();

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
