using API.Contracts;
using API.DTOs.Room;
using API.Models;
using API.Services;
using API.Utilities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("api/Room")]
    [Authorize(Roles = $"{nameof(RoleLevel.Admin)}")]
    public class RoomController : ControllerBase
    {
        private readonly RoomService _service;
        public RoomController(RoomService service)
        {
            _service = service;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var Rooms = _service.GetRoom();

            if (Rooms == null)
            {
                return NotFound(new ResponseHandler<GetRoomDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found!"
                });
            }
            return Ok(new ResponseHandler<IEnumerable<GetRoomDto>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data Found",
                Data = Rooms
            });
        }


        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var Room = _service.GetRoom(guid);

            if (Room is null)
            {
                return NotFound(new ResponseHandler<GetRoomDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid Not Found!"
                });
            }

            return Ok(new ResponseHandler<GetRoomDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Guid Found",
                Data = Room
            });
        }


        [HttpPost]
        public IActionResult Create(NewRoomDto newRoomDto)
        {
            var createdRoom = _service.CreateRoom(newRoomDto);

            if (createdRoom is null)
            {
                return NotFound(new ResponseHandler<GetRoomDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Failed to Create!"
                });
            }
            return Ok(new ResponseHandler<GetRoomDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.Accepted.ToString(),
                Message = "Successfully Created",
                Data = createdRoom
            });
        }


        [HttpPut]
        public IActionResult Update(UpdateRoomDto updateRoomDto)
        {
            var isUpdated = _service.UpdateRoom(updateRoomDto);

            if (isUpdated is -1)
            {
                return NotFound(new ResponseHandler<UpdateRoomDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Failed to Update!"
                });
            }
            if (isUpdated is 0)
            {
                return BadRequest(new ResponseHandler<UpdateRoomDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Check Your Data"
                });
            }

            return Ok(new ResponseHandler<GetRoomDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.Accepted.ToString(),
                Message = "Data Accepted"
            });
        }


        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var isDeleted = _service.DeleteRoom(guid);

            if (isDeleted is -1)
            {
                return NotFound(new ResponseHandler<GetRoomDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid Not Found!"
                });
            }
            if (isDeleted is 0)
            {
                return BadRequest(new ResponseHandler<UpdateRoomDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Check Connection to Database"
                });
            }

            return Ok(new ResponseHandler<UpdateRoomDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.Accepted.ToString(),
                Message = "Successfully Deleted"
            });
        }


        [HttpGet("unused-roomstoday")]
        public IActionResult UnusedRoomToday()
        {
            var unUsedRooms = _service.GetUnusedRoom();

            if (unUsedRooms.Count() == 0)
            {
                return NotFound(new ResponseHandler<IEnumerable<UnusedRoomTodayDto>>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Rooms Full"
                });
            }

            return Ok(new ResponseHandler<IEnumerable<UnusedRoomTodayDto>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data Found",
                Data = unUsedRooms
            });
        }
    }
}
