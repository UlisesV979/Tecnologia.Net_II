using ClaseN1.Data;
using ClaseN1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ClaseN1.Controllers
{
    public class EmpleadoController : Controller
    {
        private readonly ILogger<EmpleadoController> _logger;
        private readonly ApplicationDbContext _context;

        public EmpleadoController(
            ILogger<EmpleadoController> logger, 
            ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.Empleados = _context.Empleados.ToList();
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
