using API.Models;
using API.Contracts;
using API.Data;

namespace API.Repositories
{
    public class AccountRepository : GeneralRepository<Account>, IAccountRepository
    {
        public AccountRepository(BookingDbContext Context) : base(Context) { }
    }
}
