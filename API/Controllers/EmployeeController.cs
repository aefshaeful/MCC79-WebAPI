using API.Contracts;
using Microsoft.AspNetCore.Mvc;
using API.Models;


namespace API.Controllers
{
    [ApiController]
    [Route("api/employee")]

    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository repository;
        public EmployeeController(IEmployeeRepository Repository) 
        {
            this.repository = Repository;
        }

        [HttpGet]
        public IActionResult GetAll() 
        {
            var employee = repository.GetAll();

            if (!employee.Any())
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var employee = repository.GetByGuid(guid);
            
            if(employee is null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpPost]
        public IActionResult Create(Employee employee) 
        {
            var createdEmployee = repository.Create(employee);
            return Ok(createdEmployee);
        }

        [HttpPut]
        public IActionResult Update(Employee employee) 
        {
            var isUpdate = repository.Update(employee);

            if (!isUpdate) 
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var isDelete = repository.Delete(guid);

            if(!isDelete)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
