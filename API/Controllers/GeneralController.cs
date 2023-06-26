using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Contracts;


namespace API.Controllers
{
    public class GeneralController<TIEntityRepository, TEntity> : ControllerBase 
        where TIEntityRepository : IGeneralRepository<TEntity>
        where TEntity : class
    {
        protected readonly TIEntityRepository repository;
        public GeneralController(TIEntityRepository Repository)
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
