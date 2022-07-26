using System;
using System.Collections.Generic;

namespace AlamedasAPI.Models
{
    public partial class DetalleGastoCajachica
    {
        public int Consecutivo { get; set; }
        public int IdProdgasto { get; set; }
        public string Concepto { get; set; } = null!;
        public double? Total { get; set; }
        public DateTime Fecha { get; set; }
        public int Mes { get; set; }
        public int Anio { get; set; }

        public virtual GastosCajaChica ConsecutivoNavigation { get; set; } = null!;
        public virtual ProductoGastoCajaChica IdProdgastoNavigation { get; set; } = null!;
    }
}
