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
            var tipoEmpleados = _context.TipoEmpleados.ToList();
            ViewBag.TipoEmpleados = tipoEmpleados;
            return View();
        }

        // Acci�n para manejar el POST del formulario de creaci�n
        [HttpPost]
        public IActionResult Keep(Empleados empleado)
        {
            // Verifica si el modelo es v�lido
            if (ModelState.IsValid)
            {
                empleado.FechaCreacion = DateTime.Now;
                _context.Empleados.Add(empleado);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            // Recarga la lista de tipos de empleados
            ViewBag.TipoEmpleados = _context.TipoEmpleados.ToList();
            return View("Create", empleado);
        }

        // Acci�n para mostrar el formulario de edici�n
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
            return View(empleado);
        }

        // Acci�n para manejar el POST del formulario de edici�n
        [HttpPost]
        public IActionResult Update(Empleados empleado)
        {
            if (ModelState.IsValid)
            {
                // Buscar la entidad existente en la base de datos
                var empleadoExistente = _context.Empleados.FirstOrDefault(e => e.Id == empleado.Id);

                if (empleadoExistente != null)
                {
                    // Actualizar los campos
                    empleadoExistente.Nombre = empleado.Nombre;
                    empleadoExistente.Apellido = empleado.Apellido;
                    empleadoExistente.Dui = empleado.Dui;
                    empleadoExistente.NumeroTelefonico = empleado.NumeroTelefonico;
                    empleadoExistente.TipoEmpleadoId = empleado.TipoEmpleadoId;
                    empleadoExistente.FechaModificacion = DateTime.Now;
                    empleadoExistente.EsActivo = empleado.EsActivo;
                    _context.Empleados.Update(empleadoExistente);
                    _context.SaveChanges();

                    return RedirectToAction("Index");
                }
            }

            // Recargar la lista de tipos de empleados si hay error
            ViewBag.TipoEmpleados = _context.TipoEmpleados.ToList();
            return View("Edit", empleado);
        }

        // Acci�n para manejar la eliminaci�n
        [HttpDelete]
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
