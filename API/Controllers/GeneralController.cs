using Microsoft.AspNetCore.Mvc;
using API.Contracts;
using API.Utilities.Enums;
using System.Net;

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
                return NotFound(new ResponseHandler<TEntity>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found!"
                });
            }
            return Ok(new ResponseHandler<IEnumerable<TEntity>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data Found",
                Data = entity
            });

            /*if (!entity.Any())
            {
                return NotFound();
            }

            return Ok(entity);*/
        }


        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var entity = repository.GetByGuid(guid);
            
            if (entity is null)
            {
                return NotFound(new ResponseHandler<TEntity>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid Not Found!"
                });
            }
            return Ok(new ResponseHandler<TEntity>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Guid Found",
                Data = entity
            });


            /*if (entity is null)
            {
                return NotFound();
            }

            return Ok(entity);*/
        }


        [HttpPost]
        public IActionResult Create(TEntity entity)
        {
            var createdEntity = repository.Create(entity);

            if (createdEntity is false)
            {
                return NotFound(new ResponseHandler<TEntity>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found!"
                });
            }
            return Ok(new ResponseHandler<IEnumerable<TEntity>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.Accepted.ToString(),
                Message = "Data Accepted"
            });

            /* return Ok(createdEntity);*/
        }


        [HttpPut]
        public IActionResult Update(TEntity entity)
        {
            var isUpdated = repository.Update(entity);

            if (!isUpdated)
            {
                return NotFound(new ResponseHandler<TEntity>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found!"
                });
            }
            return Ok(new ResponseHandler<TEntity>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.Accepted.ToString(),
                Message = "Data Accepted"
            });


            /*if (!isUpdated)
            {
                return NotFound();
            }

            return Ok();*/
        }


        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var isDeleted = repository.Delete(guid);
            if (!isDeleted)
            {
                return NotFound(new ResponseHandler<TEntity>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found!"
                });
            }
            return Ok(new ResponseHandler<TEntity>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.Accepted.ToString(),
                Message = "Data Accepted"
            });


           /* if (!isDeleted)
            {
                return NotFound();
            }

            return Ok();*/
        }
    }
}
