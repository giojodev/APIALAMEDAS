using System;
using Microsoft.Extensions.Logging;
using AlamedasAPI.Infraestructure.Alamedas.DTO;
using AlamedasAPI.Db.Models.Alamedas.Models;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace AlamedasAPI.Infraestructure.Alamedas
{
    public interface ISecurityServices
    {
        Task<BaseResult> Login();
    }

    public class SecurityServices : ISecurityServices
    {
        private readonly alamedascontext _context;
        private readonly ILogger<ISecurityServices> _logger;

        public SecurityServices(alamedascontext context, ILogger<ISecurityServices> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<BaseResult> Login()
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

    public static class SecurityServicesExtensions
    {
        public static IServiceCollection AddSecurityServices(this IServiceCollection Services)
        {
            Services.AddTransient<ISecurityServices, SecurityServices>();
            return Services;
        }
     }
}