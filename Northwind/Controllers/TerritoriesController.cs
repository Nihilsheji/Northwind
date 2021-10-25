using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Northwind.Models.Entities;
using Northwind.Models.Request.Territory;
using Northwind.Models.Response;
using Northwind.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Controllers
{
    [ApiController]
    [Route("territories")]
    public class TerritoriesController : Controller
    {
        private readonly ITerritoriesService _territories;
        private readonly IMapper _mapper;

        public TerritoriesController(ITerritoriesService territories, IMapper mapper)
        {
            _territories = territories;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TerritoryView>>> GetAllTerritories()
        {
            var result = await _territories.Get();

            return Ok(_mapper.Map<IEnumerable<TerritoryView>>(result));
        }

        [HttpGet]
        [Route("region/{regionId}")]
        public async Task<ActionResult<IEnumerable<TerritoryView>>> GetTerritoriesForRegion(int regionId)
        {
            var result = await _territories.GetTerritoriesForRegion(regionId);

            return Ok(_mapper.Map<IEnumerable<TerritoryView>>(result));
        }

        [HttpGet]
        [Route("employee/{employeeId}")]
        public async Task<ActionResult<IEnumerable<TerritoryView>>> GetTerritoriesForEmployee(int employeeId)
        {
            var result = await _territories.GetTerritoriesForEmployee(employeeId);

            return Ok(_mapper.Map<IEnumerable<TerritoryView>>(result));
        }

        [HttpGet]
        [Route("{territoryId}")]
        public async Task<ActionResult<TerritoryView>> GetTerritory(int territoryId)
        {
            var result = await _territories.Get(territoryId);

            return Ok(_mapper.Map<TerritoryView>(result));
        }

        [HttpPost]
        public async Task<ActionResult<TerritoryView>> CreateTerritory([FromBody] CreateTerritoryRequest req)
        {
            var result = await _territories.Create(_mapper.Map<Territory>(req));

            return Ok(result);
        }

        [HttpPut]
        [Route("{territoryId}")]
        public async Task<ActionResult<TerritoryView>> UpdateTerritory([FromBody] UpdateTerritoryRequest req)
        {
            var result = await _territories.Update(_mapper.Map<Territory>(req));

            return Ok(result);
        }

        [HttpPost]
        [Route("{territoryId}/employees/{employeeId}")]
        public async Task<ActionResult<bool>> AddEmployeeToTerritory(int territoryId, int employeeId)
        {
            var result = await _territories.AddEmployeeToTerritory(territoryId, employeeId);

            return Ok(result);
        }

        [HttpDelete]
        [Route("{territoryId}/employees/{employeeId}")]
        public async Task<ActionResult<bool>> RemoveEmployeeFromTerritory(int territoryId, int employeeId)
        {
            var result = await _territories.RemoveEmployeeFromTerritory(territoryId, employeeId);

            return Ok(result);
        }
    }
}
