using HotelManagASP.Data;
using HotelManagASP.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace HotelManagASP.Controllers
{
    public class AccountController : Controller
    {
        private readonly ContexteHM _context;

        public AccountController(ContexteHM context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(ClientRegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_context.Clients.Any(c => c.Email == model.Email))
                {
                    ModelState.AddModelError("", "Email already exists.");
                    return View(model);
                }

                var client = new Client
                {
                    Nom = model.Nom,
                    Prenom = model.Prenom,
                    Email = model.Email,
                    Adresee = model.Adresee,
                    CIN = model.CIN,
                    Tele = model.Tele,

                    MotDePasse = BCrypt.Net.BCrypt.HashPassword(model.Password), // Hash the password
                    DateRejoin = DateTime.Now
                };

                _context.Clients.Add(client);
                _context.SaveChanges();

                return RedirectToAction("Login", "Account");
            }

            return View(model);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(ClientLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var client = _context.Clients.FirstOrDefault(c => c.Email == model.Email);

                if (client != null && BCrypt.Net.BCrypt.Verify(model.Password, client.MotDePasse))
                {
                    // Set authentication cookie or session
                    HttpContext.Session.SetInt32("ClientId", client.id);
                    return RedirectToAction("Index", "Hotel");
                }

                ModelState.AddModelError("", "Invalid email or password.");
            }

            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }
    }
}
