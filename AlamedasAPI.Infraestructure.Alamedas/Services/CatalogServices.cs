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
        List<CondominiumDebtDTO>GetListCondomino();
        List<ProductoGastoCajaChica> GetListProdExpense();
        List<ProductoIngresoCajaChica> GetListProdEntry();
        decimal GetDashboardDebt();
        decimal GetDashboardBill();
        List<CondominiumDebtDTO>GetCondominiumDebtDashboard();
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
        public decimal GetDashboardDebt(){
            var _data= Convert.ToDecimal(_context.Moras.Select(a=>a.Valor).Sum());

            return _data;
        }
        public decimal GetDashboardBill(){
            var _data=Convert.ToDecimal(_context.Gastos.Select(a=>a.Valor).Sum());
            return _data;
        }
        public List<CondominiumDebtDTO>GetCondominiumDebtDashboard()
        {
            //var _data= _context.Moras.Join(_context.Condominos,t=>t.Condomino,g=>g.IdCondomino, (t,g)=>new{g.NombreCompleto,Count(t.Mes)});
            CondominiumDebtDTO condominiumDebtDTO = _context.Moras
            .Join(_context.Condominos , t=>t.Condomino,g=>g.IdCondomino ,(t,g) => new {t,g})
            .GroupBy(x => new{ x.NombreInquilino}) 
            .Select(s => new{ 
            //Meses = s.t.Mes.Count() 
            //vMora = (s.t.Valor ?? 0).Sum())
            //(s.t.Valor??0:s.t.Valor).Sum()
            vMora = s.Sum(x => x.t.Valor)
            ,NombreInquilino = s.g.NombreInquilino
            })    
            .ToList();

            //CondominiumDebtDTO condominiumDebtDTO=new CondominiumDebtDTO();
            // condominiumDebtDTO= _data.ToList();
            return condominiumDebtDTO;
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
