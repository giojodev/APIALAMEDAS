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
        Task<BaseResult> UpdateCondominium(CondominoDTO condominoDTO);
        Task<BaseResult> UpdateDetailIncome(DetailIncomeDTO detailIncomeDTO);
        Task<BaseResult> UpdateDayDebt();
        Task<BaseResult> UpdateBills(BillsDTO billsDTO);
        Task<BaseResult> InsertDetGCC(DetalleGastoCajachica DetalleGastoCajachica);
        BaseResult DeleteDetICC(int IdConsecutive);
        Task<BaseResult> InsertDetICC(DetalleIngresoCajachica model);
        BaseResult DeleteGCC(int IdConsecutive);
        Task<BaseResult> OverridGCC(int IdConsecutive);
        Task<BaseResult> InsertGCC(GastosCajaChica GastosCajaChica);
        
        Task<BaseResult> UpdateIncomes(IncomesDTO incomesDTO);
        Task<BaseResult> UpdateDebt(DebtDTO debtDTO);
        Task<BaseResult> UpdateIncomeType(IncomeTypeDTO incomeTypeDTO);

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
                var det = _context.DetalleGastoCajachicas.Where(d => d.Consecutivo == IdConsecutive).FirstOrDefault();
                _context.DetalleGastoCajachicas.Remove(det);
                _context.SaveChanges();

                return new BaseResult() { Error = false, Message = "Registro eliminado."};
            }
            catch (Exception ex)
            {
                _logger.LogError("Error with DeleteDetGCC", ex);
                return new BaseResult() { Error = true, Message = "Error al eliminar registro."};
            }
        }
        public async Task<BaseResult> UpdateCondominium(CondominoDTO condominoDTO)
        {
            try
            {
                var condomino= await _context.Condominos.FindAsync(condominoDTO.id);
                if(condomino==null)
                    return new BaseResult() { Error = true, Message = "No se encontro el condomino"};
                
                condomino.NombreCompleto=condominoDTO.nombreCompleto;
                condomino.NombreInquilino=condominoDTO.nombreInquilino;

                _context.Entry(condomino).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await _context.SaveChangesAsync();

                return new BaseResult() { Error = false, Message = "Registro Guardado con exito"};

            }
            catch (Exception ex)
            {
                _logger.LogError("Error en el servicio", ex);
                return new BaseResult() { Error = true, Message = "Error en el servicio" };
            }
            
        }
        public async Task<BaseResult> UpdateDetailIncome(DetailIncomeDTO detailIncomeDTO)
        {
            try
            {
                var detailIncome=await _context.DetalleIngresos.FindAsync(detailIncomeDTO.idMora);
                if(detailIncome==null)
                    return new BaseResult() { Error = true, Message = "No se encontro el registro"};
                
                detailIncome.Valor=detailIncomeDTO.total;  
                _context.DetalleIngresos.Add(detailIncome);
                _context.SaveChanges();

                return new BaseResult() { Error = false, Message = "Registro Actualizado con exito", Saved = true };

            }
            catch (Exception ex)
            {
                _logger.LogError("Error en el servicio", ex);
                return new BaseResult() { Error = true, Message = "Error en el servicio"};
            } 
        }
        public async Task<BaseResult> UpdateDayDebt()
        {
            var debt= await _context.Moras.Where(x=>x.Estado=="Pendiente").ToListAsync();

            foreach (var deb in debt)
            {
                deb.DiasVencido=deb.DiasVencido+1;
                _context.Moras.Add(deb);
                _context.SaveChanges();
            }

            return new BaseResult() { Error = false, Message = "Mora Actualizada con exito"};
        }
        public async Task<BaseResult> UpdateBills(BillsDTO billsDTO)
        {
            try
            {
                var debt= await _context.Gastos.Where(x=>x.Consecutivo==billsDTO.consecutive).FirstOrDefaultAsync();
                if(debt==null)
                    return new BaseResult(){Message="Gasto no existe",Error=true};
                
                debt.Usuario=1;
                debt.Gasto1=billsDTO.bills;
                debt.Fecha=billsDTO.date;
                debt.Concepto=billsDTO.concept;
                debt.Valor=Convert.ToDecimal(billsDTO.value);
                debt.Mes=billsDTO.month;
                debt.Anio=billsDTO.year;

                _context.Gastos.Add(debt);
                _context.SaveChanges();

                return new BaseResult(){Message="Registro actualizado",Saved=true,Error=false};
            }
            catch (Exception ex)    
            {
                _logger.LogError("Error en el servicio", ex);
                return new BaseResult() { Error = true, Message = "Error en el servicio", Saved = false };
            }
            

        }
        public async Task<BaseResult> InsertDetGCC(DetalleGastoCajachica model)
        {
            try
            {
                string Message = "Registro ingresado.";
                var data = await _context.DetalleGastoCajachicas.Where(x=>x.Consecutivo == model.Consecutivo).FirstOrDefaultAsync();    

                if(data == null){
                    DetalleGastoCajachica DetalleGastoCajachica = new DetalleGastoCajachica(){
                        Consecutivo = model.Consecutivo,
                        IdProdgasto =  model.IdProdgasto,
                        Concepto = model.Concepto,
                        Total = model.Total,
                        Fecha = model.Fecha,
                        Mes = model.Mes,
                        Anio = model.Anio
                    };

                    await _context.DetalleGastoCajachicas.AddAsync(DetalleGastoCajachica);
                    await _context.SaveChangesAsync();
                }  
                else{
                    
                    data.IdProdgasto = model.IdProdgasto;
                    data.Concepto = model.Concepto;
                    data.Total = model.Total;
                    data.Fecha = model.Fecha;
                    data.Mes = model.Mes;
                    data.Anio = model.Anio;

                    _context.Entry(data).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    await _context.SaveChangesAsync();

                    Message = "Registro actualizado.";
                };

                return new BaseResult() { Error = false, Message = Message};
            }
            catch (Exception ex)
            {
                _logger.LogError("Error with InsertDetGCC", ex);
                return new BaseResult() { Error = true, Message = "Error al ingresar detalle GCC"};
            }
        }
        public BaseResult DeleteDetICC(int IdConsecutive)
        {
            try
            {
                var det = _context.DetalleIngresoCajachicas.Where(d => d.Consecutivo == IdConsecutive).FirstOrDefault();
                _context.DetalleIngresoCajachicas.Remove(det);
                _context.SaveChanges();

                return new BaseResult() { Error = false, Message = "Registro eliminado."};
            }
            catch (Exception ex)
            {
                _logger.LogError("Error with DeleteDetICC", ex);
                return new BaseResult() { Error = true, Message = "Error al eliminar detalle"};
            }
        }
        public async Task<BaseResult> InsertDetICC(DetalleIngresoCajachica model)
        {
            try
            {
                string Message = "Registro ingresado.";
                var data = await _context.DetalleIngresoCajachicas.Where(x=>x.Consecutivo == model.Consecutivo).FirstOrDefaultAsync();    

                if(data == null){
                    DetalleIngresoCajachica DetalleIngresoCajachicas = new DetalleIngresoCajachica(){
                        Consecutivo = model.Consecutivo,
                        IdProdgasto =  model.IdProdgasto,
                        Concepto = model.Concepto,
                        Total = model.Total,
                        Fecha = model.Fecha,
                        Mes = model.Mes,
                        Anio = model.Anio
                    };

                    await _context.DetalleIngresoCajachicas.AddAsync(DetalleIngresoCajachicas);
                    await _context.SaveChangesAsync();
                }  
                else{
                    
                    data.IdProdgasto = model.IdProdgasto;
                    data.Concepto = model.Concepto;
                    data.Total = model.Total;
                    data.Fecha = model.Fecha;
                    data.Mes = model.Mes;
                    data.Anio = model.Anio;

                    _context.Entry(data).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    await _context.SaveChangesAsync();

                    Message = "Registro actualizado.";
                };

                return new BaseResult() { Error = false, Message = Message};
            }
            catch (Exception ex)
            {
                _logger.LogError("Error with InsertDetGCC", ex);
                return new BaseResult() { Error = true, Message = "Error al ingresar detalle ICC"};
            }
        }
        public BaseResult DeleteGCC(int IdConsecutive)
        {
            try
            {
                var det = _context.GastosCajaChicas.Where(d => d.Consecutivo == IdConsecutive).FirstOrDefault();
                _context.GastosCajaChicas.Remove(det);
                _context.SaveChanges();

                return new BaseResult() { Error = false, Message = "Registro eliminado."};
            }
            catch (Exception ex)
            {
                _logger.LogError("Error with DeleteGCC", ex);
                return new BaseResult() { Error = true, Message = "Error al eliminar detalle."};
            }
        }
        public async Task<BaseResult> OverridGCC(int IdConsecutive)
        {
            try
            {
                var det = _context.GastosCajaChicas.Where(d => d.Consecutivo == IdConsecutive).FirstOrDefault();
                if(det == null)
                    return new BaseResult() { Error = true, Message = "Error el GCC no existe."};

                det.Anulado = false;
                _context.Entry(det).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await _context.SaveChangesAsync();

                return new BaseResult(){Message="Registro anulado.",Error=false};
            }
            catch (Exception ex)
            {
                _logger.LogError("Error with OverridGCC", ex);
                return new BaseResult() { Error = true, Message = "Error al anular GCC."};
            }
        }
        // Task<BaseResult> UpdateIncomes(IncomesDTO incomesDTO);
        // Task<BaseResult> UpdateDebt(DebtDTO debtDTO);
        // Task<BaseResult> UpdateIncomeType(IncomeTypeDTO incomeTypeDTO);
        public async Task<BaseResult> UpdateIncomeType(IncomeTypeDTO incomeTypeDTO)
        {
            try
            {
                var data= await _context.TipoIngresos.Where(x=>x.IdIngreso==incomeTypeDTO.idIncome).FirstOrDefaultAsync();

                if(data==null)
                    return new BaseResult(){Message="No se encontro el registro",Saved=false,Error=true};
                
                data.NombreIngreso=incomeTypeDTO.nombreIngreso;
                data.Activo=incomeTypeDTO.active;

                _context.Entry(data).State=Microsoft.EntityFrameworkCore.EntityState.Modified;
                await _context.SaveChangesAsync();

                return new BaseResult(){Message="Registro actualizado",Error=false,Saved=true};
            }
            catch (Exception ex)
            {
                _logger.LogError("Error with OverridGCC", ex);
                return new BaseResult() { Error = true, Message = "Error al anular GCC."};
            }
        }
        public async Task<BaseResult> UpdateDebt(DebtDTO debtDTO)
        {
            try
            {
                var data=await _context.Moras.Where(x=>x.IdMora==debtDTO.IdMora).FirstOrDefaultAsync();

                if(data==null)
                    return new BaseResult(){Message="No se encontro el registro",Saved=false,Error=true};
                
                data.Fecha=debtDTO.Fecha;
                data.Condomino=debtDTO.Condomino;
                data.Concepto=debtDTO.Concepto;
                data.Valor=debtDTO.Valor;
                data.Estado=debtDTO.Estado;
                data.DiasVencido=debtDTO.DiasVencido;
                data.Mes=debtDTO.Mes;
                data.Anio=debtDTO.Anio;

                _context.Entry(data).State=Microsoft.EntityFrameworkCore.EntityState.Modified;
                await _context.SaveChangesAsync();

                return new BaseResult(){Message="Registro actualizado",Error=false,Saved=true};

            }
            catch (Exception ex)
            {
                _logger.LogError("Error with OverridGCC", ex);
                return new BaseResult() { Error = true, Message = "Error al anular GCC."};
            }
        }
        public async Task<BaseResult> UpdateIncomes(IncomesDTO incomesDTO)
        {
            try
            {
                var data = await  _context.Ingresos.Where(x=>x.Consecutivo==incomesDTO.consecutive).FirstOrDefaultAsync();
                if(data==null)
                    return new BaseResult(){Message="No se encontro el registro",Saved=false,Error=true};
                
                data.Fecha=incomesDTO.date;
                data.Ingreso1=incomesDTO.incometype;
                data.Usuario=incomesDTO.user;
                data.Concepto=incomesDTO.concept;
                data.Total=((double)incomesDTO.total);
                data.Mes=incomesDTO.month;
                data.Anio=incomesDTO.year;
                data.NombreInquilino=incomesDTO.resident;

                _context.Entry(data).State=Microsoft.EntityFrameworkCore.EntityState.Modified;
                await _context.SaveChangesAsync();

                return new BaseResult(){Message="Registro actualizado",Error=false,Saved=true};
                
            }
            catch (Exception ex)
            {
                _logger.LogError("Error with OverridGCC", ex);
                return new BaseResult() { Error = true, Message = "Error al anular GCC."};
            }
        }
        public async Task<BaseResult> InsertGCC(GastosCajaChica model)
        {
            try
            {
                string Message = "Registro ingresado.";
                var data = await _context.GastosCajaChicas.Where(x=>x.Consecutivo == model.Consecutivo).FirstOrDefaultAsync();    

                if(data == null){
                    GastosCajaChica GastosCajaChicas = new GastosCajaChica(){
                        Consecutivo = model.Consecutivo,
                        IdUsuario =  1,
                        TipoGastoCchica = model.TipoGastoCchica,
                        Fecha = model.Fecha,
                        Concepto = model.Concepto,
                        Total = model.Total,
                        Mes = model.Mes,
                        Anio = model.Anio,
                        Anulado = model.Anulado
                    };

                    await _context.GastosCajaChicas.AddAsync(GastosCajaChicas);
                    await _context.SaveChangesAsync();
                }  
                else{
                    
                    data.TipoGastoCchica = model.TipoGastoCchica;
                    data.Fecha = model.Fecha;
                    data.Total = model.Total;
                    data.Fecha = model.Fecha;
                    data.Mes = model.Mes;
                    data.Anio = model.Anio;

                    _context.Entry(data).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    await _context.SaveChangesAsync();

                    Message = "Registro actualizado";
                };

                return new BaseResult() { Error = false, Message = Message};
            }
            catch (Exception ex)
            {
                _logger.LogError("Error with InsertGCC", ex);
                return new BaseResult() { Error = true, Message = "Error al ingresar GCC"};
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
