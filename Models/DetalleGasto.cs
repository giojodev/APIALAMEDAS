using System;
using System.Collections.Generic;

namespace AlamedasAPI.Models
{
    public partial class DetalleGasto
    {
        public int Consecutivo { get; set; }
        public int IdEntity { get; set; }
        public string Concepto { get; set; } = null!;
        public double Valor { get; set; }

        public virtual Gasto ConsecutivoNavigation { get; set; } = null!;
    }
}
