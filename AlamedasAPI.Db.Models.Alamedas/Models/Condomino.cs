using System;
using System.Collections.Generic;

namespace AlamedasAPI.Db.Models.Alamedas.Models
{
    public partial class Condomino
    {
        /*public Condomino()
        {
            Moras = new HashSet<Mora>();
        }*/

        public int IdCondomino { get; set; }
        public string NombreCompleto { get; set; } = null!;
        public string NombreInquilino { get; set; } = null!;
        public string? Correo { get; set; }
        public string? Telefono { get; set; }
        public bool? Activo { get; set; }

        //public virtual ICollection<Mora> Moras { get; set; }
    }
}
