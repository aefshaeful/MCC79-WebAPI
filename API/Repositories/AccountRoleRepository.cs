using API.Models;
using API.Contracts;
using API.Data;


namespace API.Repositories
{
    public class AccountRoleRepository : GeneralRepository<AccountRole>, IAccountRoleRepository
    {
        public AccountRoleRepository(BookingDbContext Context) : base(Context) { }
    }
}
