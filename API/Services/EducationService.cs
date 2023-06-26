﻿using API.Contracts;
using API.DTOs.Education;
using API.DTOs.Universities;
using API.Models;


namespace API.Services
{
    public class EducationService
    {
        private readonly IEducationRepository _educationRepository;
        public EducationService(IEducationRepository educationRepository)
        {
            _educationRepository = educationRepository;
        }

        public IEnumerable<GetEducationDto>? GetEducation()
        {
            var educations = _educationRepository.GetAll();
            if (!educations.Any())
            {
                return null;
            }

            var toDto = educations.Select(education => new GetEducationDto
            {
                Guid = education.Guid,
                Major = education.Major,
                Degree = education.Degree,
                Gpa = education.Gpa,
                UniversityGuid = education.UniversityGuid
            }).ToList();

            return toDto;
        }


        public GetEducationDto? GetEducation(Guid guid)
        {
            var education = _educationRepository.GetByGuid(guid);

            if (education is null)
            {
                return null;
            }

            var toDto = new GetEducationDto
            {
                Guid = education.Guid,
                Major = education.Major,
                Degree = education.Degree,
                Gpa = education.Gpa,
                UniversityGuid = education.UniversityGuid
            };

            return toDto;
        }


        public GetEducationDto? CreateEducation(NewEducationDto newEducationDto)
        {
            var education = new Education
            {
                Guid = new Guid(),
                Major = newEducationDto.Major,
                Degree = newEducationDto.Degree,
                Gpa = newEducationDto.Gpa,
                UniversityGuid = newEducationDto.UniversityGuid,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };

            var createdEducation = _educationRepository.Create(education);
            if (createdEducation is null)
            {
                return null;
            }

            var toDto = new GetEducationDto
            {
                Guid = education.Guid,
                Major = education.Major,
                Degree = education.Degree,
                Gpa = education.Gpa,
                UniversityGuid = education.UniversityGuid,
            };

            return toDto;
        }


        public int UpdateEducation(UpdateEducationDto updateEducationDto)
        {
            var isExist = _educationRepository.IsExist(updateEducationDto.Guid);
            if (!isExist)
            {
                return -1;
            }

            var getEducation = _educationRepository.GetByGuid(updateEducationDto.Guid);

            var education = new Education
            {
                Guid =updateEducationDto.Guid,
                Major = updateEducationDto.Major,
                Degree = updateEducationDto.Degree,
                Gpa = updateEducationDto.Gpa,
                UniversityGuid = updateEducationDto.UniversityGuid,
                ModifiedDate = DateTime.Now,
                CreatedDate = getEducation!.CreatedDate
            };

            var isUpdate = _educationRepository.Update(education);
            if (!isUpdate)
            {
                return 0;
            }

            return 1;
        }


        public int DeleteEducation(Guid guid)
        {
            var isExist = _educationRepository.IsExist(guid);
            if (!isExist)
            {
                return -1;
            }

            var education = _educationRepository.GetByGuid(guid);
            var isDelete = _educationRepository.Delete(education!);
            if (!isDelete)
            {
                return 0;
            }

            return 1;
        }
    }
}
