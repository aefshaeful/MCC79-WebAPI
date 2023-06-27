using API.Utilities.Enums;


namespace API.DTOs.Booking
{
    public class NewBookingDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Remarks { get; set; }
        public StatusLevel Status { get; set; }
        public Guid RoomGuid { get; set; }
        public Guid EmployeeGuid { get; set; }
    }
}
