using System;
using System.Collections.Generic;

namespace AlamedasAPI.Db.Models.Alamedas.Models
{
    public partial class TblUsuario
    {
        /*public TblUsuario()
        {
            MovimientosDocs = new HashSet<MovimientosDoc>();
            TblGastosCajaChicas = new HashSet<TblGastosCajaChica>();
            TblIngresosCajaChicas = new HashSet<TblIngresosCajaChica>();
        }*/

        public int IdUsuario { get; set; }
        public string? Usuario { get; set; } //username
        public string? Contrasena { get; set; }
        public string? Nombre { get; set; }
        public string? Correo { get; set; }
        public int? IdRol { get; set; }
        public bool? Activo { get; set; }
        public bool Admin { get; set; }

        //public virtual ICollection<MovimientosDoc> MovimientosDocs { get; set; }
        //public virtual ICollection<TblGastosCajaChica> TblGastosCajaChicas { get; set; }
        //public virtual ICollection<TblIngresosCajaChica> TblIngresosCajaChicas { get; set; }
    }
}
