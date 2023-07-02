using API.Models;
using API.Contracts;
using API.Data;
using Microsoft.EntityFrameworkCore;


namespace API.Repositories
{
    public class AccountRoleRepository : GeneralRepository<AccountRole>, IAccountRoleRepository
    {
        public AccountRoleRepository(BookingDbContext Context) : base(Context) { }

        public IEnumerable<AccountRole> GetAccountRoleByAccountGuid(Guid guid)
        {
            return context.Set<AccountRole>().Where(accountRole => accountRole.AccountGuid == guid);
        }
    }
}
