using System;

namespace AlamedasAPI.Infraestructure.Alamedas.DTO
{
    public class CondominoDTO
    {
        public int IdCondomino { get; set; }
        public string nombreCompleto { get; set; }
        public string nombreInquilino { get; set; }
        public string correo { get; set; }
        public string telefono { get; set; }
        public bool activo { get; set; }
    }
}
