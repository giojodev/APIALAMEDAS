using System;
using System.Collections.Generic;

namespace ApiAlamedas.Db.Models.Alamedas.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Gastos = new HashSet<Gasto>();
            Ingresos = new HashSet<Ingreso>();
        }

        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public byte[] Clave { get; set; }

        public virtual ICollection<Gasto> Gastos { get; set; }
        public virtual ICollection<Ingreso> Ingresos { get; set; }
    }
}
