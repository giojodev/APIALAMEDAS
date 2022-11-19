using System;
using System.Collections.Generic;

namespace AlamedasAPI.Db.Models.Alamedas.Models
{
    public partial class MovimientosDoc
    {
        public int IdMovimiento { get; set; }
        /// <summary>
        /// CONSECUTIVO O LLAVE DEL DOCUMENTO
        /// </summary>
        public int IdDocumento { get; set; }
        public int IdUsuario { get; set; }
        /// <summary>
        /// MODULO QUE DISPARA EL MOVIMIENTO CAJA CHICA / FACTURACION
        /// </summary>
        public string Modulo { get; set; } = null!;
        /// <summary>
        /// TIPO DE MOVIMIENTO I - INGRESO / G - GASTO
        /// </summary>
        public string Tipo { get; set; } = null!;
        public decimal Total { get; set; }
        public DateTime FechaIngreso { get; set; }
        /// <summary>
        /// ESTADO DEL MOV 
        /// </summary>
        public bool Anulado { get; set; }
        public DateTime? FechaAnulado { get; set; }

        public virtual TblUsuario IdUsuarioNavigation { get; set; } = null!;
    }
}
