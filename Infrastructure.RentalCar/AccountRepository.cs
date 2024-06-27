using Application.RentalCar.Repository;
using Application.RentalCar.ViewModels;
using Common.RentalCar.Crypto;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.RentalCar
{
    public class AccountRepository : IAccountRepository
    {
        private readonly SaleCarDbContext _context;

        public AccountRepository(SaleCarDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 新增帳號
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public int CreatAccount(AccountViewModel account)
        {
            int result = 0;

            Account accountEnt = new Account()
            {
                UserId = account.UserId,
                //加!的意思是告訴編譯器password不會是null，因為前面欄位有加required，所以絕對會有資料
                Password = StringEncryptor.EncryptString(account.Password!),
                Aid = account.Aid,
                ChtName = account.ChtName,
                MobilePhone = account.MobilePhone,
            };

            _context.Accounts.Add(accountEnt);

            try
            {
                //異動到資料庫。
                result = _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Trace.TraceError($"新增帳號發生錯誤, SysInfo = {ex.Message}");
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public int DeleteAccount(AccountViewModel account)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public int EditAccount(AccountViewModel account)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public AccountViewModel? GetAccount(string userId)
        {
            //第一種寫法
            var account = _context.Accounts
                .Where(x => x.UserId == userId)
                .Select(x => new AccountViewModel()
                {
                    UserId = x.UserId,
                    Password = x.Password,
                    Aid = x.Aid,
                    ChtName = x.ChtName,
                    MobilePhone = x.MobilePhone,
                })
                .FirstOrDefault();
            return account;

            //第二種寫法
            //var account = _context.Accounts.FirstOrDefault(x => x.UserId == userId);
            //return account;

            //第三種寫法
            //var account = (from x in _context.Accounts
            //               where x.UserId == account.UserId
            //               select new AccountViewModel()
            //               {
            //                   UserId = x.UserId,
            //                   Password = x.Password,
            //                   Aid = x.Aid,
            //                   ChtName = x.ChtName,
            //                   MobilePhone = x.MobilePhone,
            //               })
            //              .FirstOrDefault();
            //return account;
        }
    }
}
