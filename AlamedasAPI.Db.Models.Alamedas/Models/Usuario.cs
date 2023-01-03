using System;
using System.Collections.Generic;

namespace AlamedasAPI.Db.Models.Alamedas.Models
{
    public partial class Usuario
    {
        /*public Usuario()
        {
            Gastos = new HashSet<Gasto>();
            Ingresos = new HashSet<Ingreso>();
            MovimientosDocs = new HashSet<MovimientosDoc>();
            TblGastosCajaChicas = new HashSet<TblGastosCajaChica>();
            TblIngresosCajaChicas = new HashSet<TblIngresosCajaChica>();
        }*/

        public int IdUsuario { get; set; }
        public string? Usuario1 { get; set; }
        public string? Nombre { get; set; }
        public string? Correo { get; set; }
        public int? IdRol { get; set; }
        public bool? Activo { get; set; }
        public string? Contrasena { get; set; }

        /*public virtual Role? IdRolNavigation { get; set; }
        public virtual ICollection<Gasto> Gastos { get; set; }
        public virtual ICollection<Ingreso> Ingresos { get; set; }
        public virtual ICollection<MovimientosDoc> MovimientosDocs { get; set; }
        public virtual ICollection<TblGastosCajaChica> TblGastosCajaChicas { get; set; }
        public virtual ICollection<TblIngresosCajaChica> TblIngresosCajaChicas { get; set; }*/
    }
}
