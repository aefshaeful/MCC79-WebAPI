using API.Contracts;

namespace API.Services
{
    public class UniversityServices
    {
       /* private readonly IUniversityRepository _universityrepository;

        public UniversityServices(IUniversityRepository universityrepository)
        {
            _universityrepository = universityrepository;
        }

        public IEnumrable<GetUniversityDto>? GetUniversity()
        {
            var universities = _universityrepository.GetAll();
            if (!universities.Any()) 
            {
                return null;
            }

            var toDto = universities.Select(university => new GetUniversityDto 
            { 
                Guid = university.Guid,
                Code = university.Code,
                Name = university.Name
            }).ToList();

            return toDto;
        }*/
    }
}
