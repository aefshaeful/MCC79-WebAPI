using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/booking")]

    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository repository;
        public BookingController(IBookingRepository Repository)
        {
            this.repository = Repository;
        }

        [HttpGet]
        public IActionResult GettAll()
        {
            var booking = repository.GetAll();

            if (!booking.Any())
            {
                return NotFound();
            }

            return Ok(booking);
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var booking = repository.GetByGuid(guid);

            if (booking is null)
            {
                return NotFound();
            }

            return Ok(booking);
        }

        [HttpPost]
        public IActionResult Create(Booking booking) 
        {
            var createdBooking = repository.Create(booking);
            return Ok(createdBooking);
        }

        [HttpPut]
        public IActionResult Update(Booking booking) 
        {
            var isUpdate = repository.Update(booking);
           
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
