using API.Contracts;
using Microsoft.AspNetCore.Mvc;
using API.Models;


namespace API.Controllers
{
    [ApiController]
    [Route("api/employee")]

    public class EmployeeController : GeneralController<Employee>
    {
        public EmployeeController(IEmployeeRepository Repository): base(Repository) { }
    }
}
