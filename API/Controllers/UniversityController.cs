using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Contracts;


namespace API.Controllers
{
    [ApiController]
    [Route("api/universities")]
    public class UniversityController : ControllerBase
    {
        private readonly IUniversityRepository repository;
        public UniversityController(IUniversityRepository Repository)
        {
            repository = Repository;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var universities = repository.GetAll();
            
            if (!universities.Any()) 
            {
                return NotFound();
            }

            return Ok(universities);
        }


        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid) 
        {
            var university = repository.GetByGuid(guid);
            if (university is null) 
            {
                return NotFound(); 
            }

            return Ok(university);
        }


        [HttpPost]
        public IActionResult Create(University university) 
        {
            var createdUniversity = repository.Create(university);
            return Ok(createdUniversity);
        }


        [HttpPut]
        public IActionResult Update(University university)
        {
            var isUpdated = repository.Update(university);
            if (!isUpdated)
            {
                return NotFound();
            }

            return Ok();
        }


        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var isDeleted = repository.Delete(guid);
            if(!isDeleted)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
