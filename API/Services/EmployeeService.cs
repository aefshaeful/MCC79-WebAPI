using API.Contracts;
using API.Data;
using API.DTOs.Employee;
using API.DTOs.Universities;
using API.Models;
using API.Repositories;
using API.Utilities.Enums;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUniversityRepository _universityRepository;
        private readonly IEducationRepository _educationRepository;

        public EmployeeService(IEmployeeRepository employeeRepository,
                IUniversityRepository universityRepository,
                IEducationRepository educationRepository)
        {
            _employeeRepository = employeeRepository;
            _universityRepository = universityRepository;
            _educationRepository = educationRepository;
        }


        public IEnumerable<EmployeeMasterDto>? GetAllMaster()
        {

            var master = (
                from employee in _employeeRepository.GetAll()
                join education in _educationRepository.GetAll() on employee.Guid equals education.Guid
                join university in _universityRepository.GetAll() on education.UniversityGuid equals university.Guid
                select new 
                {
                    EmployeeGuid = employee.Guid,
                    FullName = employee.FirstName + " " + employee.LastName,
                    Nik = employee.Nik,
                    BirthDate = employee.BirthDate,
                    Email = employee.Email,
                    Gender = employee.Gender,
                    HiringDate = employee.HiringDate,
                    PhoneNumber = employee.PhoneNumber,
                    Major = education.Major,
                    Degree = education.Degree,
                    Gpa = education.Gpa,
                    UniversityName = university.Name,
                }).ToList();

            if (!master.Any())
            {
                return null;
            }

            var toDto = master.Select(master => new EmployeeMasterDto
            {
                EmployeeGuid = master.EmployeeGuid,
                FullName = master.FullName,
                Nik = master.Nik,
                BirthDate = master.BirthDate,
                Email = master.Email,
                Gender = master.Gender,
                HiringDate = master.HiringDate,
                PhoneNumber = master.PhoneNumber,
                Major = master.Major,
                Degree = master.Degree,
                Gpa = master.Gpa,
                UniversityName = master.UniversityName,
            });

            return toDto;
        }


        public EmployeeMasterDto? GetMasterByGuid(Guid guid)
        {
            var master = GetAllMaster();

            var masterByGuid = master.FirstOrDefault(master => master.EmployeeGuid == guid);

            return masterByGuid;
        }


        public string GenerateNIK()
        {
            var employees = _employeeRepository.GetAll();
            if (employees == null)
            {
                return "11111";
            }

            var lastNik = employees.LastOrDefault();
            var nextNik = Convert.ToInt32(lastNik.Nik) + 1;    // var nextNik = int.Parse(lastNik.Nik) + 1;
            return nextNik.ToString();
        }


        public IEnumerable<GetEmployeeDto>? GetEmployee()
        {
            var employees = _employeeRepository.GetAll();
            if (!employees.Any())
            {
                return null;
            }

            var toDto = employees.Select(employee => new GetEmployeeDto
            {
                Guid = employee.Guid,
                Nik = employee.Nik,
                FirstName = employee.FirstName,
                LastName = employee.LastName ?? "",
                BirthDate = employee.BirthDate,
                Gender = employee.Gender,
                HiringDate = employee.HiringDate,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber 
            }).ToList();

            return toDto;
        }


        public GetEmployeeDto? GetEmployee(Guid guid)
        {
            var employee = _employeeRepository.GetByGuid(guid);

            if (employee is null)
            {
                return null;
            }

            var toDto = new GetEmployeeDto
            {
                Guid = employee.Guid,
                Nik = employee.Nik,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                BirthDate = employee.BirthDate,
                Gender = employee.Gender,
                HiringDate = employee.HiringDate,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber
            };

            return toDto;
        }


        public GetEmployeeDto? CreateEmployee(NewEmployeeDto newEmployeeDto)
        {
            var employee = new Employee
            {
                Guid = new Guid(),
                Nik = GenerateNIK(),
                FirstName = newEmployeeDto.FirstName,
                LastName = newEmployeeDto.LastName,
                BirthDate = newEmployeeDto.BirthDate,
                Gender = newEmployeeDto.Gender,
                HiringDate = newEmployeeDto.HiringDate,
                Email = newEmployeeDto.Email,
                PhoneNumber = newEmployeeDto.PhoneNumber,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };

            var createdEmployee = _employeeRepository.Create(employee);
            if (createdEmployee is null)
            {
                return null;
            }

            var toDto = new GetEmployeeDto
            {
                Guid = employee.Guid,
                Nik = employee.Nik,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                BirthDate = employee.BirthDate,
                Gender = employee.Gender,
                HiringDate = employee.HiringDate,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber
            };

            return toDto;
        }


        public int UpdateEmployee(UpdateEmployeeDto updateEmployeeDto)
        {
            var isExist = _employeeRepository.IsExist(updateEmployeeDto.Guid);
            if (!isExist)
            {
                return -1;
            }

            var getEmployee = _employeeRepository.GetByGuid(updateEmployeeDto.Guid);

            var employee = new Employee
            {
                Guid = updateEmployeeDto.Guid,
                Nik = GenerateNIK(),
                FirstName = updateEmployeeDto.FirstName,
                LastName = updateEmployeeDto.LastName,
                BirthDate = updateEmployeeDto.BirthDate,
                Gender = updateEmployeeDto.Gender,
                HiringDate = updateEmployeeDto.HiringDate,
                Email = updateEmployeeDto.Email,
                PhoneNumber = updateEmployeeDto.PhoneNumber,
                ModifiedDate = DateTime.Now,
                CreatedDate = getEmployee!.CreatedDate
            };

            var isUpdate = _employeeRepository.Update(employee);
            if (!isUpdate)
            {
                return 0;
            }

            return 1;
        }


        public int DeleteEmployee(Guid guid)
        {
            var isExist = _employeeRepository.IsExist(guid);
            if (!isExist)
            {
                return -1;
            }

            var employee = _employeeRepository.GetByGuid(guid);
            var isDelete = _employeeRepository.Delete(employee!);
            if (!isDelete)
            {
                return 0;
            }

            return 1;
        }


       /* public string GenerateNIK()
        {
            var getlastNik = _employeeRepository.GetAll().Select(employee => employee.Nik).LastOrDefault();

            if (getlastNik is null)
            {
                return "11111"; // No employee found, return default NIK
            }

            var lastNik = Convert.ToInt32(getlastNik) + 1;
            return lastNik.ToString();
        }*/
    }
}
