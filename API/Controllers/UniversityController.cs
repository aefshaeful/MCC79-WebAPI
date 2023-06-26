using Microsoft.AspNetCore.Mvc;
using API.Utilities.Enums;
using Microsoft.AspNetCore.Http.Headers;
using API.Models;
using API.Contracts;
using System.Net;
using API.Services;
using API.DTOs.Universities;

namespace API.Controllers
{
    [ApiController]
    [Route("api/universities")]
    public class UniversityController : ControllerBase
    {
        private readonly UniversityService _service;
        public UniversityController(UniversityService service)
        {
            _service = service;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var entity = _service.GetUniversity();

            if (!entity.Any())
            {
                return NotFound(new ResponseHandler<GetUniversityDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found!"
                });
            }
            return Ok(new ResponseHandler<IEnumerable<GetUniversityDto>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data Found",
                Data = entity
            });
        }


        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var university = _service.GetUniversity(guid);

            if (university is null)
            {
                return NotFound(new ResponseHandler<GetUniversityDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid Not Found!"
                });
            }
            return Ok(new ResponseHandler<GetUniversityDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Guid Found",
                Data = university
            });
        }


        [HttpPost]
        public IActionResult Create(NewUniversityDto newUniversityDto)
        {
            var createdUniversity = _service.CreateUniversity(newUniversityDto);

            if (createdUniversity is null)
            {
                return NotFound(new ResponseHandler<GetUniversityDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Failed to Create!"
                });
            }
            return Ok(new ResponseHandler<GetUniversityDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.Accepted.ToString(),
                Message = "Successfully Created",
                Data = createdUniversity
            });
        }


        [HttpPut]
        public IActionResult Update(UpdateUniversityDto updateUniversityDto)
        {
            var isUpdated = _service.UpdateUniversity(updateUniversityDto);

            if (isUpdated is -1)
            {
                return NotFound(new ResponseHandler<UpdateUniversityDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Failed to Update!"
                });
            }
            if (isUpdated is 0)
            {
                return BadRequest(new ResponseHandler<UpdateUniversityDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Check Your Data"
                });
            }

            return Ok(new ResponseHandler<GetUniversityDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.Accepted.ToString(),
                Message = "Data Accepted"
            });
        }


        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var isDeleted = _service.DeleteUniversity(guid);

            if (isDeleted is -1)
            {
                return NotFound(new ResponseHandler<GetUniversityDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid Not Found!"
                });
            }
            if (isDeleted is 0)
            {
                return BadRequest(new ResponseHandler<UpdateUniversityDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Check Connection to Database"
                });
            }

            return Ok(new ResponseHandler<UpdateUniversityDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.Accepted.ToString(),
                Message = "Successfully Deleted"
            });
        }


        [HttpGet("get-by-name/{name}")]
        public IActionResult GetByName(string name)
        {
            var university = _service.GetUniversity(name);
            if (!university.Any())
            {
                return NotFound(new ResponseHandler<GetUniversityDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Not Universities Found With The Given Name!"
                });
            }

            return Ok(new ResponseHandler<IEnumerable<GetUniversityDto>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Universities Found",
                Data = university
            });
        }
    }
}
