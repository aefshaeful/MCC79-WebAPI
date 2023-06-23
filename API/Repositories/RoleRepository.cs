using API.Models;
using API.Contracts;
using API.Data;


namespace API.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly BookingDbContext context;
        public RoleRepository(BookingDbContext Context)
        {
            this.context = Context;
        }

        public ICollection<Role> GetAll()
        {
            return context.Set<Role>().ToList();
        }

        public Role GetByGuid(Guid guid)
        {
            return context.Set<Role>().Find(guid);
        }

        public Role Create(Role role)
        {
            try
            {
                context.Set<Role>().Add(role);
                context.SaveChanges();
                return role;
            }
            catch (Exception ex)
            {
                return new Role();
            }
        }

        public bool Update(Role role)
        {
            try
            {
                context.Set<Role>().Update(role);
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
                var role = GetByGuid(guid);

                if (role is null)
                {
                    return false;
                }
                context.Set<Role>().Remove(role);
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
