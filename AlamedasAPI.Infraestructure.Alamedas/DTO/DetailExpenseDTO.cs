using System;

namespace AlamedasAPI.Infraestructure.Alamedas.DTO
{
    public class DetailExpenseDTO
    {
        public int consecutive { get; set; }
        public int idEntity { get; set; }
        public string concept { get; set; }
        public decimal valor { get; set; }
        
    }
}
