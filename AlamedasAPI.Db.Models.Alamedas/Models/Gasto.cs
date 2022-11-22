using System;
using System.Collections.Generic;

namespace AlamedasAPI.Db.Models.Alamedas.Models
{
    public partial class Gasto
    {
        /*public Gasto()
        {
            DetalleGastos = new HashSet<DetalleGasto>();
        }*/

        public int Consecutivo { get; set; }
        public int Usuario { get; set; }
        public int Gasto1 { get; set; }
        public DateTime Fecha { get; set; }
        public string? Concepto { get; set; }
        public decimal Valor { get; set; }
        public int? Mes { get; set; }
        public int? Anio { get; set; }

        /*public virtual TipoGasto Gasto1Navigation { get; set; } = null!;
        public virtual Usuario UsuarioNavigation { get; set; } = null!;
        public virtual ICollection<DetalleGasto> DetalleGastos { get; set; }*/
    }
}
