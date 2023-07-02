using API.Models;
using API.Contracts;
using Microsoft.AspNetCore.Mvc;
using API.DTOs.AccountRole;
using API.Services;
using API.Utilities.Enums;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [ApiController]
    [Route("api/accountrole")]
    [Authorize(Roles = $"{nameof(RoleLevel.Admin)}, {nameof(RoleLevel.Manager)}")]
    public class AccountRoleController : ControllerBase
    {
        private readonly AccountRoleService _service;
        public AccountRoleController(AccountRoleService service)
        {
            _service = service;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var accountsRole = _service.GetAccountRole();

            if (accountsRole == null)
            {
                return NotFound(new ResponseHandler<GetAccountRoleDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found!"
                });
            }
            return Ok(new ResponseHandler<IEnumerable<GetAccountRoleDto>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data Found",
                Data = accountsRole
            });
        }


        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var accountRole = _service.GetAccountRole(guid);

            if (accountRole is null)
            {
                return NotFound(new ResponseHandler<GetAccountRoleDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid Not Found!"
                });
            }
            return Ok(new ResponseHandler<GetAccountRoleDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Guid Found",
                Data = accountRole
            });
        }


        [HttpPost]
        public IActionResult Create(NewAccountRoleDto newAccountRoleDto)
        {
            var createdAccountRole = _service.CreateAccountRole(newAccountRoleDto);

            if (createdAccountRole is null)
            {
                return NotFound(new ResponseHandler<GetAccountRoleDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Failed to Create!"
                });
            }
            return Ok(new ResponseHandler<GetAccountRoleDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.Accepted.ToString(),
                Message = "Successfully Created",
                Data = createdAccountRole
            });
        }


        [HttpPut]
        public IActionResult Update(UpdateAccountRoleDto updateAccountRoleDto)
        {
            var isUpdated = _service.UpdateAccountRole(updateAccountRoleDto);

            if (isUpdated is -1)
            {
                return NotFound(new ResponseHandler<UpdateAccountRoleDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Failed to Update!"
                });
            }
            if (isUpdated is 0)
            {
                return BadRequest(new ResponseHandler<UpdateAccountRoleDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Check Your Data"
                });
            }

            return Ok(new ResponseHandler<GetAccountRoleDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.Accepted.ToString(),
                Message = "Data Accepted"
            });
        }


        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var isDeleted = _service.DeleteAccountRole(guid);

            if (isDeleted is -1)
            {
                return NotFound(new ResponseHandler<GetAccountRoleDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid Not Found!"
                });
            }
            if (isDeleted is 0)
            {
                return BadRequest(new ResponseHandler<UpdateAccountRoleDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Check Connection to Database"
                });
            }

            return Ok(new ResponseHandler<UpdateAccountRoleDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.Accepted.ToString(),
                Message = "Successfully Deleted"
            });
        }
    }
}
