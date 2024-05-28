using Application.RentalCar;
using Microsoft.AspNetCore.Mvc;

namespace Web.RentalCar.Controllers
{
    public class AccountController : Controller
    {
        private readonly RentalCarServices _rentalCarServices;
        public AccountController(RentalCarServices rentalCarServices)
        {
            _rentalCarServices = rentalCarServices;
        }
        public IActionResult Index()
        {
            var accountList = _rentalCarServices.GetAllAccounts();
            return View(accountList);
        }
    }
}
