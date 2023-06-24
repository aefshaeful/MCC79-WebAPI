using API.Contracts;
using API.Data;
using API.Models;


namespace API.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly BookingDbContext context;
        public EmployeeRepository(BookingDbContext Context) 
        {
            this.context = Context;
        }

        public ICollection<Employee> GetAll() 
        { 
            return context.Set<Employee>().ToList();
        }

        public Employee? GetByGuid(Guid guid) 
        {
            return context.Set<Employee>().Find(guid);
        }

        public Employee Create(Employee employee) 
        {
            try
            {
                context.Set<Employee>().Add(employee);
                context.SaveChanges();
                return employee;
            }
            catch (Exception ex) 
            {
                return new Employee();
            }
        }

        public bool Update(Employee employee) 
        {
            try
            {
                context.Set<Employee>().Update(employee);
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
                var employee = GetByGuid(guid);

                if (employee is null)
                {
                    return false;
                }
                context.Set<Employee>().Remove(employee);
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
