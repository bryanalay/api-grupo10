﻿using System.ComponentModel.DataAnnotations;

namespace TallerTecnico
{
    public class InventarioPiezas
    {
        [Key]public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion {  get; set; }
        public int Cantidad { get; set; }
        public string? Transaccion { get; set; }
        public int Tipo { get; set; }
    }
}
