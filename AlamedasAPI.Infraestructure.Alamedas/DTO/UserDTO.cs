using System;

namespace AlamedasAPI.Infraestructure.Alamedas.DTO
{
    public class UserDTO
    {
        public int Iduser { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public string Message { get; set; }
        public bool Authenticate { get; set; }
    }
}
