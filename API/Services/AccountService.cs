using API.Contracts;
using API.DTOs.Account;
using API.DTOs.Universities;
using API.Models;


namespace API.Services
{
    public class AccountService
    {
        private readonly IAccountRepository _accountRepository;
        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
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
                Guid = new Guid(),
                Password = newAccountDto.Password,
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
                Password = updateAccountDto.Password,
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
