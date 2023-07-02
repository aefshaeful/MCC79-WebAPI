using API.Models;


namespace API.Contracts
{
    public interface IAccountRoleRepository : IGeneralRepository<AccountRole>
    {
        IEnumerable<AccountRole> GetAccountRoleByAccountGuid(Guid guid);
    }
}
