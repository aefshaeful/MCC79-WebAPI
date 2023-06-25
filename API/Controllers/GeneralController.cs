using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Contracts;


namespace API.Controllers
{
    public class GeneralController<TEntity> : ControllerBase where TEntity : class
    {
        private readonly IGeneralRepository<TEntity> repository;
        public GeneralController(IGeneralRepository<TEntity> Repository)
        {
            repository = Repository;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var entity = repository.GetAll();

            if (!entity.Any())
            {
                return NotFound();
            }

            return Ok(entity);
        }


        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var entity = repository.GetByGuid(guid);
            if (entity is null)
            {
                return NotFound();
            }

            return Ok(entity);
        }


        [HttpGet("{name}")]
        public IActionResult GetByName(string name)
        {
            var entity = repository.GetByName(name);
            if (entity is null)
            {
                return NotFound();
            }

            return Ok(entity);
        }

        [HttpPost]
        public IActionResult Create(TEntity entity)
        {
            var createdEntity = repository.Create(entity);
            return Ok(createdEntity);
        }


        [HttpPut]
        public IActionResult Update(TEntity entity)
        {
            var isUpdated = repository.Update(entity);
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
