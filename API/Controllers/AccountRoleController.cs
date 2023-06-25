using API.Models;
using API.Contracts;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{
    [ApiController]
    [Route("api/accountrole")]
    public class AccountRoleController : GeneralController<AccountRole>
    {
        public AccountRoleController(IAccountRoleRepository Repository) : base(Repository) { }
    }
}
