using Application.RentalCar;
using Application.RentalCar.ViewModels;
using Common.RentalCar.Crypto;
using EasyArchitectCore.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Web.RentalCar.Utility;

namespace Web.RentalCar.Controllers
{
    public class AccountController : Controller
    {
        private readonly RentalCarServices _rentalCarServices;
        private readonly AccountServices _accountServices;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        public AccountController(
            RentalCarServices rentalCarServices,
            AccountServices accountServices,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration)
        {
            _rentalCarServices = rentalCarServices;
            _accountServices = accountServices;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        [Authorize]
        public IActionResult Index()
        {
            var accountList = _rentalCarServices.GetAllAccounts();
            return View(accountList);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(AccountViewModel account)
        {
            if (ModelState.IsValid)
            {
                if (_accountServices.ValidationAccount(account))
                {
                    if (ProcessLogin(account))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return View();
        }

        public IActionResult Logout()
        {
            return View();
        }

        public IActionResult RunLogout()
        {
            _httpContextAccessor.HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Home");
        }

        /// <summary>
        /// 註冊帳號畫面一開始的進入點
        /// </summary>
        /// <returns></returns>
        public IActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// 註冊帳號
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Register(AccountViewModel account)
        {
            if (ModelState.IsValid)
            {
                if (_accountServices.RegisterAccount(account))
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            return View();
        }

        protected virtual bool ProcessLogin(AccountViewModel? account)
        {
            bool result = true;
            ClaimsPrincipal principal = new ClaimsPrincipal(new ClaimsIdentity(new Claim[2]
            {
                new Claim("name", account.UserId),
                new Claim("role", "Admin")
            }, "Cookies"));
            try
            {
                _httpContextAccessor.HttpContext.SignInAsync(principal);
                int value = _configuration.GetSection("AppSettings").GetValue<int>("TimeoutMinutes");
                CookieOptions options = new CookieOptions
                {
                    Expires = DateTime.Now.AddMinutes(value),
                    HttpOnly = true
                };
                NewCookie newCookie = new NewCookie(UserInfo.LOGIN_USER_INFO);
                newCookie.Values.Add("Username", account.UserId);
                string jsonByNewCookie = NewCookie.GetJsonByNewCookie(newCookie);
                _httpContextAccessor.HttpContext.Response.Cookies.Append(UserInfo.LOGIN_USER_INFO, jsonByNewCookie, options);
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }
    }
}
