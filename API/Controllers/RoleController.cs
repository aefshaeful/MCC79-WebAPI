using API.Models;
using API.Contracts;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{

    [ApiController]
    [Route("api/role")]

    public class RoleController : GeneralController<Role>
    {
        public RoleController(IRoleRepository Repository) : base(Repository) { }
    }
}
