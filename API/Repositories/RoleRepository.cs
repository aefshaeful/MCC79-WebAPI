using API.Models;
using API.Contracts;
using API.Data;
using Microsoft.EntityFrameworkCore;


namespace API.Repositories
{
    public class RoleRepository : GeneralRepository<Role>, IRoleRepository
    {
        public RoleRepository(BookingDbContext Context) : base(Context) { }
        public Role? GetByName(string name)
        {
            return context.Set<Role>().FirstOrDefault(role => role.Name == name);
        }
    }
}
