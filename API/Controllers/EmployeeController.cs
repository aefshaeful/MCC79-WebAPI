using API.Contracts;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Services;
using API.DTOs.Employee;
using API.Utilities.Enums;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [ApiController]
    [Route("api/employee")]
    [Authorize(Roles = $"{nameof(RoleLevel.Admin)}")]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService _service;
        public EmployeeController(EmployeeService service)
        {
            _service = service;
        }


        [HttpGet("get-all-master")]
        public IActionResult GetMaster()
        {
            var employeeMasters = _service.GetAllMaster();

            if (employeeMasters == null)
            {
                return NotFound(new ResponseHandler<EmployeeMasterDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found!"
                });
            }
            return Ok(new ResponseHandler<IEnumerable<EmployeeMasterDto>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data Found",
                Data = employeeMasters
            });
        }


        [HttpGet("get-master/{guid}")]
        public IActionResult GetMasterByGuid(Guid guid)
        {
            var employee = _service.GetMasterByGuid(guid);
            if (employee is null)
            {
                return NotFound(new ResponseHandler<EmployeeMasterDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data not found"
                });
            }

            return Ok(new ResponseHandler<EmployeeMasterDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data found",
                Data = employee
            });
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var employees = _service.GetEmployee();

            if (!employees.Any())
            {
                return NotFound(new ResponseHandler<GetEmployeeDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found!"
                });
            }
            return Ok(new ResponseHandler<IEnumerable<GetEmployeeDto>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data Found",
                Data = employees
            });
        }


        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var employee = _service.GetEmployee(guid);

            if (employee is null)
            {
                return NotFound(new ResponseHandler<GetEmployeeDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid Not Found!"
                });
            }
            return Ok(new ResponseHandler<GetEmployeeDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Guid Found",
                Data = employee
            });
        }


        [HttpPost]
        public IActionResult Create(NewEmployeeDto newEmployeeDto)
        {
            var createdEmployee = _service.CreateEmployee(newEmployeeDto);

            if (createdEmployee is null)
            {
                return NotFound(new ResponseHandler<GetEmployeeDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Failed to Create!"
                });
            }
            return Ok(new ResponseHandler<GetEmployeeDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.Accepted.ToString(),
                Message = "Successfully Created",
                Data = createdEmployee
            });
        }


        [HttpPut]
        public IActionResult Update(UpdateEmployeeDto updateEmployeeDto)
        {
            var isUpdated = _service.UpdateEmployee(updateEmployeeDto);

            if (isUpdated is -1)
            {
                return NotFound(new ResponseHandler<UpdateEmployeeDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Failed to Update!"
                });
            }
            if (isUpdated is 0)
            {
                return BadRequest(new ResponseHandler<UpdateEmployeeDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Check Your Data"
                });
            }

            return Ok(new ResponseHandler<GetEmployeeDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.Accepted.ToString(),
                Message = "Data Accepted"
            });
        }


        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var isDeleted = _service.DeleteEmployee(guid);

            if (isDeleted is -1)
            {
                return NotFound(new ResponseHandler<GetEmployeeDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid Not Found!"
                });
            }
            if (isDeleted is 0)
            {
                return BadRequest(new ResponseHandler<UpdateEmployeeDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Check Connection to Database"
                });
            }

            return Ok(new ResponseHandler<UpdateEmployeeDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.Accepted.ToString(),
                Message = "Successfully Deleted"
            });
        }



        /*[HttpGet("get-by-name/{name}")]
        public IActionResult GetByName(string name)
        {
            var employee = _service.GetEmployee(name);
            if (!employee.Any())
            {
                return NotFound(new ResponseHandler<GetEmployeeDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Not Universities Found With The Given Name!"
                });
            }

            return Ok(new ResponseHandler<IEnumerable<GetEmployeeDto>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Universities Found",
                Data = employee
            });
        }*/
    }
}
