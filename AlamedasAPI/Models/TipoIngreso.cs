using System;
using System.Collections.Generic;

namespace AlamedasAPI.Models
{
    public partial class TipoIngreso
    {
        public int IdIngreso { get; set; }
        public string NombreIngreso { get; set; } = null!;
        public bool? Activo { get; set; }
    }
}
