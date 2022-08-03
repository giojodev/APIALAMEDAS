using System;
using System.Collections.Generic;

namespace ApiAlamedas.Db.Models.Alamedas.Models
{
    public partial class TipoIngreso
    {
        public TipoIngreso()
        {
            Ingresos = new HashSet<Ingreso>();
        }

        public int IdIngreso { get; set; }
        public string NombreIngreso { get; set; }
        public bool? Activo { get; set; }

        public virtual ICollection<Ingreso> Ingresos { get; set; }
    }
}
