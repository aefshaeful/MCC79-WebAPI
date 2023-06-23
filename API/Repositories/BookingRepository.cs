using API.Data;
using API.Contracts;
using API.Models;

namespace API.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly BookingDbContext context;
        public BookingRepository (BookingDbContext Context) 
        {
            this.context = Context;
        }

        public ICollection<Booking> GetAll() 
        {
            return context.Set<Booking>().ToList();
        }

        public Booking GetByGuid(Guid guid) 
        {
            return context.Set<Booking>().Find(guid);
        }

        public Booking Create(Booking booking) 
        {
            try
            {
                context.Set<Booking>().Add(booking);
                context.SaveChanges();
                return booking;
            }
            catch (Exception ex) 
            {
                return new Booking();
            }
        }

        public bool Update(Booking booking) 
        {
            try
            {
                context.Set<Booking>().Update(booking);
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
                var booking = GetByGuid(guid);
                
                if (booking is null) 
                {
                    return false;
                }
                context.Set<Booking>().Remove(booking);
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
