using API.Contracts;
using API.Models;
using API.DTOs.Universities;

namespace API.Services
{
    public class UniversityServices
    {
        private readonly IUniversityRepository _universityRepository;

        public UniversityServices(IUniversityRepository universityRepository)
        {
            _universityRepository = universityRepository;
        }

        public IEnumrable<GetUniversityDto>? GetUniversity()
        {
            var universities = _universityRepository.GetAll();
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
        }

        public IEnumerable<GetUniversityDto>? GetUniversity(string name)
        {
            var universities = _universityRepository.GetByName(name);
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
        }

        public GetUniversityDto? GetUniversity(Guid guid)
        {
            var university = _universityRepository.GetByGuid(guid);

            if (university is null)
            {
                return null;
            }

            var toDto = new GetUniversityDto 
            {
                var university = new University
            }
        }
    }
}
