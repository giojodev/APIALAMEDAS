using System;
using System.Collections.Generic;

namespace ApiAlamedas.Db.Models.Alamedas.Models
{
    public partial class TipoGasto
    {
        public TipoGasto()
        {
            Gastos = new HashSet<Gasto>();
        }

        public int IdGasto { get; set; }
        public string NombreGasto { get; set; }
        public bool? Activo { get; set; }

        public virtual ICollection<Gasto> Gastos { get; set; }
    }
}
