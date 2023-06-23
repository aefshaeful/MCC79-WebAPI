using API.Models;
using API.Contracts;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{

    [ApiController]
    [Route("api/role")]

    public class RoleController : ControllerBase
    {

        private readonly IRoleRepository repository;
        public RoleController(IRoleRepository Repository)
        {
            this.repository = Repository;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var role = repository.GetAll();

            if (!role.Any())
            {
                return NotFound();
            }

            return Ok(role);
        }


        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var role = repository.GetByGuid(guid);

            if (role is null)
            {
                return NotFound();
            }

            return Ok(role);
        }


        [HttpPost]
        public IActionResult Create(Role role)
        {
            var createdRole = repository.Create(role);
            return Ok(createdRole);
        }


        [HttpPut]
        public IActionResult Update(Role role)
        {
            var isUpdate = repository.Update(role);

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
