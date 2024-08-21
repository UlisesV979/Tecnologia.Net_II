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

        // Acción para la vista principal
        public IActionResult Index()
        {
            var empleados = _context.Empleados.Include(e=> e.TipoEmpleado).ToList();
            ViewBag.Empleados = empleados;
            return View();
        }

        // Acción para mostrar el formulario de creación
        [HttpGet]
        public IActionResult Create()
        {
            var tipoEmpleados = _context.TipoEmpleados.ToList();
            ViewBag.TipoEmpleados = tipoEmpleados;
            return View();
        }

        // Acción para manejar el POST del formulario de creación
        [HttpPost]
        public IActionResult Keep(Empleados empleado)
        {
            if (ModelState.IsValid)
            {
                empleado.FechaCreacion = DateTime.Now;
                _context.Empleados.Add(empleado);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Create");
        }

        // Acción para mostrar el formulario de edición
        [HttpGet]
        public IActionResult Edit(int id)
        {
            // Buscar el empleado por Id
            var empleado = _context.Empleados.Include(e => e.TipoEmpleado).FirstOrDefault(x => x.Id == id);

            // Evaluar si el empleado fue encontrado
            if (empleado == null)
            {
                return NotFound();
            }

            // Cargar la lista de tipos de empleados
            var tipoEmpleados = _context.TipoEmpleados.ToList();
            ViewBag.TipoEmpleados = tipoEmpleados;

            // Retornar la vista con el modelo del empleado
            return View(empleado);
        }

        // Acción para manejar el POST del formulario de edición
        [HttpPost]
        public IActionResult Update(Empleados empleado)
        {
            if (ModelState.IsValid)
            {
                empleado.FechaModificacion = DateTime.Now;
                _context.Empleados.Update(empleado);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(empleado);
        }

        // Acción para manejar la eliminación
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
