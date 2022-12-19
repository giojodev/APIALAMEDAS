using System;
using Microsoft.Extensions.Logging;
using AlamedasAPI.Infraestructure.Alamedas.DTO;
using AlamedasAPI.Db.Models.Alamedas.Models;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using AlamedasAPI.Infraestructure.Alamedas.Settings;
using System.Text;
using BCryptNet = BCrypt.Net.BCrypt;

namespace AlamedasAPI.Infraestructure.Alamedas
{
    public interface ISecurityServices
    {
        Task<BaseResult> UpdateUser(TblUsuario model);
        List<TblUsuario> GetUser();
        Task<BaseResult> InsertRoles(TblRole model);
        Task<BaseResult> InsertUser(TblUsuario model);
        Task<BaseResult> DeleteUser(int IdUser);
        UserDTO Authenticate(string UserName,string Password);
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

        public UserDTO Authenticate(string UserName,string Password)
        {
            try
            {
                var user = _context.TblUsuarios.SingleOrDefault(x => x.Usuario == UserName);
                // verify password
                if (user == null || !VerifyHashedPassword(Password, user.Contrasena))
                    return new UserDTO(){ Iduser = 0,Authenticate = false, Message = "Usuario o contraseña incorrecta."};

                var token = GenerateTokenJwt(user.Usuario);
                if (token == null)
                    return new UserDTO(){ Iduser = 0,Authenticate = false, Message = "Error al generar token."};

                // authentication successful
                return new UserDTO() { 
                    Iduser = user.IdUsuario,
                    Username = user.Usuario,
                    Token = token,
                    Message = "Acceso permitido.",
                    Authenticate = true
                };   

            }
            catch (Exception ex)
            {
                _logger.LogError("Error with Authenticate", ex);
                return new UserDTO(){ Iduser = 0,Authenticate = false, Message = "Error en el servicio"};
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

                var data2 = await _context.TblUsuarios.Where(x=> x.IdUsuario != model.IdUsuario && x.Usuario == model.Usuario).FirstOrDefaultAsync();
                if(data2 != null)
                    return new BaseResult() { Error = true, Message = "El usuario ya existe en el sistema favor validar."};    

                data.Usuario = model.Usuario;
                data.Nombre = model.Nombre;
                data.Correo = model.Correo;
                //data.Contrasena = HashPassword(model.Contrasena);
                data.IdRol = model.IdRol;
                data.Activo = model.Activo;
                data.Admin = model.Admin;

                _context.Entry(data).State=Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
                return new BaseResult() { Error = false, Message = "Registro actualizado."};
            }
             catch (Exception ex)
            {
                _logger.LogError("Error with UpdateUser", ex);
                return new BaseResult() { Error = true, Message = "Error en el servicio."};
            }
        }

        public List<TblUsuario> GetUser(){
            
            var data = _context.TblUsuarios.ToList();
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
                var data2 = await _context.TblUsuarios.Where(x=> x.Usuario == model.Usuario).FirstOrDefaultAsync();
                if(data2 != null)
                    return new BaseResult() { Error = true, Message = "El usuario ya existe en el sistema favor validar."}; 

                model.Contrasena = HashPassword(model.Contrasena);
                await _context.TblUsuarios.AddAsync(model);
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

        
        //method hash
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, 12);
        }
	
        public bool VerifyHashedPassword(  string providedPassword,string hashedPassword)
        {
            var isValid = BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword);
            return isValid;
        }
        
        //method token
        public static string GenerateTokenJwt(string username)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtConfig.SecretKey));
            var signIngCredentials =new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, username) });

            var tokenHandler = new JwtSecurityTokenHandler();

            var jwtToken = tokenHandler.CreateJwtSecurityToken(
              subject: claimsIdentity,
              notBefore: DateTime.UtcNow,
              expires: DateTime.UtcNow.AddHours(JwtConfig.HourExpirationTime),
              signingCredentials: signIngCredentials

            );

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject =claimsIdentity,
                Expires = DateTime.UtcNow.AddHours(JwtConfig.HourExpirationTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtConfig.SecretKey)), SecurityAlgorithms.HmacSha256)
            };


            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;

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