using API.Models;
using API.Contracts;
using API.Data;

namespace API.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly BookingDbContext context;
        public RoomRepository(BookingDbContext Context) 
        {
            this.context = Context;
        }

        public ICollection<Room> GetAll() 
        {
            return context.Set<Room>().ToList();
        }

        public Room GetByGuid(Guid guid) 
        {
            return context.Set<Room>().Find(guid);
        }

        public Room Create(Room room) 
        {
            try
            {
                context.Set<Room>().Add(room);
                context.SaveChanges();
                return room;
            }
            catch (Exception ex) 
            {
                return new Room();
            }
        }

        public bool Update(Room room) 
        {
            try
            {
                context.Set<Room>().Update(room);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex) 
            {
                return false;
            }
        }

        public bool Delete(Guid guid)
        {
            try
            {
                var room = GetByGuid(guid);

                if(room is null)
                {
                    return false;
                }
                context.Set<Room>().Remove(room);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex) 
            {
                return false;
            }
        }
    }
}
