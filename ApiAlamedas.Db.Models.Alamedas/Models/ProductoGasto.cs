using System;
using System.Collections.Generic;

namespace ApiAlamedas.Db.Models.Alamedas.Models
{
    public partial class ProductoGasto
    {
        public ProductoGasto()
        {
            DetalleGastos = new HashSet<DetalleGasto>();
        }

        public int IdEntity { get; set; }
        public string Concepto { get; set; }
        public double Valor { get; set; }

        public virtual ICollection<DetalleGasto> DetalleGastos { get; set; }
    }
}
