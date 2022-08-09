using System;

namespace AlamedasAPI.Infraestructure.Alamedas.DTO
{
    public class BaseResult
    {
        public string Message { get; set; }
        public bool Error { get; set; }
        public bool Saved {get;set;}
    }
}
