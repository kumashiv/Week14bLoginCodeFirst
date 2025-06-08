using System.Diagnostics;
using System.Net.Mail;
using System.Net;
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

        //[HttpPost]
        //public IActionResult Create(Employee obj)
        //{
        //    try
        //    {
        //        _db.Employees.Add(obj);     // adding to SQL query    //constructor part   
        //                                    // Employees is the table name defined in DataContext.cs
        //        _db.SaveChanges();
        //    }
        //    catch (Exception ex)
        //    {
        //        //Console.WriteLine(ex.Message);
        //        return RedirectToAction("Error");
        //    }
        //    return View();
        //}

        [HttpPost]
        public IActionResult Create(Employee obj)
        {
            _db.Employees.Add(obj);     // adding to SQL query    //constructor part   
                                        // Employees is the table name defined in DataContext.cs
            _db.SaveChanges();

            SendEmailToUser(obj.Email);

            return RedirectToAction("GetEmployeeData");
        }



        public ActionResult SendEmailToUser(string userEmail)
        {
            try
            {
                // Create a MailMessage object
                MailMessage message = new MailMessage();
                message.From = new MailAddress("kajal.kiot2002@gmail.com"); // Sender's email address
                message.To.Add(userEmail); // Recipient's email address
                message.Subject = "Account Created"; // Email subject
                                                                         // message.Body = tempdatavalu;
                message.Body = "Your registration completed succesfully";
                // Configure the SMTP client
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Host = "smtp.gmail.com"; // SMTP server address
                smtpClient.Port = 587; // SMTP port (typically 587 for TLS/STARTTLS)
                smtpClient.EnableSsl = true; // Enable SSL/TLS encryption if required
                smtpClient.Credentials = new NetworkCredential("kajal.kiot2002@gmail.com", "zqcr kpnp hpnt qmkh"); // Your email credentials
                // Send the email
                smtpClient.Send(message);
                ViewBag.Message = "Email sent successfully!";
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Failed to send the email: " + ex.Message;
            }

            return RedirectToAction("dashboard");
        }


        [HttpGet]
        public IActionResult GetEmployeeData()
        {
            var Emp = _db.Employees.Where(a => a.isActive == false || a.isActive == null).ToList();

            return View(Emp);
        }

        [HttpGet]
        public IActionResult GetDeactivatedUserData()
        {

            var Emp = _db.Employees.Where(a => a.isActive == true).ToList();

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


        //public IActionResult ActivateUser(int id)       
        //{
        //    var Emp = _db.Employees.FirstOrDefault(x => x.Id == id);

        //    //_db.Employees.Update(obj);
        //    Emp.isActive = true;
        //    _db.SaveChanges();
        //    //return View();
        //    return RedirectToAction("GetEmployeeData");
        //}


        public IActionResult ActivateUser(int id)
        {
            var Emp = _db.Employees.FirstOrDefault(x => x.Id == id);
            if (Emp.isActive == null || Emp.isActive == false)
            {
                Emp.isActive = true;
            }else
            {
                Emp.isActive = false;
            }

            _db.SaveChanges();
            return RedirectToAction("GetEmployeeData");
        }


        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LogIn(Employee obj)
        {

            var Emp = _db.Employees.FirstOrDefault(e => e.Email == obj.Email && e.Password == obj.Password);

            if (Emp != null && Emp.isActive != true)        // Only active user can Login
            {
                HttpContext.Session.SetString("userEmail", Emp.Email);      //Storing session state
                HttpContext.Session.SetString("userRole", Emp.Role);

                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public IActionResult Logout()
        {
            return RedirectToAction("Login");
        }





        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        // Step 2: Handle form submission and find employee by email
        [HttpPost]
        public IActionResult ForgotPassword(string email)
        {
            var employee = _db.Employees.FirstOrDefault(e => e.Email == email);
            if (employee != null)
            {
                // Redirect to reset password page with employee ID
                return RedirectToAction("ResetPassword", new { id = employee.Id });
            }
            else
            {
                ViewBag.Message = "Email not found.";
                return View();
            }
        }

        [HttpGet]
        public IActionResult ResetPassword(int id)
        {
            var employee = _db.Employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            //return View(new Employee { Id = employee.Id }); // Only passing ID
            return View(employee); // Pass the full employee object
        }

        [HttpPost]
        public IActionResult ResetPassword(int id, string newPassword)
        {
            var employee = _db.Employees.FirstOrDefault(e => e.Id == id);
            if (employee != null)
            {
                employee.Password = newPassword;
                _db.SaveChanges();

                return RedirectToAction("LogIn");
            }

            return NotFound();
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

