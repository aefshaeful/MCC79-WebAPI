using API.Models;
using API.Contracts;
using API.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class AccountRepository : GeneralRepository<Account>, IAccountRepository
    {
        public AccountRepository(BookingDbContext Context) : base(Context) { }

        public Account? CheckOtp(string email, int otp)
        {
            return context.Set<Account>().Join(context.Set<Employee>(),
                a => a.Guid, e => e.Guid, (a, e) =>
                new { Account = a, Employee = e }).FirstOrDefault(e => e.Employee.Email == email && e.Account.Otp == otp)?.Account;

            /*var employee = _context.Set<Employee>().FirstOrDefault(e => e.Email == email);

            if (employee == null)
            {
                return null;
            }

            return _context.Set<Account>().FirstOrDefault(a => a.Guid == employee.Guid && a.Otp == otp);*/
        }
    }
}
