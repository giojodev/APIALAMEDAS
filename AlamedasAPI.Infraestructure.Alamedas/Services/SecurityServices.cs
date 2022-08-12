using System;
using Microsoft.Extensions.Logging;
using AlamedasAPI.Infraestructure.Alamedas.DTO;
using AlamedasAPI.Db.Models.Alamedas.Models;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AlamedasAPI.Infraestructure.Alamedas
{
    public interface ISecurityServices
    {
        Task<UserDTO> ValidateUser(String Login);
        Task<BaseResult> UpdateUser(TblUsuario model);
        List<TblUsuario> GetUser(int IdUser);
        Task<BaseResult> InsertRoles(TblRole model);
        Task<BaseResult> InsertUser(TblUsuario model);
        Task<BaseResult> DeleteUser(int IdUser);
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

        public async Task<UserDTO> ValidateUser(String Login)
        {
            try
            {
                var data = await _context.TblUsuarios.Where(x => x.Ulogin == Login).FirstOrDefaultAsync();
                if(data == null)
                    return new UserDTO() { Error = true, Message = "El usuario no existe."};

                return new UserDTO() { 
                    IdUsuario = data.IdUsuario,
                    IdSql = data.IdSql.Value,
                    IdRol = data.IdRol.Value,
                    Activo = data.Activo.Value,
                    Admin = data.Admin,
                    Error = false, 
                    Message = "Usuario logueado"};
            }
            catch (Exception ex)
            {
                _logger.LogError("Error with ValidateUser", ex);
                return new UserDTO() { Error = true, Message = "Error en el servicio"};
            }
        }

        public async Task<BaseResult> UpdateUser(TblUsuario model)
        {
            try
            {
                //var commandText = "";
                var data = await _context.TblUsuarios.Where(x=> x.IdUsuario == model.IdUsuario).FirstOrDefaultAsync();
                if(data == null)
                    return new BaseResult() { Error = true, Message = "Usuario no encontrado."};

                data.Ulogin = model.Ulogin;
                data.Unombre = model.Unombre;
                data.Correro = model.Correro;
                data.IdRol = model.IdRol;
                data.Activo = model.Activo;

                _context.Entry(data).State=Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();

                /*if(model.Activo.Value){
                    commandText = "ALTER LOGIN [" + model.Ulogin + "] ENABLE";
                    _context.Database.ExecuteSqlRaw(commandText);
                }
                else{
                    commandText = "ALTER LOGIN [" + model.Ulogin + "] DISABLE";
                    _context.Database.ExecuteSqlRaw(commandText);
                };

                commandText = "UPDATE tblUsuarios SET IdSql = SUSER_ID('" + model.Ulogin + "') WHERE IdUsuario = "+model.IdUsuario;
                _context.Database.ExecuteSqlRaw(commandText);*/

                return new BaseResult() { Error = true, Message = "Registro actualizado."};
            }
             catch (Exception ex)
            {
                _logger.LogError("Error with UpdateUser", ex);
                return new BaseResult() { Error = true, Message = "Error en el servicio."};
            }
        }

        public List<TblUsuario> GetUser(int IdUser){
            
            List<TblUsuario> data = null;

            if(IdUser == 0){
                data = _context.TblUsuarios.ToList();
            }else{
                data = _context.TblUsuarios.Where(x => x.IdUsuario == IdUser).ToList();
            };

            return data;
        }
   
        public async Task<BaseResult> InsertRoles(TblRole model)
        {
            try
            {
                string Message = "Registro ingresado.";
                var data = await _context.TblRoles.Where(x=>x.IdRol == model.IdRol).FirstOrDefaultAsync();

                if(data == null){

                    TblRole TblRoles = new TblRole(){
                        Nombre = model.Nombre,
                        Descripcion = model.Descripcion
                    };

                    await _context.TblRoles.AddAsync(TblRoles);
                    await _context.SaveChangesAsync();
                }
                else{
                    
                    data.Nombre = model.Nombre;
                    data.Descripcion = data.Descripcion;

                    _context.Entry(data).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    await _context.SaveChangesAsync();

                    Message = "Registro actualizado";
                };

                return new BaseResult() { Error = false, Message = Message};
            }
            catch (Exception ex)
            {
                _logger.LogError("Error with InsertRoles", ex);
                return new BaseResult() { Error = true, Message = "Error en el servicio."};
            }
        }
   
        public async Task<BaseResult> InsertUser(TblUsuario model)
        {
            try
            {
                TblUsuario TblUsuarios = new TblUsuario(){
                    IdSql = model.IdSql,
                    Ulogin = model.Ulogin,
                    Unombre = model.Unombre,
                    Correro = model.Correro,
                    IdRol = model.IdRol,
                    Activo = model.Activo,
                    Admin = model.Admin
                };

                await _context.TblUsuarios.AddAsync(TblUsuarios);
                await _context.SaveChangesAsync();

                return new BaseResult() { Error = false, Message = "Registro ingresado."};

            }
            catch (Exception ex)
            {
               _logger.LogError("Error with InsertUser", ex);
                return new BaseResult() { Error = true, Message = "Error en el servicio."};
            }
        }

        public async Task<BaseResult> DeleteUser(int IdUser){
            try
            {
                var data = await _context.TblUsuarios.Where(x=> x.IdUsuario == IdUser).FirstOrDefaultAsync();
                if(data == null)
                    return new BaseResult() { Error = true, Message = "Usuario no encontrado."};

                data.Activo = false;   

                _context.Entry(data).State=Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();

                //commandText = "ALTER LOGIN [" + data.Ulogin + "] DISABLE";
                //_context.Database.ExecuteSqlRaw(commandText);

                return new BaseResult() { Error = true, Message = "Registro eliminado."};    
            }
            catch (Exception ex)
            {
               _logger.LogError("Error with DeleteUser", ex);
                return new BaseResult() { Error = true, Message = "Error en el servicio."};
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