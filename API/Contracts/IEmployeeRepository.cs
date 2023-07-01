using API.Models;


namespace API.Contracts
{
    public interface IEmployeeRepository : IGeneralRepository<Employee>
    {
        Employee? CheckEmail(string email);
        Employee? GetEmail(string email);
    }
}
