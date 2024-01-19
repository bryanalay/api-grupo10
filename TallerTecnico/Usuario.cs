using System.ComponentModel.DataAnnotations;

namespace TallerTecnico
{
    public class Usuario
    {
        [Key]int Id { get; set; }
        string? Nombre { get; set; }
        string? Cedula { get; set; }
        string? Celular { get; set; }
        string? Correo { get; set; }
        string? Password { get; set; }
        string? Transaccion {  get; set; }
    }
}
