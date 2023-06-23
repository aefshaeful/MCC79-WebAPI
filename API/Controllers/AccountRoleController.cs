using API.Models;
using API.Contracts;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{
    [ApiController]
    [Route("api/accountrole")]
    public class AccountRoleController : ControllerBase
    {

        private readonly IAccountRoleRepository repository;
        public AccountRoleController(IAccountRoleRepository Repository)
        {
            this.repository = Repository;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var accountrole = repository.GetAll();

            if (!accountrole.Any())
            {
                return NotFound();
            }

            return Ok(accountrole);
        }


        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var accountrole = repository.GetByGuid(guid);

            if (accountrole is null)
            {
                return NotFound();
            }

            return Ok(accountrole);
        }


        [HttpPost]
        public IActionResult Create(AccountRole accountrole)
        {
            var createdAccountRole = repository.Create(accountrole);
            return Ok(createdAccountRole);
        }


        [HttpPut]
        public IActionResult Update(AccountRole accountrole)
        {
            var isUpdate = repository.Update(accountrole);

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
