namespace API.DTOs.Booking
{
    public class BookingLengthDto
    {
        public Guid RoomGuid { get; set; }
        public String RoomName { get; set; }
        public TimeSpan BookingLenght { get; set; }
    }
}
