using API.Models;
using API.Contracts;
using Microsoft.AspNetCore.Mvc;
using API.Repositories;

namespace API.Controllers
{
    [ApiController]
    [Route("api/account")]

    public class AccountController : GeneralController<IAccountRepository, Account>
    {
        /*public AccountController(IAccountRepository Repository) : base(Repository) { }*/
    }
}
