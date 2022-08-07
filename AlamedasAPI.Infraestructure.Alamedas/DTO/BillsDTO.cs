using System;

namespace AlamedasAPI.Infraestructure.Alamedas.DTO
{
    public class BillsDTO
    {
        public int consecutive { get; set; }
        public int bills { get; set; }
        public DateTime date { get; set; }
        public string concept { get; set; }
        public double value { get; set; }
        public int month { get; set; }
        public int year { get; set; }
    }
}
