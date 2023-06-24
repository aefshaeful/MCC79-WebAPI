using API.Models;
using API.Contracts;
using API.Data;

namespace API.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly BookingDbContext context;
        public AccountRepository(BookingDbContext Context)
        {
            this.context = Context;
        }

        public ICollection<Account> GetAll()
        {
            return context.Set<Account>().ToList();
        }

        public Account? GetByGuid(Guid guid)
        {
            return context.Set<Account>().Find(guid);
        }

        public Account Create(Account account)
        {
            try
            {
                context.Set<Account>().Add(account);
                context.SaveChanges();
                return account;
            }
            catch (Exception ex)
            {
                return new Account();
            }
        }

        public bool Update(Account account)
        {
            try
            {
                context.Set<Account>().Update(account);
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
                var account = GetByGuid(guid);

                if (account is null)
                {
                    return false;
                }
                context.Set<Account>().Remove(account);
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
