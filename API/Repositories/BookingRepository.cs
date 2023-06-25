using API.Data;
using API.Contracts;
using API.Models;

namespace API.Repositories
{
    public class BookingRepository : GeneralRepository<Booking>, IBookingRepository
    {
        public BookingRepository(BookingDbContext Context) : base(Context) { }
    }
}
