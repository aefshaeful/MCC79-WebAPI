using API.Contracts;
using API.DTOs.Booking;
using API.Models;
using API.Services;
using API.Utilities.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("api/booking")]

    public class BookingController : ControllerBase
    {
        private readonly BookingService _service;
        public BookingController(BookingService service)
        {
            _service = service;
        }


        [HttpGet("booking-detail")]
        public IActionResult GetBookingDetail()
        {
            var bookingDetails = _service.BookingsDetail();

            if (bookingDetails == null)
            {
                return NotFound(new ResponseHandler<BookingDetailDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            return Ok(new ResponseHandler<IEnumerable<BookingDetailDto>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data Found",
                Data = bookingDetails
            });
        }


        [HttpGet("getbooking-detail/{guid}")]
        public IActionResult GetBookingDetailByGuid(Guid guid)
        {
            var booking = _service.BookingDetailByGuid(guid);

            if (booking is null)
            {
                return NotFound(new ResponseHandler<BookingDetailDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid Not Found!"
                });
            }
            return Ok(new ResponseHandler<BookingDetailDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Guid Found",
                Data = booking
            });
        }




        [HttpGet]
        public IActionResult GetAll()
        {
            var bookings = _service.GetBooking();

            if (bookings == null)
            {
                return NotFound(new ResponseHandler<GetBookingDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            return Ok(new ResponseHandler<IEnumerable<GetBookingDto>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data Found",
                Data = bookings
            });
        }


        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var booking = _service.GetBooking(guid);

            if (booking is null)
            {
                return NotFound(new ResponseHandler<GetBookingDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid Not Found!"
                });
            }
            return Ok(new ResponseHandler<GetBookingDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Guid Found",
                Data = booking
            });
        }


        [HttpGet("rooms-in-use-today")]
        public IActionResult GetDetail()
        {
            var bookingDetail = _service.GetRoomsInUseToday();

            if (bookingDetail == null)
            {
                return NotFound(new ResponseHandler<GetBookedByDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found!"
                });
            }

            return Ok(new ResponseHandler<IEnumerable<GetBookedByDto>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data Found",
                Data = bookingDetail
            });
        }


        [HttpGet("booking-duration")]
        public IActionResult CalculateBookingLenght()
        {
            var bookingDuration = _service.BookingDuration();

            if (bookingDuration == null)
            {
                return NotFound(new ResponseHandler<BookingLengthDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data not found"
                });
            }

            return Ok(new ResponseHandler<IEnumerable<BookingLengthDto>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data found",
                Data = bookingDuration
            });
        }


        [HttpPost]
        public IActionResult Create(NewBookingDto newBookingDto)
        {
            var createdBooking = _service.CreateBooking(newBookingDto);

            if (createdBooking is null)
            {
                return NotFound(new ResponseHandler<GetBookingDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Failed to Create!"
                });
            }
            return Ok(new ResponseHandler<GetBookingDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.Accepted.ToString(),
                Message = "Successfully Created",
                Data = createdBooking
            });
        }


        [HttpPut]
        public IActionResult Update(UpdateBookingDto updateBookingDto)
        {
            var isUpdated = _service.UpdateBooking(updateBookingDto);

            if (isUpdated is -1)
            {
                return NotFound(new ResponseHandler<UpdateBookingDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Failed to Update!"
                });
            }
            if (isUpdated is 0)
            {
                return BadRequest(new ResponseHandler<UpdateBookingDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Check Your Data"
                });
            }

            return Ok(new ResponseHandler<GetBookingDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.Accepted.ToString(),
                Message = "Data Accepted"
            });
        }


        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var isDeleted = _service.DeleteBooking(guid);

            if (isDeleted is -1)
            {
                return NotFound(new ResponseHandler<GetBookingDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid Not Found!"
                });
            }
            if (isDeleted is 0)
            {
                return BadRequest(new ResponseHandler<UpdateBookingDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Check Connection to Database"
                });
            }

            return Ok(new ResponseHandler<UpdateBookingDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.Accepted.ToString(),
                Message = "Successfully Deleted"
            });
        }
    }
}
