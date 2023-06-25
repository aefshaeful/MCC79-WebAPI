using System.Xml.Linq;

namespace API.Contracts
{
    public interface IGeneralRepository<TEntity>
    {
        ICollection<TEntity> GetAll();
        TEntity? GetByGuid(Guid guid);
        TEntity? GetByName(string name);
        TEntity? Create(TEntity entity);
        bool Update(TEntity entity);
        bool Delete(Guid guid);
    }
}
