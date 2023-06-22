using API.Data;
//using API.Contracts;
using API.Models;
using System.Linq.Expressions;

namespace API.Repositories
{
    public class UniversityRepository
    {
        private readonly BookingDbContext context;
        public UniversityRepository(BookingDbContext Context)
        {
            this.context = Context;
        }


        public ICollection<University> GetAll()
        {
            return context.Set<University>().ToList();
        }


        public University? GetByGuid(Guid guid) 
        {
            return context.Set<University>().Find(guid);
        }


        public University Create(University university) 
        {
            try 
            {
                context.Set<University>().Add(university);
                context.SaveChanges();
                return university;
            }
            catch (Exception ex) 
            {
                return new University();
            }
        }


        public bool Update(University university) 
        {
            try
            {
                context.Set<University>().Update(university);
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
                var university = GetByGuid(guid);
                if (university is null)
                {
                    return false;
                }
                context.Set<University>().Remove(university);
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
