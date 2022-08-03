using System;
using System.Collections.Generic;

namespace ApiAlamedas.Db.Models.Alamedas.Models
{
    public partial class Error
    {
        public int IdError { get; set; }
        public string Descripcion { get; set; }
        public string Pantalla { get; set; }
    }
}
