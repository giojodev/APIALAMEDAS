using System;

namespace AlamedasAPI.Infraestructure.Services.Alamedas.DTO
{
    public class BaseResult
    {
        public string Message { get; set; }
        public bool Saved { get; set; }
        public bool Error { get; set; }
        public Array Data { get; set; }
    }
}
