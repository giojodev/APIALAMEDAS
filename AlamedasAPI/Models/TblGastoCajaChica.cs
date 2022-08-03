using System;
using System.Collections.Generic;

namespace AlamedasAPI.Models
{
    public partial class TblGastoCajaChica
    {
        public TblGastoCajaChica()
        {
            GastosCajaChicas = new HashSet<GastosCajaChica>();
        }

        public int IdGastoCajaChica { get; set; }
        public string NombreGastoCajachica { get; set; } = null!;
        public bool? Activo { get; set; }

        public virtual ICollection<GastosCajaChica> GastosCajaChicas { get; set; }
    }
}
