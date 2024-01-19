using System.ComponentModel.DataAnnotations;

namespace TallerTecnico
{
    public class InventarioPiezas
    {
        [Key]int Id { get; set; }
        string? Nombre { get; set; }
        string? Descripcion {  get; set; }
        int Cantidad { get; set; }
        string? Transaccion { get; set; }
    }
}
