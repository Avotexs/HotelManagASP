using HotelManagASP.Data;
using HotelManagASP.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace HotelManagASP.Controllers
{
    public class ReservationController : Controller
    {
        private readonly ContexteHM _context;

        public ReservationController(ContexteHM context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Reserver(int chambreId)
        {
            // Pré-remplir le formulaire avec les informations de la chambre
            var chambre = _context.Chambres.FirstOrDefault(c => c.id == chambreId);

            if (chambre == null)
            {
                return NotFound("Chambre introuvable.");
            }

            var model = new ReservationViewModel
            {
                ChambreId = chambreId,
                dateArrive = DateTime.Now, // Date par défaut
                dateSortie = DateTime.Now.AddDays(1) // Date par défaut
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Reserver(ReservationViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Récupérer les informations du client connecté
                var clientId = HttpContext.Session.GetInt32("ClientId");

                if (clientId == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                var chambre = _context.Chambres.FirstOrDefault(c => c.id == model.ChambreId);

                if (chambre == null)
                {
                    return NotFound("Chambre introuvable.");
                }

                // Calculer le prix total
                var nombreDeNuits = (model.dateSortie - model.dateArrive).Days;
                var prixtotal = nombreDeNuits * chambre.Prix;

                // Créer la réservation
                var reservation = new Reservation
                {
                    ClientId = clientId.Value,
                    ChambreId = model.ChambreId,
                    dateArrive = model.dateArrive,
                    dateSortie = model.dateSortie,
                    prixtotal = prixtotal,
                    statut = "En attente"
                };

                _context.Reservations.Add(reservation);
                _context.SaveChanges();

                // Rediriger vers une page de confirmation
                return RedirectToAction("Confirmation", new { id = reservation.id });
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Confirmation(int id)
        {
            var reservation = _context.Reservations
                .Where(r => r.id == id)
                .Select(r => new
                {
                    r.id,
                    r.dateArrive,
                    r.dateSortie,
                    r.prixtotal,
                    Chambre = r.Chambre.type_Chambre,
                    Client = r.Client.Nom + " " + r.Client.Prenom
                })
                .FirstOrDefault();

            if (reservation == null)
            {
                return NotFound("Réservation introuvable.");
            }

            ViewBag.Reservation = reservation;
            return View();
        }
    }
}
