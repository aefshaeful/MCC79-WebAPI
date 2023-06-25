using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Contracts;


namespace API.Controllers
{
    [ApiController]
    [Route("api/universities")]
    public class UniversityController : GeneralController<University>
    {
        public UniversityController(IUniversityRepository Repository) : base(Repository) { }
    }
}
