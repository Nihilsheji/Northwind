using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Northwind.Models.Entities;
using Northwind.Models.Request.Demographic;
using Northwind.Models.Response;
using Northwind.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Controllers
{
    [ApiController]
    [Route("demographics")]
    public class DemographicsController : Controller
    {
        private readonly IDemographicsService _demo;
        private readonly IMapper _mapper;

        public DemographicsController(IDemographicsService demo, IMapper mapper)
        {
            _demo = demo;
            _mapper = mapper;

        }

        /// <summary>
        /// Returns dictionary of id, name values
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("dictionary")]
        public async Task<ActionResult<IEnumerable<DictionaryValue<int, string>>>> GetDictionary() 
        {
            var result = await _demo.GetDictionary();

            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DemographicView>>> GetAllDemographics()
        {
            var result = await _demo.Get();

            return Ok(_mapper.Map<IEnumerable<DemographicView>>(result));
        }

        [HttpGet]
        [Route("{demographicId}")]
        public async Task<ActionResult<IEnumerable<DemographicView>>> GetDemographic(int demographicId)
        {
            var result = await _demo.Get(demographicId);

            return Ok(_mapper.Map<IEnumerable<DemographicView>>(result));
        }

        [HttpPost]
        [Route("{demographicId}/customers/{customerId}")]
        public async Task<ActionResult<bool>> AddCustomerToDemographic(int demographicId, string customerId)
        {
            var result = await _demo.AddCustomerToDemographic(demographicId, customerId);

            return Ok(result);
        }

        [HttpDelete]
        [Route("{demographicId}/customers/{customerId}")]
        public async Task<ActionResult<bool>> RemoveCustomerFromDemographic(int demographicId, string customerId)
        {
            var result = await _demo.RemoveCustomerFromDemographic(demographicId, customerId);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<DemographicView>> PostDemographic([FromBody] CreateDemographicRequest req) 
        {
            var result = await _demo.Create(_mapper.Map<Demographic>(req));

            return Ok(_mapper.Map<DemographicView>(result));
        }

        [HttpDelete]
        [Route("{demoId}")]
        public async Task<ActionResult<bool>> DeleteDemographic(int demoId)
        {
            var result = await _demo.Remove(demoId);

            return result;
        }

        [HttpPut]
        [Route("{demoId}")]
        public async Task<ActionResult<DemographicView>> UpdateDemographic([FromBody] UpdateDemographicRequest req)
        {
            var result = await _demo.Update(_mapper.Map<Demographic>(req));

            return Ok(_mapper.Map<DemographicView>(result));
        }
    }
}
