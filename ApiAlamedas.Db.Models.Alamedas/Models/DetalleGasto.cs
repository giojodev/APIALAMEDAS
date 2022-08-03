﻿using System;
using System.Collections.Generic;

namespace ApiAlamedas.Db.Models.Alamedas.Models
{
    public partial class DetalleGasto
    {
        public int Consecutivo { get; set; }
        public int IdEntity { get; set; }
        public string Concepto { get; set; }
        public double Valor { get; set; }

        public virtual Gasto ConsecutivoNavigation { get; set; }
        public virtual ProductoGasto IdEntityNavigation { get; set; }
    }
}
