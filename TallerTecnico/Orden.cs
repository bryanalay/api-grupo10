using System.ComponentModel.DataAnnotations;

namespace TallerTecnico
{
    public class Orden
    {
        [Key]int Id { get; set; }
        string? Tarea { get; set; }
        string? Fecha { get; set; }
        string? Estado { get; set; }
        string? Cliente { get; set; }
        string? EmpleadoAsignado { get; set; }
        string? Transaccion { get; set; }
    }
}
