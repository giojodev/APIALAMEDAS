using System;
using System.Collections.Generic;

namespace AlamedasAPI.Db.Models.Alamedas.Models
{
    public partial class TipoGastoCajaChica
    {
       /* public TipoGastoCajaChica()
        {
            TblGastosCajaChicas = new HashSet<TblGastosCajaChica>();
        }*/

        public int IdGastoCajaChica { get; set; }
        public string NombreGastoCajachica { get; set; } = null!;
        public bool? Activo { get; set; }

        //public virtual ICollection<TblGastosCajaChica> TblGastosCajaChicas { get; set; }
    }
}
