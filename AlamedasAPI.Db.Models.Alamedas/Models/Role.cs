using System;
using System.Collections.Generic;

namespace AlamedasAPI.Db.Models.Alamedas.Models
{
    public partial class Role
    {
        /*public Role()
        {
            Usuarios = new HashSet<Usuario>();
        }*/

        public int IdRol { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }

       // public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
