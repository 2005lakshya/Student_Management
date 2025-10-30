using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using TryMVC.Models;
using YourApp.Models;

namespace YourApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        // ------------------- LOGIN -------------------
        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var student = await _context.Students
                    .FirstOrDefaultAsync(s => s.Username == model.Username && s.Password == model.Password);

                if (student != null)
                {
                    HttpContext.Session.SetString("StudentId", student.Id.ToString());
                    HttpContext.Session.SetString("StudentName", student.Name);
                    HttpContext.Session.SetString("StudentUsername", student.Username);
                    return RedirectToAction("Index", "Account");
                }
                else
                {
                    ViewBag.ErrorMessage = "Invalid username or password.";
                    return View(model);
                }
            }
            return View(model);
        }


        // ------------------- SIGN / SIGNUP -------------------
        public IActionResult Sign()
        {
            return View(new SignModel());
        }

        [HttpPost]
        public IActionResult Sign(SignModel model)
        {
            if (ModelState.IsValid)
            {
                TempData["SignupUsername"] = model.Username;
                TempData["SignupPassword"] = model.Password;
                return RedirectToAction("Form", "Account");
            }
            return View(model);
        }

        // ------------------- FORM -------------------
        [HttpGet]
        public IActionResult Form()
        {
            var model = new FormModel();

            if (TempData["SignupUsername"] != null)
            {
                model.Username = TempData["SignupUsername"].ToString();
                model.Password = TempData["SignupPassword"].ToString();
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Form(FormModel model)
        {
            if (ModelState.IsValid)
            {
                _context.Students.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Account");
            }
            return View(model);
        }

        // ------------------- HOME / STUDENT LIST -------------------
        public async Task<IActionResult> Index()
        {
            var username = HttpContext.Session.GetString("StudentUsername");
            
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login", "Account");
            }

            if (username.ToLower() == "admin")
            {
                var allStudents = await _context.Students.ToListAsync();
                return View(allStudents);
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.Username == username);

            if (student == null)
            {
                return RedirectToAction("Login", "Account");
            }

            return View(new List<FormModel> { student });
        }
    }
}
