﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalksAPI.CustomActionFilters;
using NZWalksAPI.Data;
using NZWalksAPI.Mappings;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;
using NZWalksAPI.Repositories;
using System.Text.Json;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]   //route:whenever user enters this route 
    [ApiController]
                     //along with appl.n url,it will be pointing to
                                  //this controller
                                  // :https://localhost:port/api/controller
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDBContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;

        public IRegionRepository RegionRepository { get; }

        public RegionsController(NZWalksDBContext dbContext, IRegionRepository regionRepository,
            IMapper mapper,ILogger<RegionsController> logger)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.logger = logger;
        }


        [HttpGet]
        //[Authorize(Roles ="Reader")]

        public async Task<IActionResult> GetAll()
        {

            //try
            //{
                //throw new Exception("This is a test exception for logging purposes.");

                //logger.LogInformation("GetAll Action Method was invoked");

                //get data from DB- domain models
                var regionsDomain = await regionRepository.GetAllAsync();
                //convert domain models to dto's
                //var regiondto = new list<regiondto>();
                //foreach (var regiondomain in regionsdomain)
                //{
                //    regiondto.add(new regiondto()
                //    {
                //        id = regiondomain.id,
                //        code = regiondomain.code,
                //        name = regiondomain.name,
                //        regionimageurl = regiondomain.regionimageurl
                //    });
                //}

                //map domain model to dto




                //return dtos
                //logger.LogInformation($"Finished GetAllRegions request with data : {JsonSerializer.Serialize(regionsDomain)} ");

                return Ok(mapper.Map<List<RegionDto>>(regionsDomain));
            
        
                //logger.LogError(ex, ex.Message);
                //return StatusCode(500, "Internal server error. Please try again later.");
                //throw;
            
        }

        //GET SINGLE REGION(get Region by ID)
        //GET : https://localhost:port/api/regions/{id}

        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles ="Reader")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //var region = dbContext.Regions.Find(id);
            //Get region domain model from DB

            var regionDomain = await regionRepository.GetByIdAsync(id);
            if (regionDomain == null)
            {
                return NotFound();
            }
            //map/convert region model -> region dto

            
            return Ok(mapper.Map<RegionDto>(regionDomain));
        }

        //POST to create new region
        //POST:https://localhost:port/api/regions

        [HttpPost]
        [ValidateModel]
        [Authorize(Roles="Writer")]
        //custom action filter to validate model
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addregionreqDto)

        {
            var regionDomainModel = mapper.Map<Region>(addregionreqDto);


            //Map or Convert DTO to Domain model

            //var regionDomainModel = new Region
            //{
            //    Code = addregionreqDto.Code,
            //    Name = addregionreqDto.Name,
            //    RegionImageUrl = addregionreqDto.RegionImageUrl
            //};



            //use repository to create region
            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

                //map domain model back to dto
                //var regionDto = new RegionDto
                //{
                //    Id = regionDomainModel.Id,
                //    Code = regionDomainModel.Code,
                //    Name = regionDomainModel.Name,
                //    RegionImageUrl = regionDomainModel.RegionImageUrl

                //};
             var regionDto = mapper.Map<RegionDto>(regionDomainModel);
                        return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);

            
            
        }

        //Update region
        //PUT :URL
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]

        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateregreqdto)



        {
            //map dto to domain model

            //var regionDomainModel = new Region()
            //{
            //    Code = updateregreqdto.Code,
            //    Name = updateregreqdto.Name,
            //    RegionImageUrl = updateregreqdto.RegionImageUrl
            //};


            var regionDomainModel = mapper.Map<Region>(updateregreqdto);



            regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);

            if (regionDomainModel == null)
            {
                return NotFound();
            }







            //convert domain model to dto

            //var regionDto = new RegionDto
            //{
            //    Id = regionDomainModel.Id,
            //    Code = regionDomainModel.Code,
            //    Name = regionDomainModel.Name,
            //    RegionImageUrl = regionDomainModel.RegionImageUrl
            //};
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);
            return Ok(regionDto);

        }
            

        

        //DELETE region
        //DELETE :URL
        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles ="Writer")]

        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel = await regionRepository.DeleteAsync(id);
            if (regionDomainModel == null)
            {
                return NotFound();

            }
            

            //return deleted region back
            //map domain model to dto

            //var regionDto = new RegionDto
            //{
            //    Id = regionDomainModel.Id,
            //    Code = regionDomainModel.Code,
            //    Name = regionDomainModel.Name,
            //    RegionImageUrl = regionDomainModel.RegionImageUrl

            //};
            var regionDto=mapper.Map<RegionDto>(regionDomainModel);
            return Ok(regionDto);



        }

    }
}
