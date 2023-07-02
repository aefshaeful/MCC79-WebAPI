using API.Contracts;
using API.DTOs.Education;
using API.Models;
using API.Services;
using API.Utilities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("api/education")]
    [Authorize(Roles = $"{nameof(RoleLevel.Admin)}")]
    public class EducationController : ControllerBase
    {
        private readonly EducationService _service;
        public EducationController(EducationService service)
        {
            _service = service;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var educations = _service.GetEducation();

            if (!educations.Any())
            {
                return NotFound(new ResponseHandler<GetEducationDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found!"
                });
            }
            return Ok(new ResponseHandler<IEnumerable<GetEducationDto>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data Found",
                Data = educations
            });
        }


        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var education = _service.GetEducation(guid);

            if (education is null)
            {
                return NotFound(new ResponseHandler<GetEducationDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid Not Found!"
                });
            }
            return Ok(new ResponseHandler<GetEducationDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Guid Found",
                Data = education
            });
        }


        [HttpPost]
        public IActionResult Create(NewEducationDto newEducationDto)
        {
            var createdEducation = _service.CreateEducation(newEducationDto);

            if (createdEducation is null)
            {
                return NotFound(new ResponseHandler<GetEducationDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Failed to Create!"
                });
            }
            return Ok(new ResponseHandler<GetEducationDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.Accepted.ToString(),
                Message = "Successfully Created",
                Data = createdEducation
            });
        }


        [HttpPut]
        public IActionResult Update(UpdateEducationDto updateEducationDto)
        {
            var isUpdated = _service.UpdateEducation(updateEducationDto);

            if (isUpdated is -1)
            {
                return NotFound(new ResponseHandler<UpdateEducationDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Failed to Update!"
                });
            }
            if (isUpdated is 0)
            {
                return BadRequest(new ResponseHandler<UpdateEducationDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Check Your Data"
                });
            }

            return Ok(new ResponseHandler<GetEducationDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.Accepted.ToString(),
                Message = "Data Accepted"
            });
        }


        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var isDeleted = _service.DeleteEducation(guid);

            if (isDeleted is -1)
            {
                return NotFound(new ResponseHandler<GetEducationDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid Not Found!"
                });
            }
            if (isDeleted is 0)
            {
                return BadRequest(new ResponseHandler<UpdateEducationDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Check Connection to Database"
                });
            }

            return Ok(new ResponseHandler<UpdateEducationDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.Accepted.ToString(),
                Message = "Successfully Deleted"
            });
        }
    }
}
