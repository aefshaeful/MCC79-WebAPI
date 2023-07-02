﻿using API.Contracts;
using API.Data;
using API.DTOs.Account;
using API.DTOs.AccountRole;
using API.DTOs.Employee;
using API.DTOs.Universities;
using API.Models;
using API.Repositories;
using API.Utilities.Enums;
using System.Security.Claims;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using static System.Net.WebRequestMethods;
using API.DTOs.Booking;
using System.Diagnostics.Metrics;
using System.Xml.Linq;


namespace API.Services
{
    public class AccountService
    {
        private readonly BookingDbContext _context;
        private readonly IAccountRepository _accountRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUniversityRepository _universityRepository;
        private readonly IEducationRepository _educationRepository;
        private readonly ITokenHandler _tokenHandler;
        private readonly IEmailHandler _emailHandler;
        private readonly IAccountRoleRepository _accountRoleRepository;
        private readonly IRoleRepository _roleRepository;

        public AccountService(BookingDbContext context, 
                IAccountRepository accountRepository,
                IEmployeeRepository employeeRepository,
                IUniversityRepository universityRepository,
                IEducationRepository educationRepository,
                ITokenHandler tokenHandler,
                IEmailHandler emailHandler,
                IAccountRoleRepository accountRoleRepository,
                IRoleRepository roleRepository)
        {
            _context = context;
            _accountRepository = accountRepository;
            _employeeRepository = employeeRepository;
            _universityRepository = universityRepository;
            _educationRepository = educationRepository;
            _tokenHandler = tokenHandler;
            _emailHandler = emailHandler;
            _accountRoleRepository = accountRoleRepository;
            _roleRepository = roleRepository;
        }


        public GetRegisterDto RegisterAccount(GetRegisterDto getRegisterDto)
        {
            EmployeeService employeService = new EmployeeService(_employeeRepository, _universityRepository, _educationRepository);
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var employeeData = new Employee
                {
                    Guid = new Guid(),
                    Nik = employeService.GenerateNIK(),
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
                var createdEmployee = _employeeRepository.Create(employeeData);

                var universityEntity = _universityRepository.CreateWithDCheckCodeAndName(getRegisterDto.UniversityCode, getRegisterDto.UniversityName);
                if (universityEntity is null)
                {
                    var university = new University
                    {/* Guid = new Guid(),*/
                        Code = getRegisterDto.UniversityCode,
                        Name = getRegisterDto.UniversityName
                    };
                    universityEntity = _universityRepository.Create(university);
                }


                var educationData = new Education
                {
                    Guid = employeeData.Guid,
                    Major = getRegisterDto.Major,
                    Degree = getRegisterDto.Degree,
                    Gpa = getRegisterDto.Gpa,
                    UniversityGuid = universityEntity.Guid
                };
                var createdEducation = _educationRepository.Create(educationData);


                var account = new Account
                {
                    Guid = employeeData.Guid,
                    Password = Hashing.HashPassword(getRegisterDto.Password),
                };
                var createdAccount = _accountRepository.Create(account);


                var getRoleUser = _roleRepository.GetByName("User");
                var getAccountRoleUser = new AccountRole
                {
                    AccountGuid = account.Guid,
                    RoleGuid = getRoleUser.Guid
                };
                var createdAccountRoleUser = _accountRoleRepository.Create(getAccountRoleUser);


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
                    ConfirmPassword = getRegisterDto.ConfirmPassword,
                    Major = createdEducation.Major,
                    Degree = createdEducation.Degree,
                    Gpa = createdEducation.Gpa,
                    UniversityCode = universityEntity.Code,
                    UniversityName = universityEntity.Name
                };

                transaction.Commit();
                return toDto;
            }
            catch
            {
                transaction.Rollback();
                return null;
            }
        }


        public string LoginAccount(LoginDto loginDto)
        {
            var emailEmp = _employeeRepository.GetEmail(loginDto.Email);
            if (emailEmp == null)
            {
                return "0";
            }

            var passwordEmp = _accountRepository.GetByGuid(emailEmp.Guid);
            if (passwordEmp == null)
            {
                return "0";
            }

            var isValid = Hashing.ValidatePassword(loginDto.Password, passwordEmp!.Password);
            if (!isValid)
            {
                return "-1";
            }


            try
            {
                var claims = new List<Claim>()
                {
                    new Claim("NIK", emailEmp.Nik),
                    new Claim("FullName", $"{emailEmp.FirstName} {emailEmp.LastName}"),
                    new Claim("Email", loginDto.Email)
                };
               
                var getAccountRole = _accountRoleRepository.GetAccountRoleByAccountGuid(emailEmp.Guid);
                var getRoleNameByAccountRole = from ar in getAccountRole
                                               join r in _roleRepository.GetAll() on ar.RoleGuid equals r.Guid
                                               select r.Name;

                claims.AddRange(getRoleNameByAccountRole.Select(role => new Claim(ClaimTypes.Role, role)));

                var getToken = _tokenHandler.GenerateToken(claims);
                return getToken;
            }

            catch (Exception)
            {
                return "-2";
            }
            
        }


        public int ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            var emailValidation = _employeeRepository.CheckEmail(forgotPasswordDto.Email);
            if (emailValidation == null)
            {
                return 0;
            }


            var getAccount = _accountRepository.GetByGuid(emailValidation.Guid);


            int generateOtp = int.Parse(new string(Enumerable.Range(1, 6).Select(i => $"{RandomNumberGenerator.GetInt32(0, 10)}"[0]).ToArray()));


            var updateAccountDto = new Account
            {
                Guid = emailValidation.Guid,
                Password = getAccount.Password,
                IsDeleted = getAccount.IsDeleted,
                Otp = generateOtp,
                IsUsed = false,
                ExpiredTime = DateTime.Now.AddMinutes(5)
            };

            var updateResult = _accountRepository.Update(updateAccountDto);
            if (!updateResult)
            {
                return -1;
            }

            var client = emailValidation.FirstName;
            _emailHandler.SendEmail(forgotPasswordDto.Email, "Forgot Password", $"Hello {client}, Your OTP code is {generateOtp}");


            return 1;
        }


        public int ChangePassword(ChangePasswordDto changePasswordDto)
        {
            var changePassword = _employeeRepository.CheckEmail(changePasswordDto.Email);
            if (changePassword is null)
            {
                return 0;
            }

            var getAccount = _accountRepository.GetByGuid(changePassword.Guid);
            if (getAccount.Otp != changePasswordDto.Otp)
            {
                return -1;
            }

            if (getAccount.IsUsed == true)
            {
                return -2;
            }

            if (getAccount.ExpiredTime < DateTime.Now)
            {
                return -3;
            }

            var accountNewPassword = new Account
            {

                Guid = getAccount.Guid,
                Password = Hashing.HashPassword(changePasswordDto.NewPassword),
                IsDeleted = getAccount.IsDeleted,
                Otp = getAccount.Otp,
                IsUsed = getAccount.IsUsed,
                ExpiredTime = getAccount.ExpiredTime,
                ModifiedDate = DateTime.Now,
                CreatedDate = getAccount!.CreatedDate
            };

            var isUpdate = _accountRepository.Update(accountNewPassword);
            if (!isUpdate)
            {
                return -4;
            }

            return 1;
        }


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
                IsUsed = account.IsUsed,
                ExpiredTime = account.ExpiredTime
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
                IsUsed = account.IsUsed,
                ExpiredTime = account.ExpiredTime,
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
                ExpiredTime = newAccountDto.ExpiredTime,
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
                ExpiredTime = account.ExpiredTime
            };

            return toDto;
        }


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
                ExpiredTime = updateAccountDto.ExpiredTime,
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


/** < summary >
*Kelas AccountService merupakan layanan atau service untuk melakukan operasi terkait akun. Kelas tersebut memiliki beberapa dependency yang diinject melalui konstruktor, yaitu IAccountRepository, IEmployeeRepository, IUniversityRepository, dan IEducationRepository.
* Method GetRegisterDto bertujuan untuk mendaftarkan akun baru dengan menggunakan data yang diberikan dalam objek GetRegisterDto. Data tersebut digunakan untuk membuat dan menyimpan objek Employee, University, Education, dan Account ke dalam repository yang sesuai.
*/