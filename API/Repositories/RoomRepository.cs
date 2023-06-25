using API.Models;
using API.Contracts;
using API.Data;

namespace API.Repositories
{
    public class RoomRepository : GeneralRepository<Room>, IRoomRepository
    {
        public RoomRepository(BookingDbContext Context) : base(Context) { }
    }
}
