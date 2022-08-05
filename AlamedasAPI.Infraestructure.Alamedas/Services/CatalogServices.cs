using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Microsoft.Extensions.Logging;
using AlamedasAPI.Infraestructure.Alamedas.DTO;
using AlamedasAPI.Db.Models.Alamedas.Models;

namespace AlamedasAPI.Infraestructure.Alamedas
{
    public interface ICatalogServices
    {
        List<Usuario> GetListUsers();
        List<Condomino>GetListCondomino();
        List<ProductoGastoCajaChica> GetListProdExpense();
        List<ProductoIngresoCajaChica> GetListProdEntry();
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
            var users = _context.Usuarios.ToList();
            return users;
        }

        public List<Condomino> GetListCondomino(){
            var condominos=_context.Condominos.ToList();
            return condominos;
        }

        public List<ProductoGastoCajaChica> GetListProdExpense()
        {
            var data = _context.ProductoGastoCajaChicas.ToList();
            return data;
        }

        public List<ProductoIngresoCajaChica> GetListProdEntry()
        {
            var data = _context.ProductoIngresoCajaChicas.ToList();
            return data;
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
