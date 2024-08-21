using ClaseN1.Data;
using ClaseN1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        // Acci�n para la vista principal
        public IActionResult Index()
        {
            var empleados = _context.Empleados.Include(e=> e.TipoEmpleado).ToList();
            ViewBag.Empleados = empleados;
            return View();
        }

        // Acci�n para mostrar el formulario de creaci�n
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Acci�n para manejar el POST del formulario de creaci�n
        [HttpPost]
        public IActionResult Create(Empleados empleado)
        {
            if (ModelState.IsValid)
            {
                _context.Empleados.Add(empleado);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(empleado);
        }

        // Acci�n para mostrar el formulario de edici�n
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var empleado = _context.Empleados.Find(id);
            if (empleado == null)
            {
                return NotFound();
            }
            return View(empleado);
        }

        // Acci�n para manejar el POST del formulario de edici�n
        [HttpPost]
        public IActionResult Edit(Empleados empleado)
        {
            if (ModelState.IsValid)
            {
                _context.Empleados.Update(empleado);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(empleado);
        }

        // Acci�n para manejar la eliminaci�n
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var empleado = _context.Empleados.Find(id);
            if (empleado == null)
            {
                return NotFound();
            }
            return View(empleado);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var empleado = _context.Empleados.Find(id);
            if (empleado != null)
            {
                _context.Empleados.Remove(empleado);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
