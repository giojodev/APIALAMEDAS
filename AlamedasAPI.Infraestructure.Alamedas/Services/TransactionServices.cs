using System;
using Microsoft.Extensions.Logging;
using AlamedasAPI.Infraestructure.Alamedas.DTO;
using AlamedasAPI.Db.Models.Alamedas.Models;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace AlamedasAPI.Infraestructure.Alamedas
{
    public interface ITransactionServices
    {
        Task<BaseResult> ProdExpense_PerryCash();
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

        public async Task<BaseResult> ProdExpense_PerryCash()
        {
            try
            {
                return new BaseResult() { Error = false, Message = "Servico conectado", Saved = true };
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
