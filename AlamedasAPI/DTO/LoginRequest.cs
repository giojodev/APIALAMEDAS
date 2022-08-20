using System;

namespace AlamedasAPI.DTO
{
    public class LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public LoginRequest(){
            UserName = "";
            Password = "";
        }
    }
}
