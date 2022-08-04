using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Microsoft.Extensions.Logging;
using AlamedasAPI.Infraestructure.Services.Alamedas.DTO;
using AlamedasAPI.Db.Models.Alamedas.Models;

namespace AlamedasAPI.Infraestructure.Services.Alamedas.Services
{
    public interface ICatalogServices
    {
        List<Usuario> GetListUsers();
        List<Condomino>GetListCondomino(); 
    }

    public class CatalogServices: ICatalogServices
    {
        private readonly alamedascontext _context;
        private readonly ILogger<CatalogServices> _logger;

        public CatalogServices(alamedascontext context,ILogger<CatalogServices> logger){
            _context = context;
            _logger = logger;
        }

        public List<Usuario> GetListUsers(){
            try
            {
                 _logger.LogError(1, "NLog injected into HomeController");
                var users = _context.Usuarios.ToList();
                return users;
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        public List<Condomino> GetListCondomino(){
            // _logger.LogError(1,)
            var condominos=_context.Condominos.ToList();
            return condominos;
        }
    }

    public static class CatalogServicesExtensions
    {
        public static IServiceCollection AddCatalogServices(this IServiceCollection Services){
            Services.AddTransient<ICatalogServices, CatalogServices>();
            return Services;
        }
    }
}
