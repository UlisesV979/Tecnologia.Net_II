using System.ComponentModel.DataAnnotations.Schema;

namespace ClaseN1.Models
{
    public class Empleados : BaseEntity
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Dui { get; set; }
        public int NumeroTelefonico { get; set; }

        [ForeignKey("TipoEmpleado")]
        public int TipoEmpleadoId { get; set; }
        public TipoEmpleado TipoEmpleado { get; set; }
    }
}
