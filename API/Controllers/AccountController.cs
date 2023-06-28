using API.Models;
using API.Contracts;
using Microsoft.AspNetCore.Mvc;
using API.Repositories;
using API.DTOs.Account;
using API.Services;
using API.Utilities.Enums;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("api/account")]

    public class AccountController : ControllerBase
    {
        private readonly AccountService _service;
        public AccountController(AccountService service)
        {
            _service = service;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var accounts = _service.GetAccount();

            if (accounts == null)
            {
                return NotFound(new ResponseHandler<GetAccountDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found!"
                });
            }
            return Ok(new ResponseHandler<IEnumerable<GetAccountDto>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data Found",
                Data = accounts
            });
        }


        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var account = _service.GetAccount(guid);

            if (account is null)
            {
                return NotFound(new ResponseHandler<GetAccountDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid Not Found!"
                });
            }
            return Ok(new ResponseHandler<GetAccountDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Guid Found",
                Data = account
            });
        }


        [HttpPost]
        public IActionResult Create(NewAccountDto newAccountDto)
        {
            var createdAccount = _service.CreateAccount(newAccountDto);

            if (createdAccount is null)
            {
                return NotFound(new ResponseHandler<GetAccountDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Failed to Create!"
                });
            }
            return Ok(new ResponseHandler<GetAccountDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.Accepted.ToString(),
                Message = "Successfully Created",
                Data = createdAccount
            });
        }


        [Route("register")]
        [HttpPost]
        public IActionResult RegisterAccount(GetRegisterDto getRegisterDto)
        {
            var createdRegister = _service.RegisterAccount(getRegisterDto);
            if (createdRegister == null)
            {
                return BadRequest(new ResponseHandler<GetRegisterDto>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Register failed"
                });
            }

            return Ok(new ResponseHandler<GetRegisterDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Successfully register",
                Data = createdRegister
            });
        }


        [HttpPut]
        public IActionResult Update(UpdateAccountDto updateAccountDto)
        {
            var isUpdated = _service.UpdateAccount(updateAccountDto);

            if (isUpdated is -1)
            {
                return NotFound(new ResponseHandler<UpdateAccountDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Failed to Update!"
                });
            }
            if (isUpdated is 0)
            {
                return BadRequest(new ResponseHandler<UpdateAccountDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Check Your Data"
                });
            }

            return Ok(new ResponseHandler<GetAccountDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.Accepted.ToString(),
                Message = "Data Accepted"
            });
        }


        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var isDeleted = _service.DeleteAccount(guid);

            if (isDeleted is -1)
            {
                return NotFound(new ResponseHandler<GetAccountDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid Not Found!"
                });
            }
            if (isDeleted is 0)
            {
                return BadRequest(new ResponseHandler<UpdateAccountDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Check Connection to Database"
                });
            }

            return Ok(new ResponseHandler<UpdateAccountDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.Accepted.ToString(),
                Message = "Successfully Deleted"
            });
        }
    }
}
