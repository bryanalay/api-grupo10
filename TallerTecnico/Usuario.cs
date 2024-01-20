using System.ComponentModel.DataAnnotations;

namespace TallerTecnico
{
    public class Usuario
    {
        [Key]public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Cedula { get; set; }
        public string? Celular { get; set; }
        public string? Correo { get; set; }
        public string? Password { get; set; }
        public string? Transaccion {  get; set; }
    }
}
