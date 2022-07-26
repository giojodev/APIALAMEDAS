using System;
using System.Collections.Generic;

namespace AlamedasAPI.Models
{
    public partial class DetalleIngreso
    {
        public int Consecutivo { get; set; }
        public int IdMora { get; set; }
        public string? Concepto { get; set; }
        public string? Mes { get; set; }
        public string? Anio { get; set; }
        public int? DiasVencido { get; set; }
        public double Valor { get; set; }

        public virtual Ingreso ConsecutivoNavigation { get; set; } = null!;
        public virtual Mora IdMoraNavigation { get; set; } = null!;
    }
}
