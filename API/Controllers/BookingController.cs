using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/booking")]

    public class BookingController : GeneralController<IBookingRepository, Booking>
    {
       /* public BookingController(IBookingRepository Repository) : base(Repository) { }*/
    }
}
