using API.Contracts;
using API.DTOs.Account;
using API.DTOs.Universities;
using API.Models;
using API.Repositories;
using API.Utilities.Enums;

namespace API.Services
{
    public class AccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUniversityRepository _universityRepository;
        private readonly IEducationRepository _educationRepository;

        public AccountService(IAccountRepository accountRepository,
            IEmployeeRepository employeeRepository,
            IUniversityRepository universityRepository,
            IEducationRepository educationRepository)
        {
            _accountRepository = accountRepository;
            _employeeRepository = employeeRepository;
            _universityRepository = universityRepository;
            _educationRepository = educationRepository;
        }

        /*   
        * <summary>
        * Kelas AccountService merupakan layanan atau service untuk melakukan operasi terkait akun. Kelas tersebut memiliki beberapa dependency yang diinject melalui konstruktor, yaitu IAccountRepository, IEmployeeRepository, IUniversityRepository, dan IEducationRepository.
        */

        /*private readonly IAccountRepository _accountRepository;
        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }*/

        public IEnumerable<GetAccountDto>? GetAccount()
        {
            var accounts = _accountRepository.GetAll();
            if (!accounts.Any())
            {
                return null;
            }

            var toDto = accounts.Select(account => new GetAccountDto
            {
                Guid = account.Guid,
                Password = account.Password,
                IsDeleted = account.IsDeleted,
                Otp = account.Otp,
                IsUsed = account.IsUsed
            }).ToList();

            return toDto;
        }


        public GetAccountDto? GetAccount(Guid guid)
        {
            var account = _accountRepository.GetByGuid(guid);

            if (account is null)
            {
                return null;
            }

            var toDto = new GetAccountDto
            {
                Guid = account.Guid,
                Password = account.Password,
                IsDeleted = account.IsDeleted,
                Otp = account.Otp,
                IsUsed = account.IsUsed
            };

            return toDto;
        }


        public GetAccountDto? CreateAccount(NewAccountDto newAccountDto)
        {
            var account = new Account
            {
                Guid = newAccountDto.Guid,
                Password = Hashing.HashPassword(newAccountDto.Password),
                IsDeleted = newAccountDto.IsDeleted,
                Otp = newAccountDto.Otp,
                IsUsed = newAccountDto.IsUsed,
               /* ExpiredTime = newAccountDto.ExpiredTime,*/
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };

            var createdAccount = _accountRepository.Create(account);
            if (createdAccount is null)
            {
                return null;
            }

            var toDto = new GetAccountDto
            {
                Guid = account.Guid,
                Password = account.Password,
                IsDeleted = account.IsDeleted,
                Otp = account.Otp,
                IsUsed = account.IsUsed,
                /*ExpiredTime = account.ExpiredTime*/
            };

            return toDto;
        }


        public GetRegisterDto? RegisterAccount(GetRegisterDto getRegisterDto) // 
        {
            EmployeeService employeeService = new EmployeeService(_employeeRepository);
            Employee employee = new Employee
            {
                Guid = new Guid(),
                Nik = employeeService.GenerateNIK(),
                FirstName = getRegisterDto.FirstName,
                LastName = getRegisterDto.LastName,
                BirthDate = getRegisterDto.BirthDate,
                Gender = getRegisterDto.Gender,
                HiringDate = getRegisterDto.HiringDate,
                Email = getRegisterDto.Email,
                PhoneNumber = getRegisterDto.PhoneNumber,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };

            var createdEmployee = _employeeRepository.Create(employee);
            if (createdEmployee is null)
            {
                return null;
            }

            University university = new University
            {
                Guid = new Guid(),
                Code = getRegisterDto.UniversityCode,
                Name = getRegisterDto.UniversityName
            };

            var createdUniversity = _universityRepository.Create(university);
            if (createdUniversity is null)
            {
                return null;
            }

            Education education = new Education
            {
                Guid = employee.Guid,
                Major = getRegisterDto.Major,
                Degree = getRegisterDto.Degree,
                Gpa = getRegisterDto.Gpa,
                UniversityGuid = university.Guid
            };

            var createdEducation = _educationRepository.Create(education);
            if (createdEducation is null)
            {
                return null;
            }

            Account account = new Account
            {
                Guid = employee.Guid,
                Password = Hashing.HashPassword(getRegisterDto.Password),
            };

            if (getRegisterDto.Password != getRegisterDto.ConfirmPassword)
            {
                return null;
            }

            var createdAccount = _accountRepository.Create(account);
            if (createdAccount is null)
            {
                return null;
            }


            var toDto = new GetRegisterDto
            {
                FirstName = createdEmployee.FirstName,
                LastName = createdEmployee.LastName,
                BirthDate = createdEmployee.BirthDate,
                Gender = createdEmployee.Gender,
                HiringDate = createdEmployee.HiringDate,
                Email = createdEmployee.Email,
                PhoneNumber = createdEmployee.PhoneNumber,
                Password = createdAccount.Password,
                Major = createdEducation.Major,
                Degree = createdEducation.Degree,
                Gpa = createdEducation.Gpa,
                UniversityCode = createdUniversity.Code,
                UniversityName = createdUniversity.Name
            };

            return toDto;
        }


       /*   
       * <summary>
       * Secara keseluruhan, kode tersebut bertujuan untuk mendaftarkan akun baru dengan menggunakan data yang diberikan dalam objek GetRegisterDto. Data tersebut digunakan untuk membuat dan menyimpan objek Employee, University, Education, dan Account ke dalam repository yang sesuai.
       */



        public int UpdateAccount(UpdateAccountDto updateAccountDto)
        {
            var isExist = _accountRepository.IsExist(updateAccountDto.Guid);
            if (!isExist)
            {
                return -1;
            }

            var getAccount= _accountRepository.GetByGuid(updateAccountDto.Guid);

            var account = new Account
            {

                Guid = updateAccountDto.Guid,
                Password = Hashing.HashPassword(updateAccountDto.Password),
                IsDeleted = updateAccountDto.IsDeleted,
                Otp = updateAccountDto.Otp,
                IsUsed = updateAccountDto.IsUsed,
                /*ExpiredTime = updateAccountDto.ExpiredTime,*/
                ModifiedDate = DateTime.Now,
                CreatedDate = getAccount!.CreatedDate
            };

            var isUpdate = _accountRepository.Update(account);
            if (!isUpdate)
            {
                return 0;
            }

            return 1;
        }


        public int DeleteAccount(Guid guid)
        {
            var isExist = _accountRepository.IsExist(guid);
            if (!isExist)
            {
                return -1;
            }

            var account = _accountRepository.GetByGuid(guid);
            var isDelete = _accountRepository.Delete(account!);
            if (!isDelete)
            {
                return 0;
            }

            return 1;
        }
    }
}
