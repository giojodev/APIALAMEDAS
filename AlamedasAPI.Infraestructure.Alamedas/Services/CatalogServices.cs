using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Microsoft.Extensions.Logging;
using AlamedasAPI.Infraestructure.Alamedas.DTO;
using AlamedasAPI.Db.Models.Alamedas.Models;
using System.Threading.Tasks;

namespace AlamedasAPI.Infraestructure.Alamedas
{
    public interface ICatalogServices
    {
        List<Usuario> GetListUsers();
        List<Condomino>GetListCondomino();
        List<ProductoGastoCajaChica> GetListProdExpense();
        List<ProductoIngresoCajaChica> GetListProdEntry();
        decimal GetDashboardDebt();
        decimal GetDashboardBill();
        dynamic GetCondominiumDebtDashboard();
        List<DetalleGastoCajachica> GetDetGCC(int IdConsecutive);
        List<DetalleIngresoCajachica> GetDetICC(int IdConsecutive);
        decimal GetIncomesDashboard();
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
        public decimal GetIncomesDashboard()
        {
            var _data=Convert.ToDecimal(_context.Ingresos.Select(a=>a.Total).Sum());
            return _data;
        }
        
        public dynamic GetCondominiumDebtDashboard()
        {
            var Details = _context.Condominos
            .Join(_context.Moras , x=>x.IdCondomino,y=>y.Condomino ,(x,y) => 
            new {Valor = y.Valor,NombreInquilino = x.NombreInquilino})
            .GroupBy(g => g.NombreInquilino) 
            .Select(s => new{ 
                meses = s.Count(),mora = s.Sum(xx => xx.Valor),condomino = s.Key
            })    
            .ToList();

            return Details;
        }
         public List<DetalleGastoCajachica> GetDetGCC(int IdConsecutive){
            
            List<DetalleGastoCajachica> data = null;

            if(IdConsecutive == 0){
                data = _context.DetalleGastoCajachicas.ToList();
            }else{
                data = _context.DetalleGastoCajachicas.Where(x => x.Consecutivo == IdConsecutive).ToList();
            };

            return data;
        }

         public List<DetalleIngresoCajachica> GetDetICC(int IdConsecutive){
            
            List<DetalleIngresoCajachica> data = null;

            if(IdConsecutive == 0){
                data = _context.DetalleIngresoCajachicas.ToList();
            }else{
                data = _context.DetalleIngresoCajachicas.Where(x => x.Consecutivo == IdConsecutive).ToList();
            };

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
