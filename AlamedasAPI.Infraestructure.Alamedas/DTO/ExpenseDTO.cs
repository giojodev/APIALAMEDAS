using System;

namespace AlamedasAPI.Infraestructure.Alamedas.DTO
{
    public class ExpenseDTO
    {
        public int consecutive { get; set; }
        public int expense { get; set; }
        public DateTime date { get; set; }
        public string concept { get; set; }
        public decimal value { get; set; }
        public int month { get; set; }
        public int year { get; set; }
    }
}
