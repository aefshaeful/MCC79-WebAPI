using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Room
{
    public class UnusedRoomTodayDto
    {
        public Guid RoomGuid { get; set; }
        public string RoomName { get; set; }
        public int Floor { get; set; }
        public int Capacity { get; set; }
    }
}
