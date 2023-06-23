using API.Models;
using API.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/account")]

    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository repository;
        public AccountController(IAccountRepository Repository)
        {
            this.repository = Repository;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var account = repository.GetAll();

            if (!account.Any())
            {
                return NotFound();
            }

            return Ok(account);
        }


        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var account = repository.GetByGuid(guid);

            if (account is null)
            {
                return NotFound();
            }

            return Ok(account);
        }


        [HttpPost]
        public IActionResult Create(Account account)
        {
            var createdAccount = repository.Create(account);
            return Ok(createdAccount);
        }


        [HttpPut]
        public IActionResult Update(Account account)
        {
            var isUpdate = repository.Update(account);

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

            if (!isDelete)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
