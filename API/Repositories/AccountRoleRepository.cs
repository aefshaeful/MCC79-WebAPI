using API.Models;
using API.Contracts;
using API.Data;


namespace API.Repositories
{
    public class AccountRoleRepository : IAccountRoleRepository
    {
        private readonly BookingDbContext context;
        public AccountRoleRepository(BookingDbContext Context)
        {
            this.context = Context;
        }

        public ICollection<AccountRole> GetAll()
        {
            return context.Set<AccountRole>().ToList();
        }

        public AccountRole GetByGuid(Guid guid)
        {
            return context.Set<AccountRole>().Find(guid);
        }

        public AccountRole Create(AccountRole accountrole)
        {
            try
            {
                context.Set<AccountRole>().Add(accountrole);
                context.SaveChanges();
                return accountrole;
            }
            catch (Exception ex)
            {
                return new AccountRole();
            }
        }

        public bool Update(AccountRole accountrole)
        {
            try
            {
                context.Set<AccountRole>().Update(accountrole);
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
                var accountrole = GetByGuid(guid);

                if (accountrole is null)
                {
                    return false;
                }
                context.Set<AccountRole>().Remove(accountrole);
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
