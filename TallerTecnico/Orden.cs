using System.ComponentModel.DataAnnotations;

namespace TallerTecnico
{
    public class Orden
    {
        [Key]public int Id { get; set; }
        public string? Tarea { get; set; }
        public string? Fecha { get; set; }
        public string? Estado { get; set; }
        public string? Cliente { get; set; }
        public string? EmpleadoAsignado { get; set; }
        public string? Transaccion { get; set; }
    }
}
