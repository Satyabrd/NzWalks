using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;
using NZWalks.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(NZWalksDbContext dbContext,
            IRegionRepository regionRepository,
            IMapper mapper,
            ILogger<RegionsController> logger) 
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        //Get all regions
        [HttpGet]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll()
        {
            var regionsDomain = await regionRepository.GetAllAsync();
            logger.LogInformation($"Get All Regions Method was invoked with data : {JsonSerializer.Serialize(regionsDomain)}");
            var regionDTOs = mapper.Map<List<RegionDTO>>(regionsDomain);
            return Ok(regionDTOs);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var region = await regionRepository.GetByIDAsync(id);
            if (region == null)
            {
                return NotFound();
            }
            //Map/convert Region domain model to Region DTO
            var regionDto = mapper.Map<RegionDTO>(region);
            return Ok(regionDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            //Map or Convert DTO to Domain Model
            var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);
            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);


            //Map Domain model back to DTO
            var regionDto = mapper.Map<RegionDTO>(regionDomainModel);
            //It returns a 201 response 
            return CreatedAtAction(nameof(GetById), new { id = regionDomainModel.Id }, regionDto);
        }

        //update region
        //PUT: https:/localhost:portnumber/api/regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody]UpdateRegionRequestDto updateRegionRequestDto)
        {
            //Map dto to domain model to pass it to repo
            var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);

            //Find the data from db first
            regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);

            //Map Domain model back to DTO
            var regionDto = mapper.Map<RegionDTO>(regionDomainModel);
            //It returns a 201 response
            return Ok(regionDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute]Guid id)
        {
            //Find the data from db first
            var regionDomainModel = await regionRepository.DeleteAsync(id);
            //If not found return not found
            if (regionDomainModel == null)
            {
                return NotFound();
            }
            //return the deleted region back
            //Map domain model to dto first and we can use regionDTO.cs
            var regionDto = mapper.Map<RegionDTO>(regionDomainModel);
            return Ok(regionDto);
        }
    }
}

