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
    public interface ISecurityServices
    {
        Task<UserDTO> ValidateUser(String Login);
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