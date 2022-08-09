using System;
using System.Collections.Generic;

namespace AlamedasAPI.Db.Models.Alamedas.Models
{
    public partial class ProductoGastoCajaChica
    {
        /*public ProductoGastoCajaChica()
        {
            DetalleGastoCajachicas = new HashSet<DetalleGastoCajachica>();
        }*/

        public int Id { get; set; }
        public string Concepto { get; set; } = null!;
        public double Valor { get; set; }

        //public virtual ICollection<DetalleGastoCajachica> DetalleGastoCajachicas { get; set; }
    }
}
