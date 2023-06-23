using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/education")]
    public class EducationController : ControllerBase
    {

        private readonly IEducationRepository repository;
        public EducationController(IEducationRepository Repository)
        {
            repository = Repository;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var education = repository.GetAll();

            if (!education.Any())
            {
                return NotFound();
            }

            return Ok(education);
        }


        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var education = repository.GetByGuid(guid);

            if (education is null)
            {
                return NotFound();
            }

            return Ok(education);
        }


        [HttpPost]
        public IActionResult Create(Education education)
        {
            var createdEducation = repository.Create(education);
            return Ok(createdEducation);
        }


        [HttpPut]
        public IActionResult Update(Education education)
        {
            var isUpdated = repository.Update(education);

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

            if (!isDeleted)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
