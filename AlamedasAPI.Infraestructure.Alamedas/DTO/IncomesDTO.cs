using System;

namespace AlamedasAPI.Infraestructure.Alamedas.DTO
{
    public class IncomesDTO
    {
        public int consecutive { get; set; }
        public int user { get; set; }
        public int incometype { get; set; }
        public DateTime date { get; set; }
        public string concept { get; set; }
        public decimal total { get; set; }
        public int month { get; set; }
        public int year { get; set; }
        public string resident { get; set; }
    }
}
