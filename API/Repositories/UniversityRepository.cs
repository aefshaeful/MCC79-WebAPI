using API.Data;
using API.Contracts;
using API.Models;
using System.Linq.Expressions;

namespace API.Repositories
{
    public class UniversityRepository : GeneralRepository<University>, IUniversityRepository
    {
        public UniversityRepository(BookingDbContext Context) : base(Context) 
        { 
        }
        public IEnumerable<University> GetByName(string name)   // Method untuk mendapatkan universitas berdasarkan nama.
        {
            return context.Set<University>().Where(university => university.Name.Contains(name));   // Melakukan query pada database.
        }
    }
}
