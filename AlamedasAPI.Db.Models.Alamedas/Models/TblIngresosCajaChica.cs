﻿using System;
using System.Collections.Generic;

namespace AlamedasAPI.Db.Models.Alamedas.Models
{
    public partial class TblIngresosCajaChica
    {
        /*public TblIngresosCajaChica()
        {
            DetalleIngresoCajachicas = new HashSet<DetalleIngresoCajachica>();
        }*/

        public int Consecutivo { get; set; }
        public int IdUsuario { get; set; }
        public int TipoIngresoC { get; set; }
        public DateTime Fecha { get; set; }
        public string? Concepto { get; set; }
        public double Total { get; set; }
        public int? Mes { get; set; }
        public int? Anio { get; set; }
        public bool? Anulado { get; set; }

        /*public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
        public virtual TipoIngresoCajaChica TipoIngresoCNavigation { get; set; } = null!;
        public virtual ICollection<DetalleIngresoCajachica> DetalleIngresoCajachicas { get; set; }*/
    }
}
