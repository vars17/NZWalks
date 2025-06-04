using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.CustomActionFilters;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;
using NZWalksAPI.Repositories;

namespace NZWalksAPI.Controllers
{
    //api/walks
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }

        public IWalkRepository WalkRepository { get; }

        //CREATE Walk
        //POST :url
        [HttpPost]
        [ValidateModel]

        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {

            //map dto to domail model
            var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);
            await walkRepository.CreateAsync(walkDomainModel);
            var walkDto = mapper.Map<WalkDto>(walkDomainModel);

            return Ok(walkDto);




        }

        //GET Walks
        //GET :/api/walks?filterOn=Name&filterQuery=mountain&SortBy=Name&SortOrder=asc&pageNumber=1&pageSize=10
        [HttpGet]

        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber=1, [FromQuery] int pageSize=1000)
        {
            //get all walks from repository
            var walksDomainModel = await walkRepository.GetAllAsync(filterOn,filterQuery,sortBy,isAscending?? true,pageNumber,pageSize);
            //map domain model to dto
            var walksDto = mapper.Map<List<WalkDto>>(walksDomainModel);
            return Ok(walksDto);

        }
        //GET Walk by Id
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute]Guid id)
        {
            //get walk by id from repository
            var walkDomainModel = await walkRepository.GetAsync(id);
            if (walkDomainModel == null)
            {
                return NotFound();
            }
            //map domain model to dto
            var walkDto = mapper.Map<WalkDto>(walkDomainModel);
            return Ok(walkDto);
        }

        //PUT
        //Update by id
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]

        public async Task<IActionResult> Update([FromRoute] Guid id,UpdateWalkRequestDto updatewalkdto)
        {

            //map dto to domain model
            var walkDomainModel = mapper.Map<Walk>(updatewalkdto);
            walkDomainModel.Id = id;
            //update walk in repository
            var updatedWalk = await walkRepository.UpdateAsync(id, walkDomainModel);
            if (updatedWalk == null)
            {
                return NotFound();
            }
            //map domain model to dto

            return Ok(mapper.Map<WalkDto>(updatedWalk));


        }

        //DELETE
        [HttpDelete]
        [Route("{id:Guid}")]

        public async Task<IActionResult> Delete([FromRoute]  Guid id)
        {
            var deletedDomainModel = await walkRepository.DeleteAsync(id);
            if(deletedDomainModel==null)
            {
                return NotFound();
            }

            //map domain model to dto
            return Ok(mapper.Map<WalkDto>(deletedDomainModel));


        }
    }
}
