using API.Contracts;
using API.Data;
using API.Models;


namespace API.Repositories
{
    public class EducationRepository : IEducationRepository
    {
        private readonly BookingDbContext context;
        public EducationRepository (BookingDbContext Context)
        {
            this.context = Context;
        }


        public ICollection<Education> GetAll()
        {
            return context.Set<Education>().ToList();
        }


        public Education? GetByGuid(Guid guid) 
        {
            return context.Set<Education>().Find(guid);
        }


        public Education Create(Education education)
        {
            try
            {
                context.Set<Education>().Add(education);
                context.SaveChanges();
                return education;
            }
            catch (Exception ex) 
            {
                return new Education();
            }
        }


        public bool Update(Education education)
        {
            try
            {
                context.Set<Education>().Update(education);
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
                var education = GetByGuid(guid);
                if(education is null)
                {
                    return false;
                }
                context.Set<Education>().Remove(education);
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
