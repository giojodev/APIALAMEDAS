using System;
using System.Collections.Generic;

namespace AlamedasAPI.Models
{
    public partial class TipoGasto
    {
        public int IdGasto { get; set; }
        public string NombreGasto { get; set; } = null!;
        public bool? Activo { get; set; }
    }
}
