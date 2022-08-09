using System;
using AlamedasAPI.Db.Models.Alamedas.Models;

namespace AlamedasAPI.Infraestructure.Alamedas.DTO
{
    public class DebtDTO
    {
        public int IdMora { get; set; }
        public DateTime Fecha { get; set; }
        public int Condomino { get; set; }
        public string Concepto { get; set; }
        public decimal? Valor { get; set; }
        public string Estado { get; set; }
        public int? DiasVencido { get; set; }
        public string Mes { get; set; }
        public string Anio { get; set; }

        // public virtual Condomino CondominoNavigation { get; set; } = null!;
    }
}
