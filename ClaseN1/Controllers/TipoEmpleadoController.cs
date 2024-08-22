using ClaseN1.Data;
using ClaseN1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ClaseN1.Controllers
{
    public class TipoEmpleadoController : Controller
    {
        private readonly ILogger<TipoEmpleadoController> _logger;
        private readonly ApplicationDbContext _context;

        public TipoEmpleadoController(
            ILogger<TipoEmpleadoController> logger,
            ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // Acci�n para la vista principal
        public IActionResult Index()
        {
            var tipoempleados = _context.TipoEmpleados.ToList();
            ViewBag.TipoEmpleados = tipoempleados;
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
        public IActionResult Keep(TipoEmpleado tipoempleado)
        {
            // Verifica si el modelo es v�lido
            if (ModelState.IsValid)
            {
                tipoempleado.FechaCreacion = DateTime.Now;
                _context.TipoEmpleados.Add(tipoempleado);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            // Recarga la lista de tipos de tipoempleados
            return View("Create", tipoempleado);
        }

        // Acci�n para mostrar el formulario de edici�n
        [HttpGet]
        public IActionResult Edit(int id)
        {
            // Buscar el tipoempleado por Id
            var tipoempleado = _context.TipoEmpleados.Where(x => x.Id == id).FirstOrDefault();

            // Evaluar si el tipoempleado fue encontrado
            if (tipoempleado == null)
            {
                return NotFound();
            }

            // Cargar el tipo de empleado
            return View(tipoempleado);
        }

        // Acci�n para manejar el POST del formulario de edici�n
        [HttpPost]
        public IActionResult Update(TipoEmpleado tipoempleado)
        {
            if (ModelState.IsValid)
            {
                // Buscar la entidad existente en la base de datos
                var tipoempleadoExistente = _context.TipoEmpleados.FirstOrDefault(e => e.Id == tipoempleado.Id);

                if (tipoempleadoExistente != null)
                {
                    // Actualizar los campos
                    tipoempleadoExistente.Nombre = tipoempleado.Nombre;
                    tipoempleadoExistente.FechaModificacion = DateTime.Now;
                    tipoempleadoExistente.EsActivo = tipoempleado.EsActivo;
                    _context.TipoEmpleados.Update(tipoempleadoExistente);
                    _context.SaveChanges();

                    return RedirectToAction("Index");
                }
            }

            // Recargar la lista de tipos de tipoempleados si hay error
            return View("Edit", tipoempleado);
        }

        // Acci�n para manejar la eliminaci�n
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var tipoempleado = _context.TipoEmpleados.Find(id);
            if (tipoempleado == null)
            {
                return NotFound();
            }
            return View(tipoempleado);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var tipoempleado = _context.TipoEmpleados.Find(id);
            if (tipoempleado != null)
            {
                _context.TipoEmpleados.Remove(tipoempleado);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
