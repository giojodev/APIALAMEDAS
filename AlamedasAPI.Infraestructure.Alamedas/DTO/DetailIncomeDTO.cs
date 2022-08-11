using System;

namespace AlamedasAPI.Infraestructure.Alamedas.DTO
{
    public class DetailIncomeDTO
    {
        public int Consecutive { get; set; }
        public int idMora { get; set; }
        public double total { get; set; }
        public string concept { get; set; }
        public string month { get; set; }
        public string year { get; set; }
        public int daysExpired { get; set; }
    }
}
