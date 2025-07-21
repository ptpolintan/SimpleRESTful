using Microsoft.AspNetCore.Mvc;
using SimpleRESTful.API.DTOs.WebRequest;
using SimpleRESTful.API.Extensions;
using SimpleRESTful.Application.Employees.Interfaces;

namespace SimpleRESTful.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeesController : ControllerBase
    {
        public readonly IEmployeeService service;

        public EmployeesController(IEmployeeService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            return (await this.service.GetEmployeesAsync()).AsHttpResponse();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById([FromRoute] int id)
        {
            return (await this.service.GetEmployeeByIdAsync(id)).AsHttpResponse();
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeWebRequest request)
        {
            return (await this.service.CreateEmployeeAsync(request.AsDTO())).AsHttpResponse();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployees([FromRoute] int id, [FromBody] UpdateEmployeeWebRequest body)
        {
            var request = new UpdateEmployeeWebRequest
            {
                Id = id,
                FirstName = body.FirstName,
                MiddleName = body.MiddleName,
                LastName = body.LastName
            };

            return (await this.service.UpdateEmployeeAsync(request.AsDTO())).AsHttpResponse();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployees([FromRoute] int id)
        {
            return (await this.service.DeleteEmployeeAsync(id)).AsHttpResponse();
        }
    }
}
