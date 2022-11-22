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
        }*/

        public int IdUsuario { get; set; }
        public string Nombre { get; set; } = null!;
        public byte[] Clave { get; set; } = null!;

        /*public virtual ICollection<Gasto> Gastos { get; set; }
        public virtual ICollection<Ingreso> Ingresos { get; set; }*/
    }
}
