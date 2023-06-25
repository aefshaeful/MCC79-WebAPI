using API.Models;
using API.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/account")]

    public class AccountController : GeneralController<Account>
    {
        public AccountController(IAccountRepository Repository) : base(Repository) { }
    }
}
