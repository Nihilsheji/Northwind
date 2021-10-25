using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Northwind.Models.Entities;
using Northwind.Models.Request.Regions;
using Northwind.Models.Response;
using Northwind.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Controllers
{
    [ApiController]
    [Route("regions")]
    public class RegionsController : Controller
    {
        private readonly IRegionsService _regions;
        private readonly IMapper _mapper;

        public RegionsController(IRegionsService regions, IMapper mapper)
        {
            _regions = regions;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RegionView>>> GetAllRegions()
        {
            var result = await _regions.Get();

            return Ok(_mapper.Map<IEnumerable<RegionView>>(result));
        }

        [HttpGet]
        [Route("{regionId}")]
        public async Task<ActionResult<RegionView>> GetRegion(int regionId)
        {
            var result = await _regions.Get(regionId);

            return Ok(_mapper.Map<RegionView>(result));
        }

        [HttpPost]
        public async Task<ActionResult<CreatedRegionView>> CreateRegion([FromBody] CreateRegionRequest req)
        {
            var result = await _regions.Create(_mapper.Map<CreateRegionRequest, Region>(req));

            return Ok(_mapper.Map<CreatedRegionView>(result));
        }

        [HttpDelete]
        [Route("{regionId}")]
        public async Task<ActionResult<bool>> DeleteRegion(int regionId)
        {
            var result = await _regions.Remove(regionId);

            return Ok(result);
        }

        [HttpPut]
        [Route("{regionId}")]
        public async Task<ActionResult<RegionView>> UpdateRegion([FromBody] UpdateRegionRequest req)
        {
            var result = await _regions.Create(_mapper.Map<UpdateRegionRequest, Region>(req));

            return Ok(_mapper.Map<RegionView>(result));
        }
    }
}
