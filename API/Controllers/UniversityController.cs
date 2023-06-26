using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Contracts;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("api/universities")]
    public class UniversityController : GeneralController<IUniversityRepository, University> 
    {
        public UniversityController(IUniversityRepository Repository) : base(Repository) { }


        [HttpGet("get-by-name/{name}")]
        public IActionResult GetByName(string name)
        {
            var university = repository.GetByName(name);
            if (university is null)
            {
                return NotFound();
            }

            return Ok(university);

            /*var university = repository.GetByName(name);
            if (!university.Any())
            {
                return NotFound(new ResponsHandler<University> 
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "No Universities found with the given name"
                })
            }

            return Ok(new ResponsHandler<IEnumerable<University>)
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.
            }*/
        }
    }
}
