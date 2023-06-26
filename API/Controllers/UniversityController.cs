using Microsoft.AspNetCore.Mvc;
using API.Utilities.Enums;
using Microsoft.AspNetCore.Http.Headers;
using API.Models;
using API.Contracts;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("api/universities")]
    public class UniversityController : ControllerBase
    {
        public UniversityController(IUniversityRepository Repository) : base(Repository) { }

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



        [HttpGet("get-by-name/{name}")]
        public IActionResult GetByName(string name)
        {
            /* var university = repository.GetByName(name);
             if (university is null)
             {
                 return NotFound();
             }

             return Ok(university);*/

            var university = repository.GetByName(name);
            if (!university.Any())
            {
                return NotFound(new ResponseHandler<University>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Not Universities Found With The Given Name!"
                });
            }

            return Ok(new ResponseHandler<IEnumerable<University>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Universities Found",
                Data = university
            });
        }
    }
}
