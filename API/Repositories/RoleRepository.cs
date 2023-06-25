using API.Models;
using API.Contracts;
using API.Data;


namespace API.Repositories
{
    public class RoleRepository : GeneralRepository<Role>, IRoleRepository
    {
        public RoleRepository(BookingDbContext Context) : base(Context) { }
    }
}
