using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Week14bLoginCodeFirst.AppDbContext;
using Week14bLoginCodeFirst.Models;

namespace Week14bLoginCodeFirst.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        
        private readonly DataContext _db;

        public HomeController(DataContext db)
        {
            _db = db;
        }


        //public HomeController(ILogger<HomeController> logger, DataContext db)
        //{
        //    _logger = logger;
        //    _db=db;
        //}


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LogIn()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

