using System;
using System.Collections.Generic;

namespace AlamedasAPI.Db.Models.Alamedas.Models
{
    public partial class TipoIngreso
    {
        public TipoIngreso()
        {
            Ingresos = new HashSet<Ingreso>();
        }

        public int IdIngreso { get; set; }
        public string NombreIngreso { get; set; } = null!;
        public bool? Activo { get; set; }

        public virtual ICollection<Ingreso> Ingresos { get; set; }
    }
}
