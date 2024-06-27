using Application.RentalCar.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.RentalCar.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAccountRepository
    {


        /// <summary>
        /// 建立帳號
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        int CreatAccount(AccountViewModel account); 

        /// <summary>
        /// 編輯帳號
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        int EditAccount(AccountViewModel account);

        /// <summary>
        /// 刪除帳號
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        int DeleteAccount(AccountViewModel account);

        /// <summary>
        /// 取Account資料
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        AccountViewModel? GetAccount(string userId);
    }
}
