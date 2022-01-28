using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using StockRoomProject.Interface;
using StockRoomProject.Models;
using static StockRoomProject.Models.EmployeeResponseModel;
using static StockRoomProject.Models.EmployeeRequestModel;
using static StockRoomProject.Models.EmployeeModel;
using System.Collections.Generic;


namespace StockRoomProject.Controllers
{
    [Route("[controller]/")]
    [ApiController]
    
    public class EmployeeController : ControllerBase
    {

        private readonly ILogger<EmployeeController> _logger;
        private readonly IEmployee _employee;

        public EmployeeController(ILogger<EmployeeController> logger, IEmployee employee)
        {
            _logger = logger;
            _employee = employee;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var result = await _employee.GetEmployees();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee(EmployeeRequestModel employee)
        {
            var result = await _employee.AddEmployee(employee);

            if (result)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetEmployeeId(int Id)
        {
            var result = await _employee.GetEmployee(Id);

            return Ok(result);

        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> EmployeeDelete(int Id)
        {
            var result = await _employee.DeleteEmployee(Id);
            if (result)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<IActionResult> EmployeeUpdate(EmployeeRequestModel employee)
        {
            var result = await _employee.UpdateEmployee(employee);
            if (result)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
