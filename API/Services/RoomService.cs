using API.Contracts;
using API.DTOs.Role;
using API.DTOs.Room;
using API.Models;
using API.Repositories;
using API.Utilities.Enums;

namespace API.Services
{
    public class RoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IBookingRepository _bookingRepository;
        public RoomService(IRoomRepository roomRepository, IBookingRepository bookingRepository)
        {
            _roomRepository = roomRepository;
            _bookingRepository = bookingRepository;
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


        public IEnumerable<UnusedRoomTodayDto> GetUnusedRoom()
        {
            var rooms = _roomRepository.GetAll();
            var usedRooms = from room in rooms
                            join booking in _bookingRepository.GetAll()
                            on room.Guid equals booking.Guid
                            where booking.Status == StatusLevel.OnGoing
                            select new UnusedRoomTodayDto
                            {
                                RoomGuid = room.Guid,
                                RoomName = room.Name,
                                Floor = room.Floor,
                                Capacity = room.Capacity,
                            };


            List<Room> tmpRooms = new List<Room>(rooms);
            foreach (var room in rooms)
            {
                foreach (var usedRoom in usedRooms)
                {
                    if (room.Guid == usedRoom.RoomGuid)
                    {
                        tmpRooms.Remove(room);
                        break;
                    }
                }
            }

            var unusedRooms = from room in tmpRooms
                              select new UnusedRoomTodayDto
                              {
                                  RoomGuid = room.Guid,
                                  RoomName = room.Name,
                                  Floor = room.Floor,
                                  Capacity = room.Capacity,
                              };

            return unusedRooms;
        }
    }

}
