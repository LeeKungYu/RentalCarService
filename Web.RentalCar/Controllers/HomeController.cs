using Infrastructure.RentalCar;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;
using Web.RentalCar.Models;

namespace Web.RentalCar.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //private readonly SaleCarDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult MyJSON()
        {
            return Json(new
            {
                id = 1,
                name = "Liv",
                age = 18

            });
        }

        public IActionResult DownloadFile()
        {
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "Download", "通訊錄_2024.3.xlsx");
            //return Content(path, "text/txt", Encoding.UTF8);

            var stream = System.IO.File.OpenRead(path); ;
            return File(stream, "application/excel", "Mentortrust通訊錄.xlsx"); // user要下載時會自動把檔名變成Mentortrust通訊錄.txt
        }

        public IActionResult MyXML()
        {
            return Content(@"
            <xml>
                <id>1</id>
                <name>Liv</name>
                <age>18</age>
            </xml>",
            "application/xml",
            Encoding.UTF8);
        }

        public IActionResult MyPartial()
        {
            return PartialView();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
