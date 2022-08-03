using System;
using System.Collections.Generic;

namespace ApiAlamedas.Db.Models.Alamedas.Models
{
    public partial class ProductoGastoCajaChica
    {
        public ProductoGastoCajaChica()
        {
            DetalleGastoCajachicas = new HashSet<DetalleGastoCajachica>();
        }

        public int Id { get; set; }
        public string Concepto { get; set; }
        public double Valor { get; set; }

        public virtual ICollection<DetalleGastoCajachica> DetalleGastoCajachicas { get; set; }
    }
}
