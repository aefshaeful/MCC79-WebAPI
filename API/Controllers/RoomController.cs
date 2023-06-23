using API.Models;
using API.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/room")]

    public class RoomController : ControllerBase
    {
        private readonly IRoomRepository repository;
        public RoomController(IRoomRepository Repository)
        {
            this.repository = Repository;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var room = repository.GetAll();

            if(!room.Any()) 
            {
                return NotFound();
            }

            return Ok(room);
        }


        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid) 
        { 
            var room = repository.GetByGuid(guid);

            if(room is null)
            {
                return NotFound();
            }

            return Ok(room);
        }


        [HttpPost]
        public IActionResult Create(Room room) 
        {
            var createdRoom = repository.Create(room);
            return Ok(createdRoom);
        }


        [HttpPut]
        public IActionResult Update(Room room) 
        {
            var isUpdate = repository.Update(room);

            if(!isUpdate) 
            {
                return NotFound();
            }

            return Ok();
        }


        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var isDelete = repository.Delete(guid);

            if(!isDelete)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
