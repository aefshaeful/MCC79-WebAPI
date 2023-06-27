﻿using API.Contracts;
using API.DTOs.Role;
using API.DTOs.Room;
using API.Models;

namespace API.Services
{
    public class RoomService
    {
        private readonly IRoomRepository _roomRepository;
        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public IEnumerable<GetRoomDto>? GetRoom()
        {
            var rooms = _roomRepository.GetAll();
            if (!rooms.Any())
            {
                return null;
            }

            var toDto = rooms.Select(room => new GetRoomDto
            {
                Guid = room.Guid,
                Name = room.Name
,               Floor = room.Floor,
                Capacity = room.Capacity
            }).ToList();

            return toDto;
        }


        public GetRoomDto? GetRoom(Guid guid)
        {
            var room = _roomRepository.GetByGuid(guid);

            if (room is null)
            {
                return null;
            }

            var toDto = new GetRoomDto
            {
                Guid = room.Guid,
                Name = room.Name,
                Floor = room.Floor,
                Capacity = room.Capacity
            };

            return toDto;
        }


        public GetRoomDto? CreateRoom(NewRoomDto newRoomDto)
        {
            var room = new Room
            {
                Guid = new Guid(),
                Name = newRoomDto.Name,
                Floor = newRoomDto.Floor,
                Capacity = newRoomDto.Capacity,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };

            var createdRoom = _roomRepository.Create(room);
            if (createdRoom is null)
            {
                return null;
            }

            var toDto = new GetRoomDto
            {
                Guid = room.Guid,
                Name = room.Name,
                Floor = room.Floor,
                Capacity = room.Capacity
            };

            return toDto;
        }


        public int UpdateRoom(UpdateRoomDto updateRoomDto)
        {
            var isExist = _roomRepository.IsExist(updateRoomDto.Guid);
            if (!isExist)
            {
                return -1;
            }

            var getRoom = _roomRepository.GetByGuid(updateRoomDto.Guid);

            var room = new Room
            {
                Guid = updateRoomDto.Guid,
                Name = updateRoomDto.Name,
                Floor = updateRoomDto.Floor,
                Capacity  = updateRoomDto.Capacity,
                ModifiedDate = DateTime.Now,
                CreatedDate = getRoom!.CreatedDate
            };

            var isUpdate = _roomRepository.Update(room);
            if (!isUpdate)
            {
                return 0;
            }

            return 1;
        }


        public int DeleteRoom(Guid guid)
        {
            var isExist = _roomRepository.IsExist(guid);
            if (!isExist)
            {
                return -1;
            }

            var room = _roomRepository.GetByGuid(guid);
            var isDelete = _roomRepository.Delete(room!);
            if (!isDelete)
            {
                return 0;
            }

            return 1;
        }
    }
}
