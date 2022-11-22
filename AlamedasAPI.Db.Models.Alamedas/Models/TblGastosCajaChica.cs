using System;
using System.Collections.Generic;

namespace AlamedasAPI.Db.Models.Alamedas.Models
{
    public partial class TblGastosCajaChica
    {
        /*public TblGastosCajaChica()
        {
            DetalleGastoCajachicas = new HashSet<DetalleGastoCajachica>();
        }*/

        public int Consecutivo { get; set; }
        public int IdUsuario { get; set; }
        public int TipoGastoCchica { get; set; }
        public DateTime Fecha { get; set; }
        public string? Concepto { get; set; }
        public double Total { get; set; }
        public int? Mes { get; set; }
        public int? Anio { get; set; }
        public bool? Anulado { get; set; }

        //public virtual TblUsuario IdUsuarioNavigation { get; set; } = null!;
        //public virtual TipoGastoCajaChica TipoGastoCchicaNavigation { get; set; } = null!;
       // public virtual ICollection<DetalleGastoCajachica> DetalleGastoCajachicas { get; set; }
    }
}
