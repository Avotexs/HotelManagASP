using HotelManagASP.Data;
using HotelManagASP.Models;
using HotelManagASP.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace HotelManagASP.Controllers
{
    public class AccountController : Controller
    {
        private readonly ContexteHM _context;
        private readonly IConfiguration _configuration;

        public AccountController(ContexteHM context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

       

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(ClientRegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Enregistrer le clien dans la db
                var client = new Client
                {
                    Nom = model.Nom,
                    Prenom = model.Prenom,
                    Adresee = model.Adresee,
                    Tele = model.Tele,
                    CIN = model.CIN,
                    Email = model.Email,
                    MotDePasse = BCrypt.Net.BCrypt.HashPassword(model.Password),
                    DateRejoin = DateTime.Now,
                    IsEmailVerified = false
                };

                _context.Clients.Add(client);
                await _context.SaveChangesAsync();

                // Envoyer email verification
                var emailSender = new EmailSender(_configuration);

                string emailBody = $@"
            <h1>Bienvenue, {client.Nom} {client.Prenom}!</h1>
            <p>Merci de vous être enregistré. Cliquez sur le lien ci-dessous pour vérifier votre adresse email :</p>
            <a href='https://localhost:5001/Account/VerifyEmail?email={client.Email}'>Vérifier votre email</a>";

                await emailSender.SendEmailAsync(client.Email, "Vérification d'email", emailBody);

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

                if (client != null)
                {
                    // Vérifier le mot de passe
                    if (BCrypt.Net.BCrypt.Verify(model.Password, client.MotDePasse))
                    {
                        // Vérifier si l'email a été vérifié
                        if (!client.IsEmailVerified)
                        {
                            ModelState.AddModelError("", "Votre email n'a pas encore été vérifié. Veuillez vérifier votre boîte de réception.");
                            return View(model);
                        }

                        // Si tout est correct, connecter l'utilisateur
                        HttpContext.Session.SetInt32("ClientId", client.id);
                        return RedirectToAction("Index", "Hotel");
                    }
                }

                ModelState.AddModelError("", "Email ou mot de passe invalide.");
            }

            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }
        public IActionResult VerifyEmail(string email)
        {
            var client = _context.Clients.FirstOrDefault(c => c.Email == email);
            if (client != null)
            {
                client.IsEmailVerified = true;
                _context.SaveChanges();
                ViewBag.Message = "Votre email a été vérifié avec succès. Vous pouvez maintenant vous connecter.";
            }
            else
            {
                ViewBag.Message = "Email invalide ou non trouvé.";
            }

            return View();
        }
    }
}
