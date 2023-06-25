using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/education")]
    public class EducationController : GeneralController<Education>
    {
        public EducationController(IEducationRepository Repository) : base(Repository) { }
    }
}
