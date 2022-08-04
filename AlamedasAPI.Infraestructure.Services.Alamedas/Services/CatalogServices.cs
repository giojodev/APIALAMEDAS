using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

using AlamedasAPI.Db.Models.Alamedas.Models;

namespace AlamedasAPI.Infraestructure.Services.Alamedas.Services
{
    public Interface ICatalogServices
    {
        List<Usuario> GetListUser();
    }

    public class CatalogServices:ICatalogServices
    {
        private readonly alamedascontext _context;

        public CatalogServices(alamedascontext context){
            _context = context;
        }

        public List<Usuario> GetListUser(){
            var users = _context.Usuarios.ToList();
        }
    }

    public static class CatalogServicesExtensions
    {
        public static IServiceCollection AddCatalogServices(this IServiceCollection services){
            service.AddTransient<ICatalogServices, CatalogServices>();
            return services;
        }
    }
}
