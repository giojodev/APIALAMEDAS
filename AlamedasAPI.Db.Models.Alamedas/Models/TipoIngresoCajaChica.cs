using System;
using System.Collections.Generic;

namespace AlamedasAPI.Db.Models.Alamedas.Models
{
    public partial class TipoIngresoCajaChica
    {
        /*public TipoIngresoCajaChica()
        {
            TblIngresosCajaChicas = new HashSet<TblIngresosCajaChica>();
        }*/

        public int IdIngresoaCajaChica { get; set; }
        public string NombreIngresoCajaChica { get; set; } = null!;
        public bool? Activo { get; set; }

        //public virtual ICollection<TblIngresosCajaChica> TblIngresosCajaChicas { get; set; }
    }
}
