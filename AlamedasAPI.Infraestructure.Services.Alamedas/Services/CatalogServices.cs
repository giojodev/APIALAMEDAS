using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

using AlamedasAPI.Db.Models.Alamedas.Models;

namespace AlamedasAPI.Infraestructure.Services.Alamedas.Services
{
    public interface ICatalogServices
    {
        List<Usuario> GetListUsers();
    }

    public class CatalogServices: ICatalogServices
    {
        private readonly alamedascontext _context;

        public CatalogServices(alamedascontext context){
            _context = context;
        }

        public List<Usuario> GetListUsers(){
            var users = _context.Usuarios.ToList();
            return users;
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
