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
        Task<BaseResult> InsertGCC(TblGastosCajaChica TblGastosCajaChica);
        Task<BaseResult> UpdateIncomes(IncomesDTO incomesDTO);
        Task<BaseResult> UpdateExpense(ExpenseDTO expenseDTO);
        Task<BaseResult> UpdateDebt(DebtDTO debtDTO);
        Task<BaseResult> UpdateIncomeType(IncomeTypeDTO incomeTypeDTO);
        BaseResult DeleteICC(int IdConsecutive);
        Task<BaseResult> OverridICC(int IdConsecutive);
        Task<BaseResult> InsertICC(TblIngresosCajaChica model);
        BaseResult DeleteTGCC(int IdTGCC);
        Task<BaseResult> InsertTGCC(TipoGastoCajaChica model);
        BaseResult DeleteTICC(int IdTICC);
        Task<BaseResult> UpdateTGCC(TipoGastoCajaChica model);
        Task<BaseResult> InsertTICC(TipoIngresoCajaChica model);
        Task<BaseResult>UpdateTICC(TipoIngresoCajaChica model);
        Task<BaseResult> DeleteCondominium(int IdCondomino);
        Task<BaseResult> DeleteExpenses(int idExpense);
        Task<BaseResult> DeleteIncome(int consecutive);
        Task<BaseResult> DeleteDebt(int idDebt);
        Task<BaseResult> DeleteTypeExpense(int id);
        Task<BaseResult> DeleteTypeIncome(int id);
        Task<BaseResult> InsertDetailIncome(DetailIncomeDTO detailIncomeDTO);
        Task<BaseResult> InsertCondominum(CondominoDTO condominoDTO);
        Task<BaseResult> InsertDetailExpense(DetailExpenseDTO detailExpenseDTO);
        Task<BaseResult> InsertExpense(ExpenseDTO expenseDTO);
        Task<BaseResult> InsertIncome(IncomesDTO incomesDTO);
        Task<BaseResult> InsertDebt(DebtDTO debtDTO);
        Task<BaseResult> InsertTypeExpense(ExpenseTypeDTO expenseTypeDTO);
        Task<BaseResult> InsertTypeIncome(IncomeTypeDTO incomeTypeDTO);
        Task<BaseResult> CancelMovDoc(int IdMovimiento);
        Task<BaseResult> InsertMovDoc(MovimientosDoc model);
        Task<BaseResult> InsertProdExpense(ProductoGastoCajaChica model);
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
                var data = _context.TblGastosCajaChicas.Select(grp =>new {number = grp.Consecutivo})
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
                var condomino= await _context.Condominos.FindAsync(condominoDTO.IdCondomino);
                if(condomino==null)
                    return new BaseResult() { Error = true, Message = "No se encontro el condomino"};
                
                condomino.NombreCompleto=condominoDTO.nombreCompleto;
                condomino.NombreInquilino=condominoDTO.nombreInquilino;
                condomino.Telefono=condominoDTO.telefono;
                condomino.Correo=condominoDTO.correo;
                condomino.Activo=condominoDTO.activo;

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

                return new BaseResult() { Error = false, Message = "Registro Actualizado con exito"};

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

                return new BaseResult(){Message="Registro actualizado",Error=false};
            }
            catch (Exception ex)    
            {
                _logger.LogError("Error en el servicio", ex);
                return new BaseResult() { Error = true, Message = "Error en el servicio"};
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
               // var det = _context.GastosCajaChicas.Where(d => d.Consecutivo == IdConsecutive).FirstOrDefault();
               // _context.GastosCajaChicas.Remove(det);
               // _context.SaveChanges();

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
                /*var det = _context.GastosCajaChicas.Where(d => d.Consecutivo == IdConsecutive).FirstOrDefault();
                if(det == null)
                    return new BaseResult() { Error = true, Message = "Error el GCC no existe."};

                det.Anulado = false;
                _context.Entry(det).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await _context.SaveChangesAsync();*/

                return new BaseResult(){Message="Registro anulado.",Error=false};
            }
            catch (Exception ex)
            {
                _logger.LogError("Error with OverridGCC", ex);
                return new BaseResult() { Error = true, Message = "Error al anular GCC."};
            }
        }
        public async Task<BaseResult> UpdateIncomeType(IncomeTypeDTO incomeTypeDTO)
        {
            try
            {
                var data= await _context.TipoIngresos.Where(x=>x.IdIngreso==incomeTypeDTO.idIncome).FirstOrDefaultAsync();

                if(data==null)
                    return new BaseResult(){Message="No se encontro el registro",Error=true};
                
                data.NombreIngreso=incomeTypeDTO.nombreIngreso;
                data.Activo=incomeTypeDTO.active;

                _context.Entry(data).State=Microsoft.EntityFrameworkCore.EntityState.Modified;
                await _context.SaveChangesAsync();

                return new BaseResult(){Message="Registro actualizado",Error=false};
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
                    return new BaseResult(){Message="No se encontro el registro",Error=true};
                
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

                return new BaseResult(){Message="Registro actualizado",Error=false};

            }
            catch (Exception ex)
            {
                _logger.LogError("Error with OverridGCC", ex);
                return new BaseResult() { Error = true, Message = "Error al anular GCC."};
            }
        }
        public async Task<BaseResult> UpdateExpense(ExpenseDTO expenseDTO)
        {
            try{
               var data=await _context.Gastos.Where(x=>x.Consecutivo==expenseDTO.consecutive).FirstOrDefaultAsync();
               if(data==null)
                    return new BaseResult(){Message="No se encontro el registro",Error=true};
                data.Anio=expenseDTO.year;
                data.Concepto=expenseDTO.concept;
                data.Fecha=expenseDTO.date;
                data.Gasto1=expenseDTO.expense;
                data.Mes=expenseDTO.month;
                data.Valor=expenseDTO.value;
                data.Usuario=1;
                _context.Entry(data).State=Microsoft.EntityFrameworkCore.EntityState.Modified;
                await _context.SaveChangesAsync();
                return new BaseResult(){Message="Registro actualizado",Error=false};
            }
            catch(Exception ex)
            {
                 _logger.LogError("Error with UpdateExpense", ex);
                return new BaseResult() { Error = true, Message = "Error al actualizar gasto."};
            }
            
        }
        public async Task<BaseResult> UpdateIncomes(IncomesDTO incomesDTO)
        {
            try
            {
                var data = await  _context.Ingresos.Where(x=>x.Consecutivo==incomesDTO.consecutive).FirstOrDefaultAsync();
                if(data==null)
                    return new BaseResult(){Message="No se encontro el registro",Error=true};
                
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

                return new BaseResult(){Message="Registro actualizado",Error=false};
                
            }
            catch (Exception ex)
            {
                _logger.LogError("Error with OverridGCC", ex);
                return new BaseResult() { Error = true, Message = "Error al anular GCC."};
            }
        }

        public async Task<BaseResult> InsertGCC(TblGastosCajaChica model)
        {
            try
            {
                string Message = "Registro ingresado.";
                var data = await _context.TblGastosCajaChicas.Where(x=>x.Consecutivo == model.Consecutivo).FirstOrDefaultAsync();    

                if(data == null){

                    TblGastosCajaChica TblGastosCajaChicas = new TblGastosCajaChica(){
                        //Consecutivo = model.Consecutivo,
                        IdUsuario =  model.IdUsuario,
                        TipoGastoCchica = model.TipoGastoCchica,
                        Fecha = model.Fecha,
                        Concepto = model.Concepto,
                        Total = model.Total,
                        Mes = model.Mes,
                        Anio = model.Anio,
                        Anulado = false
                    };

                    await _context.TblGastosCajaChicas.AddAsync(TblGastosCajaChicas);
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
        public BaseResult DeleteICC(int IdConsecutive)
        {
            try
            {
                var det = _context.TblIngresosCajaChicas.Where(d => d.Consecutivo == IdConsecutive).FirstOrDefault();
                _context.TblIngresosCajaChicas.Remove(det);
                _context.SaveChanges();

                return new BaseResult() { Error = false, Message = "Registro eliminado."};
            }
            catch (Exception ex)
            {
                _logger.LogError("Error with DeleteICC", ex);
                return new BaseResult() { Error = true, Message = "Error al eliminar."};
            }
        }
        public async Task<BaseResult> DeleteCondominium(int IdCondomino)
        {
            try
            {
                var condo= await _context.Condominos.Where(x=>x.IdCondomino==IdCondomino).FirstOrDefaultAsync();

                if(condo==null)
                    return new BaseResult(){Message="Registro no encontrado", Error=true };
                
                _context.Condominos.Remove(condo);
                await _context.SaveChangesAsync();
                
                return new BaseResult(){Message="Registro eliminado",Error=false};
            }
            catch (Exception ex)
            {
                _logger.LogError("Error with DeleteCondominium", ex);
                return new BaseResult() { Error = true, Message = "Error al eliminar."};
            }
            
        }
        public async Task<BaseResult> DeleteExpenses(int idExpense)
        {
            try
            {
                var expense=await _context.Gastos.Where(x=>x.Consecutivo==idExpense).FirstOrDefaultAsync();
                if(expense==null)
                    return new BaseResult(){Message="No se encontro el registro",Error=true};
                
                _context.Gastos.Remove(expense);
                await _context.SaveChangesAsync();
                return new BaseResult(){Message="Registro eliminado",Error=false};
            }
            catch (Exception ex)
            {
                _logger.LogError("Error with DeleteExpenses", ex);
                return new BaseResult() { Error = true, Message = "Error al eliminar."};
            }
            
        }
        public async Task<BaseResult> DeleteIncome(int consecutive)
        {
            try
            {
                var income=await _context.Ingresos.Where(x=>x.Consecutivo==consecutive).FirstOrDefaultAsync();
                if(income==null)
                    return new BaseResult(){Message="No se encontro el registro",Error=true};
                
                _context.Ingresos.Remove(income);
                await _context.SaveChangesAsync();
                
                var detailIncome= await _context.DetalleIngresos.Where(x=>x.Consecutivo==consecutive).FirstOrDefaultAsync();

                if(detailIncome==null)
                    return new BaseResult(){Message="No se encontro el registro",Error=true};
                
                _context.DetalleIngresos.Remove(detailIncome);
                await _context.SaveChangesAsync();


                return new BaseResult(){Message="Registro eliminado",Error=false};
            }
            catch (Exception ex)
            {
                _logger.LogError("Error with DeleteIncome", ex);
                return new BaseResult() { Error = true, Message = "Error al eliminar."};
            }
            
        }
        public async Task<BaseResult> DeleteDebt(int idDebt)
        {
            try
            {
                var debt=await _context.Moras.Where(x=>x.IdMora==idDebt).FirstOrDefaultAsync();
                if(debt==null)
                    return new BaseResult(){Message="No se encontro el registro",Error=true};
                _context.Moras.Remove(debt);
                _context.SaveChanges();
                return new BaseResult(){Message="Registro eliminado",Error=false};
            }
            catch (Exception ex)
            {
                _logger.LogError("Error with DeleteDebt", ex);
                return new BaseResult() { Error = true, Message = "Error al eliminar."};
            }
        }
        public async Task<BaseResult> DeleteTypeExpense(int id)
        {
            try
            {
                var typeExpense=await _context.TipoGastos.Where(x=>x.IdGasto==id).FirstOrDefaultAsync();
                if(typeExpense==null)
                    return new BaseResult(){Message="No se encontro el registro",Error=true};
                
                _context.TipoGastos.Remove(typeExpense);
                _context.SaveChanges();
                return new BaseResult(){Message="Registro eliminado",Error=false};
            }
            catch (Exception ex)
            {
                _logger.LogError("Error with DeleteTypeExpense", ex);
                return new BaseResult() { Error = true, Message = "Error al eliminar."};
            }
        }
        public async Task<BaseResult> DeleteTypeIncome(int id)
        {
            try
            {
                var typeIncome=await _context.TipoIngresos.Where(x=>x.IdIngreso==id).FirstOrDefaultAsync();
                if(typeIncome==null)
                    return new BaseResult(){Message="No se encontro el registro",Error=true};
                
                _context.TipoIngresos.Remove(typeIncome);
                _context.SaveChanges();
                return new BaseResult(){Message="Registro eliminado",Error=false};
            }
            catch (Exception ex)
            {
                _logger.LogError("Error with DeleteTypeIncome", ex);
                return new BaseResult() { Error = true, Message = "Error al eliminar."};
            }
        }
        public async Task<BaseResult> InsertDetailIncome(DetailIncomeDTO detailIncomeDTO)
        {
            try
            {
                var detailIncome= await _context.DetalleIngresos.Where(x=>x.Consecutivo==detailIncomeDTO.Consecutive).FirstOrDefaultAsync();
                if(detailIncome==null)
                {
                    detailIncome.Anio=detailIncomeDTO.year;
                    detailIncome.Mes=detailIncomeDTO.month;
                    detailIncome.Concepto=detailIncomeDTO.concept;
                    detailIncome.DiasVencido=detailIncomeDTO.daysExpired;
                    detailIncome.Valor=detailIncomeDTO.total;
                    detailIncome.IdMora=detailIncomeDTO.idMora;
                    detailIncome.Consecutivo=detailIncomeDTO.Consecutive;

                    _context.DetalleIngresos.Add(detailIncome);
                    _context.SaveChanges();

                    return new BaseResult(){Message="Se ha insertado el registro",Error=false};
                }
                else
                {
                    detailIncome.Anio=detailIncomeDTO.year;
                    detailIncome.Mes=detailIncomeDTO.month;
                    detailIncome.Concepto=detailIncomeDTO.concept;
                    detailIncome.DiasVencido=detailIncomeDTO.daysExpired;
                    detailIncome.Valor=detailIncomeDTO.total;
                    detailIncome.IdMora=detailIncomeDTO.idMora;
                    detailIncome.Consecutivo=detailIncomeDTO.Consecutive;

                    _context.Entry(detailIncome).State=Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _context.SaveChanges();
                     return new BaseResult(){Message="Se ha actualizado el registro",Error=false};
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error with InsertDetailIncome", ex);
                return new BaseResult() { Error = true, Message = "Error al eliminar."};
            }
        }
        public async Task<BaseResult> InsertCondominum(CondominoDTO condominoDTO)
        {
            try
            {
                var condo= await _context.Condominos.Where(x=>x.IdCondomino==condominoDTO.IdCondomino).FirstOrDefaultAsync();
                if( condo == null )
                {
                    Condomino data = new Condomino();
                    data.Activo=condominoDTO.activo;
                    data.Correo=condominoDTO.correo;
                    data.IdCondomino=condominoDTO.IdCondomino;
                    data.NombreCompleto=condominoDTO.nombreCompleto;
                    data.NombreInquilino=condominoDTO.nombreInquilino;
                    data.Telefono=condominoDTO.telefono;

                    _context.Condominos.Add(data);
                    _context.SaveChanges();
                    return new BaseResult(){Message="Registro creado con exito.",Error=false};

                }   
                else
                    return new BaseResult(){Message="Ya existe un condomino en el sistema con este numero de casa.",Error=true};
            }
            catch (Exception ex)
            {
                _logger.LogError("Error with InsertCondominum", ex);
                return new BaseResult() { Error = true, Message = "Error al crear el condomino."};
            }
        }
        public async Task<BaseResult> InsertDetailExpense(DetailExpenseDTO detailExpenseDTO)
        {
            try
            {
                var detail=await _context.DetalleGastos.Where(x=>x.Consecutivo==detailExpenseDTO.consecutive && x.IdEntity==detailExpenseDTO.idEntity).FirstOrDefaultAsync();

                if(detail==null)
                {
                    detail.Concepto=detailExpenseDTO.concept;
                    detail.Consecutivo=detailExpenseDTO.consecutive;
                    detail.IdEntity=detailExpenseDTO.idEntity;
                    detail.Valor=((double)detailExpenseDTO.valor);

                    _context.DetalleGastos.Add(detail);
                    _context.SaveChanges();
                    return new BaseResult(){Message="Registro creado con exito",Error=false};
                }
                else
                {
                    detail.Concepto=detailExpenseDTO.concept;
                    detail.Consecutivo=detailExpenseDTO.consecutive;
                    detail.IdEntity=detailExpenseDTO.idEntity;
                    detail.Valor=((double)detailExpenseDTO.valor);

                    _context.Entry(detail).State=Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _context.SaveChanges();
                    return new BaseResult(){Message="Registro actualizado con exito",Error=false};
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error with InsertDetailExpense", ex);
                return new BaseResult() { Error = true, Message = "Error al eliminar."};
            }
        }

        public async Task<BaseResult> InsertExpense(ExpenseDTO expenseDTO)
        {
            try
            {
                var expense=await _context.Gastos.Where(x=>x.Consecutivo==expenseDTO.consecutive).FirstOrDefaultAsync();

                if(expense==null)
                {
                    expense.Consecutivo=expenseDTO.consecutive;
                    expense.Usuario=1;
                    expense.Gasto1=expenseDTO.expense;
                    expense.Fecha=expenseDTO.date;
                    expense.Concepto=expenseDTO.concept;
                    expense.Valor=expenseDTO.value;
                    expense.Mes=expenseDTO.month;
                    expense.Anio=expenseDTO.year;

                    _context.Gastos.Add(expense);
                    _context.SaveChanges();
                    return new BaseResult(){Message="Registro creado con exito",Error=false};
                }
                else
                {
                    expense.Consecutivo=expenseDTO.consecutive;
                    expense.Usuario=1;
                    expense.Gasto1=expenseDTO.expense;
                    expense.Fecha=expenseDTO.date;
                    expense.Concepto=expenseDTO.concept;
                    expense.Valor=expenseDTO.value;
                    expense.Mes=expenseDTO.month;
                    expense.Anio=expenseDTO.year;
                    _context.Entry(expense).State=Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _context.SaveChanges();
                    return new BaseResult(){Message="Registro actualizado con exito",Error=false};
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error with InsertExpense", ex);
                return new BaseResult() { Error = true, Message = "Error al anular."};
            }
        }
        public async Task<BaseResult> InsertIncome(IncomesDTO incomesDTO)
        {
            try
            {
                var ingreso= await _context.Ingresos.Where(x=>x.Consecutivo==incomesDTO.consecutive).FirstOrDefaultAsync();

                if(ingreso==null)
                {
                    ingreso.Consecutivo=incomesDTO.consecutive;
                    ingreso.Usuario=1;
                    ingreso.NombreInquilino=incomesDTO.resident;
                    ingreso.Ingreso1=incomesDTO.incometype;
                    ingreso.Fecha=incomesDTO.date;
                    ingreso.Concepto=incomesDTO.concept;
                    ingreso.Total=((double)incomesDTO.total);
                    ingreso.Mes=incomesDTO.month;
                    ingreso.Anio=incomesDTO.year;
                    ingreso.Anulado=false;

                    _context.Ingresos.Add(ingreso);
                    _context.SaveChanges();
                    return new BaseResult(){Message="Registro creado con exito",Error=false};
                }
                else
                {
                    ingreso.Consecutivo=incomesDTO.consecutive;
                    ingreso.Usuario=1;
                    ingreso.NombreInquilino=incomesDTO.resident;
                    ingreso.Ingreso1=incomesDTO.incometype;
                    ingreso.Fecha=incomesDTO.date;
                    ingreso.Concepto=incomesDTO.concept;
                    ingreso.Total=((double)incomesDTO.total);
                    ingreso.Mes=incomesDTO.month;
                    ingreso.Anio=incomesDTO.year;
                    ingreso.Anulado=false;
                    _context.Entry(ingreso).State=Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _context.SaveChanges();
                    return new BaseResult(){Message="Registro actualizado con exito",Error=false};
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error with InsertIncome", ex);
                return new BaseResult() { Error = true, Message = "Error al anular."};
            }
        }
        public async Task<BaseResult> InsertDebt(DebtDTO debtDTO)
        {
            try
            {
                var mora=await _context.Moras.Where(x=>x.IdMora==debtDTO.IdMora).FirstOrDefaultAsync();

                if(mora==null)
                {
                    mora.IdMora=debtDTO.IdMora;
                    mora.Anio=debtDTO.Anio;
                    mora.Concepto=debtDTO.Concepto;
                    mora.Condomino=debtDTO.Condomino;
                    mora.DiasVencido=debtDTO.DiasVencido;
                    mora.Estado=debtDTO.Estado;
                    mora.Fecha=debtDTO.Fecha;
                    mora.Valor=debtDTO.Valor;
                    mora.Mes=debtDTO.Mes;

                    _context.Moras.Add(mora);
                    _context.SaveChanges();

                    return new BaseResult(){Message="Registro creado con exito",Error=false};
                }
                else
                {
                    mora.IdMora=debtDTO.IdMora;
                    mora.Anio=debtDTO.Anio;
                    mora.Concepto=debtDTO.Concepto;
                    mora.Condomino=debtDTO.Condomino;
                    mora.DiasVencido=debtDTO.DiasVencido;
                    mora.Estado=debtDTO.Estado;
                    mora.Fecha=debtDTO.Fecha;
                    mora.Valor=debtDTO.Valor;
                    mora.Mes=debtDTO.Mes;

                    _context.Entry(mora).State=Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _context.SaveChanges();
                    return new BaseResult(){Message="Registro actualizado con exito",Error=false};
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error with InsertDebt", ex);
                return new BaseResult() { Error = true, Message = "Error al anular."};
            }
        }
        public async Task<BaseResult> InsertTypeExpense(ExpenseTypeDTO expenseTypeDTO)
        {
            try
            {
                var tipoGasto=await _context.TipoGastos.Where(x=>x.IdGasto==expenseTypeDTO.Id).FirstOrDefaultAsync();

                if(tipoGasto==null)
                {
                    tipoGasto.NombreGasto=expenseTypeDTO.name;
                    tipoGasto.Activo=true;

                    _context.TipoGastos.Add(tipoGasto);
                    _context.SaveChanges();
                    return new BaseResult(){Message="Registro creado con exito",Error=false};
                }
                else
                {
                    tipoGasto.NombreGasto=expenseTypeDTO.name;
                    tipoGasto.Activo=expenseTypeDTO.state;
                    _context.Entry(tipoGasto).State=Microsoft.EntityFrameworkCore.EntityState.Modified;
                    return new BaseResult(){Message="Registro actualizado con exito",Error=false};
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error with InsertTypeExpense", ex);
                return new BaseResult() { Error = true, Message = "Error al anular."};
            }
            
        }
        public async Task<BaseResult> InsertTypeIncome(IncomeTypeDTO incomeTypeDTO)
        {
            try
            {
                var tipoingreso=await _context.TipoIngresos.Where(x=>x.IdIngreso==incomeTypeDTO.idIncome).FirstOrDefaultAsync();

                if(tipoingreso==null)
                {
                    tipoingreso.NombreIngreso=incomeTypeDTO.nombreIngreso;
                    tipoingreso.Activo=true;

                    _context.TipoIngresos.Add(tipoingreso);
                    _context.SaveChanges();
                    return new BaseResult(){Message="Registro creado con exito",Error=false};
                }
                else
                {
                    tipoingreso.NombreIngreso=incomeTypeDTO.nombreIngreso;
                    tipoingreso.Activo=incomeTypeDTO.active;
                    _context.Entry(tipoingreso).State=Microsoft.EntityFrameworkCore.EntityState.Modified;
                    return new BaseResult(){Message="Registro actualizado con exito",Error=false};
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error with InsertTypeIncome", ex);
                return new BaseResult() { Error = true, Message = "Error al anular."};
            }
            
        }
        
        public async Task<BaseResult> OverridICC(int IdConsecutive)
        {
            try
            {
                var det = _context.TblIngresosCajaChicas.Where(d => d.Consecutivo == IdConsecutive).FirstOrDefault();
                if(det == null)
                    return new BaseResult() { Error = true, Message = "Error el consecutivo no existe."};

                det.Anulado = false;
                _context.Entry(det).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await _context.SaveChangesAsync();

                return new BaseResult(){Message="Registro anulado.",Error=false};
            }
            catch (Exception ex)
            {
                _logger.LogError("Error with OverridICC", ex);
                return new BaseResult() { Error = true, Message = "Error al anular."};
            }
        }
        public async Task<BaseResult> InsertICC(TblIngresosCajaChica model)
        {
            try
            {
                string Message = "Registro ingresado.";
                var data = await _context.TblIngresosCajaChicas.Where(x=>x.Consecutivo == model.Consecutivo).FirstOrDefaultAsync();    

                if(data == null){
                    TblIngresosCajaChica TblIngresosCajaChicas = new TblIngresosCajaChica(){
                        Consecutivo = model.Consecutivo,
                        IdUsuario =  1,
                        TipoIngresoC = model.TipoIngresoC,
                        Fecha = model.Fecha,
                        Concepto = model.Concepto,
                        Total = model.Total,
                        Mes = model.Mes,
                        Anio = model.Anio,
                        Anulado = false
                    };

                    await _context.TblIngresosCajaChicas.AddAsync(TblIngresosCajaChicas);
                    await _context.SaveChangesAsync();
                }  
                else{
                    
                    data.TipoIngresoC = model.TipoIngresoC;
                    data.Fecha = model.Fecha;
                    data.Concepto = model.Concepto;
                    data.Total = model.Total;
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
                _logger.LogError("Error with InsertICC", ex);
                return new BaseResult() { Error = true, Message = "Error al ingresar ICC"};
            }
        }
        public BaseResult DeleteTGCC(int IdTGCC)
        {
            try
            {
               /* var det = _context.TblGastoCajaChicas.Where(d => d.IdGastoCajaChica == IdTGCC).FirstOrDefault();
                _context.TblGastoCajaChicas.Remove(det);
                _context.SaveChanges();
                */
                return new BaseResult() { Error = false, Message = "Registro eliminado."};
            }
            catch (Exception ex)
            {
                _logger.LogError("Error with DeleteTGCC", ex);
                return new BaseResult() { Error = true, Message = "Error al eliminar."};
            }
        }
        public async Task<BaseResult> UpdateTGCC(TipoGastoCajaChica model)
        {
            try
            {
                var data = _context.TipoGastoCajaChicas.Where(x=>x.IdGastoCajaChica == model.IdGastoCajaChica).FirstOrDefault();
                if (data == null)
                    return new BaseResult() { Error = true, Message = "No fue encontrado registro"};
                
                data.NombreGastoCajachica = model.NombreGastoCajachica;
                data.Activo = model.Activo;
                _context.Entry(data).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await _context.SaveChangesAsync();

                return new BaseResult() { Error = false, Message = "Registro actualizado"};
            }
            catch (Exception ex)
            {
                _logger.LogError("Error with UpdateTGCC", ex);
                return new BaseResult() { Error = true, Message = "Error al ingresar TGCC"};
            }
        }

        public async Task<BaseResult> InsertTGCC(TipoGastoCajaChica model)
        {
            try
            {
                string Message = "Registro ingresado.";
                var data = await _context.TipoGastoCajaChicas.Where(x=>x.IdGastoCajaChica == model.IdGastoCajaChica).FirstOrDefaultAsync();    

                if(data == null){
                    TipoGastoCajaChica TipoGastoCajaChicas = new TipoGastoCajaChica(){
                        IdGastoCajaChica = model.IdGastoCajaChica,
                        NombreGastoCajachica =  model.NombreGastoCajachica,
                        Activo = true
                    };

                    await _context.TipoGastoCajaChicas.AddAsync(TipoGastoCajaChicas);
                    await _context.SaveChangesAsync();
                }  
                else{
                    
                    data.NombreGastoCajachica = model.NombreGastoCajachica;

                    _context.Entry(data).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    await _context.SaveChangesAsync();

                    Message = "Registro actualizado";
                };

                return new BaseResult() { Error = false, Message = Message};
            }
            catch (Exception ex)
            {
                _logger.LogError("Error with InsertTGCC", ex);
                return new BaseResult() { Error = true, Message = "Error al ingresar TGCC"};
            }
        }

        public BaseResult DeleteTICC(int IdTICC)
        {
            try
            {
                var det = _context.TipoIngresoCajaChicas.Where(d => d.IdIngresoaCajaChica == IdTICC).FirstOrDefault();
                _context.TipoIngresoCajaChicas.Remove(det);
                _context.SaveChanges();

                return new BaseResult() { Error = false, Message = "Registro eliminado."};
            }
            catch (Exception ex)
            {
                _logger.LogError("Error with DeleteTICC", ex);
                return new BaseResult() { Error = true, Message = "Error al eliminar."};
            }
        }
        public async Task<BaseResult>UpdateTICC(TipoIngresoCajaChica model)
        {
            try{
                var det = _context.TipoIngresoCajaChicas.Where(d => d.IdIngresoaCajaChica == model.IdIngresoaCajaChica).FirstOrDefault();
                if(det==null)
                    return new BaseResult(){Error=true,Message="No fue encontrado un registro"};
                det.NombreIngresoCajaChica=model.NombreIngresoCajaChica;
                det.Activo=model.Activo;
                _context.Entry(det).State=EntityState.Modified;
                await _context.SaveChangesAsync();

                return new BaseResult() { Error = false, Message = "Registro Actualizado."};
            }
            catch(Exception ex){
                _logger.LogError("Error with UpdateTICC", ex);
                return new BaseResult() { Error = true, Message = "Error al eliminar."};
            }
        }
         public async Task<BaseResult> InsertTICC(TipoIngresoCajaChica model)
        {
            try
            {
                string Message = "Registro ingresado.";
                var data = await _context.TipoIngresoCajaChicas.Where(x=>x.IdIngresoaCajaChica == model.IdIngresoaCajaChica).FirstOrDefaultAsync();    

                if(data == null){
                    TipoIngresoCajaChica TipoIngresoCajaChicas = new TipoIngresoCajaChica(){
                        NombreIngresoCajaChica = model.NombreIngresoCajaChica,
                        Activo =  model.Activo,
                    };

                    await _context.TipoIngresoCajaChicas.AddAsync(TipoIngresoCajaChicas);
                    await _context.SaveChangesAsync();
                }  
                else{
                    
                    data.NombreIngresoCajaChica = model.NombreIngresoCajaChica;

                    _context.Entry(data).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    await _context.SaveChangesAsync();

                    Message = "Registro actualizado.";
                };

                return new BaseResult() { Error = false, Message = Message};
            }
            catch (Exception ex)
            {
                _logger.LogError("Error with InsertTICC", ex);
                return new BaseResult() { Error = true, Message = "Error al ingresar TICC"};
            }
        }
   
        /**
            TABLA PARA GRABAR MOVIMIENTOS DE LOS DOCUMENTOS
            InsertMovDoc -- inserta mov
            CancelMovDoc -- anular mov
        **/
        public async Task<BaseResult> InsertMovDoc(MovimientosDoc model)
        {
            try
            {
                MovimientosDoc MovimientosDocs = new MovimientosDoc(){
                    IdDocumento = model.IdDocumento,
                    IdUsuario =  model.IdUsuario,
                    Modulo = model.Modulo,
                    Total = model.Total,
                    Tipo = model.Tipo,
                    FechaIngreso = model.FechaIngreso,
                    Anulado = false,
                    FechaAnulado = null
                };

                await _context.MovimientosDocs.AddAsync(MovimientosDocs);
                await _context.SaveChangesAsync();

                return new BaseResult() { Error = false, Message = "Registro ingresado."};
            }
            catch (Exception ex)
            {
                _logger.LogError("Error with InsertMovDoc", ex);
                return new BaseResult() { Error = true, Message = "Error al ingresar InsertMovDoc"};
            }
        }

        public async Task<BaseResult> CancelMovDoc(int IdMovimiento)
        {
            try
            {
                var data = await _context.MovimientosDocs.Where(x=>x.IdMovimiento == IdMovimiento).FirstOrDefaultAsync(); 
                if(data == null)
                    return new BaseResult() { Error = true, Message = "El movimiento no existe."};

                data.Anulado = true;
                data.FechaAnulado = DateTime.Now;

                _context.Entry(data).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await _context.SaveChangesAsync();

                return new BaseResult() { Error = false, Message = "Registro anulado."};
            }
            catch (Exception ex)
            {
                _logger.LogError("Error with InsertTICC", ex);
                return new BaseResult() { Error = true, Message = "Error al ingresar InsertMovDoc"};
            }
        }

        public async Task<BaseResult> InsertProdExpense(ProductoGastoCajaChica model)
        {
            try
            {
                if(model.Valor <= 0)
                    return new BaseResult(){Message="El valor no puede ser menor o igual a 0.",Error=true};

                var expense = await _context.ProductoGastoCajaChicas.Where(x=>x.Id==model.Id).FirstOrDefaultAsync();

                if(expense == null)
                {
                    expense.Concepto = model.Concepto;
                    expense.Valor = model.Valor;

                    _context.ProductoGastoCajaChicas.Add(expense);
                    _context.SaveChanges();
                    return new BaseResult(){Message= "Registro creado con exito",Error=false};
                }
                else
                {
                    expense.Concepto = model.Concepto;
                    expense.Valor = model.Valor;

                    _context.Entry(expense).State=Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _context.SaveChanges();

                    return new BaseResult(){Message="Registro actualizado con exito",Error=false};
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error with InsertProdExpense", ex);
                return new BaseResult() { Error = true, Message = "Error al anular."};
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
