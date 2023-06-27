using API.Utilities.Enums;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Booking
{
    public class UpdateBookingDto
    {
        [Required]
        public Guid Guid { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public string Remarks { get; set; }
        [Required]
        [Range(0, 3, ErrorMessage = "0 to Request, 1 to Reject, 2 to UpComing, 3 to OnGoing")]
        public StatusLevel Status { get; set; }
        [Required]
        public Guid RoomGuid { get; set; }
        [Required]
        public Guid EmployeeGuid { get; set; }
    }
}
