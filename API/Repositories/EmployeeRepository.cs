using API.Contracts;
using API.Data;
using API.Models;



namespace API.Repositories
{
    public class EmployeeRepository : GeneralRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(BookingDbContext Context) : base(Context) { }

        public Employee? CheckEmail(string email)
        {
            return context.Set<Employee>().FirstOrDefault(e => e.Email == email);
            /*return context.Set<Employee>().Where(Employee => employee.Email.Contains(email));*/
        }

        public Employee? GetEmail(string email) 
        {
            return context.Set<Employee>().SingleOrDefault(e => e.Email == email);
        }
    }
}
