﻿using API.Models;
using API.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/room")]

    public class RoomController : GeneralController<Room>
    {
        public RoomController(IRoomRepository Repository) : base(Repository) { }
    }
}
