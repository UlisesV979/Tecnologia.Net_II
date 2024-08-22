namespace ClaseN1.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public bool EsActivo { get; set; }
    }
}
