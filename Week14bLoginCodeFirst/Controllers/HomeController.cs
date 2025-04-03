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

        [HttpGet]
        public IActionResult GetEmployeeData()
        {
            var Emp = _db.Employees.ToList();
            return View(Emp);
        }

        public IActionResult Delete(int id)
        {
            var Emp = _db.Employees.FirstOrDefault(x => x.Id == id);     // matching with database id
            _db.Employees.Remove(Emp);
            _db.SaveChanges();
            return RedirectToAction("GetEmployeeData");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var Emp = _db.Employees.FirstOrDefault(x => x.Id == id);

            return View(Emp);
        }

        [HttpPost]
        public IActionResult Update(Employee obj)       // Saving edited page with information
        {
            _db.Employees.Update(obj);
            _db.SaveChanges();
            //return View();
            return RedirectToAction("GetEmployeeData");
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

