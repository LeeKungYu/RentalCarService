using Application.RentalCar.Repository;
using Application.RentalCar.ViewModels;
using Common.RentalCar.Crypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.RentalCar
{
    /// <summary>
    /// 登入驗證相關服務
    /// </summary>
    public class AccountServices
    {
        private readonly IAccountRepository _accountRepository;

        public AccountServices(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        /// <summary>
        /// 驗證登入帳號密碼
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public bool ValidationAccount(AccountViewModel account)
        {
            //預設就是false (無法登入)
            bool result = false;

            //系統內部不一定有帳號
            AccountViewModel? accountViewModel = _accountRepository.GetAccount(account.UserId);

            //找到帳號後才去比對密碼
            if (accountViewModel != null)
            {
                //比對密碼是否符合 Liv Password = "1qaz@WSX"
                result = StringEncryptor.EncryptString(account.Password) == accountViewModel.Password;
            }

            //不建議有多個return，所以用變數變更狀態，最後以變數結果為主。
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public bool RegisterAccount(AccountViewModel account)
        {
            //只要異動筆數>0， 就是新增成功。
            return _accountRepository.CreatAccount(account) > 0;
        }
    }
}
