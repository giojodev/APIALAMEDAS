using System;
using System.Collections.Generic;

namespace AlamedasAPI.Models
{
    public partial class Error
    {
        public int IdError { get; set; }
        public string Descripcion { get; set; } = null!;
        public string Pantalla { get; set; } = null!;
    }
}
