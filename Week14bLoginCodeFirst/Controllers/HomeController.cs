using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Week14bLoginCodeFirst.AppDbContext;
using Week14bLoginCodeFirst.Models;

namespace Week14bLoginCodeFirst.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _db;
        public HomeController(ILogger<HomeController> logger, DataContext db)
        {
            _logger = logger;
            _db = db;
        }


        //private readonly DataContext _db;

        //public HomeController(DataContext db)
        //{
        //    _db = db;
        //}


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee obj)
        {
            try
            {
                _db.Employees.Add(obj);     // adding to SQL query    //constructor part   
                                            // Employees is the table name defined in DataContext.cs
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                return RedirectToAction("Error");
            }
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

