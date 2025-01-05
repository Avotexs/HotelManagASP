using Microsoft.AspNetCore.Mvc;
using System.Xml.Serialization;
using HotelManagASP.Models;
using HotelManagASP.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelManagASP.Controllers
{
    
    public class Hotel : Controller
    {
        private readonly ContexteHM _context;
        public Hotel(ContexteHM context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View( await _context.Chambres.ToListAsync());
        }
    }
}
