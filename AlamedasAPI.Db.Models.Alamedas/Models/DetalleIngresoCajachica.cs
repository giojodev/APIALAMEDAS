﻿using System;
using System.Collections.Generic;

namespace AlamedasAPI.Db.Models.Alamedas.Models
{
    public partial class DetalleIngresoCajachica
    {
        public int Consecutivo { get; set; }
        public int IdProdgasto { get; set; }
        public string Concepto { get; set; } = null!;
        public double? Total { get; set; }
        public DateTime Fecha { get; set; }
        public int Mes { get; set; }
        public int Anio { get; set; }

       /* public virtual TblIngresosCajaChica ConsecutivoNavigation { get; set; } = null!;
        public virtual ProductoIngresoCajaChica IdProdgastoNavigation { get; set; } = null!;*/
    }
}
