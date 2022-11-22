using System;
using System.Collections.Generic;

namespace AlamedasAPI.Db.Models.Alamedas.Models
{
    public partial class Ingreso
    {
        /*public Ingreso()
        {
            DetalleIngresos = new HashSet<DetalleIngreso>();
        }*/

        public int Consecutivo { get; set; }
        public int Usuario { get; set; }
        public string NombreInquilino { get; set; } = null!;
        public int Ingreso1 { get; set; }
        public DateTime Fecha { get; set; }
        public string? Concepto { get; set; }
        public double Total { get; set; }
        public int? Mes { get; set; }
        public int? Anio { get; set; }
        public bool? Anulado { get; set; }

        /*public virtual TipoIngreso Ingreso1Navigation { get; set; } = null!;
        public virtual Usuario UsuarioNavigation { get; set; } = null!;
        public virtual ICollection<DetalleIngreso> DetalleIngresos { get; set; }*/
    }
}
