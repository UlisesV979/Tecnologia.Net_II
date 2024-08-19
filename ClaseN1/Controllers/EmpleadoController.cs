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

        [HttpGet("GetEmpleados")]
        public IActionResult GetEmpleados()
        {
            var result = _context.Empleados.ToList();
            if (!result.Any())
            {
                return Json(new { data = new string[] { } });
            }
            return Json(new { data = result.Where(x => x.EsActivo)});
        }

        [HttpPost("KeepEmpleados")]
        public async Task<IActionResult> Keep(Empleados empleado)
        {
            return RedirectToAction("Index", "Empleado");
        }

        [HttpPut("UpdateEmpleados")]
        public IActionResult Update(Empleados empleado)
        {
            empleado.EsActivo = true;
            _context.Empleados.Update(empleado);
            return Ok(new string[] { "success", "Empleado actualizado exitosamente", "El empleado sera actualizado en el listado" });
        }

        [HttpDelete("DeleteEmpleados")]
        public IActionResult Delete(int Id)
        {
            Empleados employee = _context.Empleados.Where(x => x.Id == Id).First();
            _context.Empleados.Remove(employee);
            return Ok(new string[] { "success", "Empleado eliminado exitosamente", "El empleado sera removido del listado" });
        }

        [HttpDelete("SoftDeleteEmpleados")]
        public async Task<IActionResult> SoftDeleteEmpleado(int Id)
        {
            Empleados empleado = _context.Empleados.Where(x => x.Id == Id).First();
            empleado.EsActivo = false;
            _context.Empleados.Update(empleado);
            return Ok(new string[] { "success", "Empleado inhabilitado exitosamente", "El empleado sera removido del listado" });
        }
    }
}
