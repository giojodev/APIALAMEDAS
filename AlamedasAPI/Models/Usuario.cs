using System;
using System.Collections.Generic;

namespace AlamedasAPI.Models
{
    public partial class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; } = null!;
        public byte[] Clave { get; set; } = null!;
    }
}
