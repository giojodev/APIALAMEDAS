using System;

namespace AlamedasAPI.Infraestructure.Alamedas.DTO
{
    public class UserDTO
    {
        public int IdUsuario { get; set; }
        public int IdSql { get; set; }
        public int IdRol { get; set; }
        public bool Activo { get; set; }
        public bool Admin { get; set; }
        public bool Error { get; set; }
        public string Message { get; set; }
    }
}
