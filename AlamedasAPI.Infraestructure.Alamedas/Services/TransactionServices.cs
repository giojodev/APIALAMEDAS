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
        int GetConsecutiveGCC();
        BaseResult DeleteDetGCC(int IdConsecutive);
        Task<BaseResult> UpdateCondominium(CondominoDTO condominoDTO);
        Task<BaseResult> UpdateDetailIncome(DetailIncomeDTO detailIncomeDTO);

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

        public async Task<BaseResult> UpdateCondominium(CondominoDTO condominoDTO)
        {
            try
            {
                var condomino= await _context.Condominos.FindAsync(condominoDTO.id);
                if(condomino==null)
                    return new BaseResult() { Error = true, Message = "No se encontro el condomino", Saved = false };
                
                condomino.NombreCompleto=condominoDTO.nombreCompleto;
                condomino.NombreInquilino=condominoDTO.nombreInquilino;

                _context.Condominos.Add(condomino);
                _context.SaveChanges();

                return new BaseResult() { Error = false, Message = "Registro Guardado con exito", Saved = true };

            }
            catch (Exception ex)
            {
                _logger.LogError("Error en el servicio", ex);
                return new BaseResult() { Error = true, Message = "Error en el servicio", Saved = false };
            }
            
        }
        public async Task<BaseResult> UpdateDetailIncome(DetailIncomeDTO detailIncomeDTO)
        {
            try
            {
                var detailIncome=await _context.DetalleIngresos.FindAsync(detailIncomeDTO.idMora);
                if(detailIncome==null)
                    return new BaseResult() { Error = true, Message = "No se encontro el registro", Saved = false };
                
                detailIncome.Valor=detailIncomeDTO.total;  
                _context.DetalleIngresos.Add(detailIncome);
                _context.SaveChanges();

                return new BaseResult() { Error = false, Message = "Registro Actualizado con exito", Saved = true };

            }
            catch (Exception ex)
            {
                _logger.LogError("Error en el servicio", ex);
                return new BaseResult() { Error = true, Message = "Error en el servicio", Saved = false };
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
