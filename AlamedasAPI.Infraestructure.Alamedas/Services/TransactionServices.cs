using System;
using Microsoft.Extensions.Logging;
using AlamedasAPI.Infraestructure.Alamedas.DTO;
using AlamedasAPI.Db.Models.Alamedas.Models;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AlamedasAPI.Infraestructure.Alamedas
{
    public interface ITransactionServices
    {
        int GetConsecutiveICC();
        int GetConsecutiveGCC();
        BaseResult DeleteDetGCC(int IdConsecutive);
    }

    public class TransactionServices : ITransactionServices
    {
        private readonly alamedascontext _context;
        private readonly ILogger<ITransactionServices> _logger;

        public TransactionServices(alamedascontext context, ILogger<ITransactionServices> logger)
        {
            _context = context;
            _logger = logger;
        }

        public int GetConsecutiveICC()
        {
            try
            {
                int number = 0;
                var data = _context.TblIngresosCajaChicas.Select(grp =>new {number = grp.Consecutivo})
                .OrderByDescending(x => x.number).FirstOrDefault();
                if(data != null)
                    number = data.number;

                return number;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error with GetConsecutiveICC", ex);
                return -1;
            }
        }

          public int GetConsecutiveGCC()
        {
            try
            {
                int number = 0;
                var data = _context.GastosCajaChicas.Select(grp =>new {number = grp.Consecutivo})
                .OrderByDescending(x => x.number).FirstOrDefault();
                if(data != null)
                    number = data.number;
                    
                return number;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error with GetConsecutiveGCC", ex);
                return -1;
            }
        }

        public BaseResult DeleteDetGCC(int IdConsecutive)
        {
            try
            {
                var det = new DetalleGastoCajachica { Consecutivo = IdConsecutive };
                _context.Entry(det).State = EntityState.Deleted;
                _context.SaveChanges();

                return new BaseResult() { Error = false, Message = "Detalle eliminado.", Saved = true };
            }
            catch (Exception ex)
            {
                _logger.LogError("Error with DeleteDetGCC", ex);
                return new BaseResult() { Error = true, Message = "Error al eliminar detalle", Saved = false };
            }
        }


    }

    public static class TransactionsServicesExtensions
    {
        public static IServiceCollection AddTransactionServices(this IServiceCollection Services)
        {
            Services.AddTransient<ITransactionServices, TransactionServices>();
            return Services;
        }
     }
}
