using System;
using System.Collections.Generic;

namespace AlamedasAPI.Db.Models.Alamedas.Models
{
    public partial class ProductoIngresoCajaChica
    {
        /*public ProductoIngresoCajaChica()
        {
            DetalleIngresoCajachicas = new HashSet<DetalleIngresoCajachica>();
        }*/

        public int Id { get; set; }
        public string Concepto { get; set; } = null!;
        public double Valor { get; set; }

        //public virtual ICollection<DetalleIngresoCajachica> DetalleIngresoCajachicas { get; set; }
    }
}
