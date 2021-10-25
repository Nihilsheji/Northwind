using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Northwind.Models.Entities;
using Northwind.Models.Request.Employee;
using Northwind.Models.Response;
using Northwind.Services.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Northwind.Controllers
{
    [ApiController]
    [Route("employees")]
    public class EmployeesController : Controller
    {
        private readonly IEmployeesService _employees;
        private readonly IMapper _mapper;

        public EmployeesController(IEmployeesService employees, IMapper mapper)
        {
            _employees = employees;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeView>>> GetAllEmployees()
        {
            var result = await _employees.Get();

            return Ok(_mapper.Map<IEnumerable<EmployeeView>>(result));
        }

        [HttpGet]
        [Route("dictionary")]
        public async Task<ActionResult<IEnumerable<DictionaryValue<int, string>>>> GetDictionary()
        {
            var result = await _employees.GetDictionary();

            return Ok(result);
        }

        [HttpGet]
        [Route("list")]
        public async Task<ActionResult<IEnumerable<EmployeeListView>>> GetEmployeesListView()
        {
            var result = await _employees.GetEmployeesListView();

            return Ok(result);
        } 

        [HttpPost]
        public async Task<ActionResult<EmployeeView>> CreateEmployee([FromBody] CreateEmployeeRequest req) 
        {
            var result = await _employees.Create(_mapper.Map<CreateEmployeeRequest, Employee>(req));

            return Ok(_mapper.Map<EmployeeView>(result));
        }

        [HttpDelete]
        [Route("{employeeId}")]

        public async Task<ActionResult<bool>> DeleteEmployee(int employeeId)
        {
            var result = await _employees.Remove(employeeId);

            return Ok(result);
        }

        [HttpPatch]
        [Route("{employeeId}")]
        public async Task<ActionResult<EmployeeView>> UpdateEmployee([FromBody] UpdateEmployeeRequest req)
        {
            var result = await _employees.Create(_mapper.Map<UpdateEmployeeRequest, Employee>(req));

            return Ok(_mapper.Map<EmployeeView>(result));
        }

        [HttpPost]
        [Route("{employeeId}/territories/{territoryId}")]
        public async Task<ActionResult<bool>> AddTerritoryToEmployee(int employeeId, int territoryId)
        {
            var result = await _employees.AddTerritoryToEmployee(employeeId, territoryId);
            
            return result;
        }

        [HttpDelete]
        [Route("{employeeId}/territories/{territoryId}")]
        public async Task<ActionResult<bool>> RemoveTerritoryFromEmployee(int employeeId, int territoryId)
        {
            var result = await _employees.RemoveTerritoryFromEmployee(employeeId, territoryId);

            return result;
        }
    }
}
