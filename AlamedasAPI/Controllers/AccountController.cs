using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using AlamedasAPI.DTO;
using AlamedasAPI.Settings;

namespace AlamedasAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class AccountController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IOptionsSnapshot<AppSettings> appSettings, ILogger<AccountController> logger)
        {
            _appSettings = appSettings.Value;
            _logger = logger;
        }
        
        [HttpPost("[action]")]
        public IActionResult Login(LoginRequest login)
        {
            try
            {

                string[] users;
                string[] pass;

                if (login == null)
                    return BadRequest("No se encontro usuario o contraseña");

                if(_appSettings.UserName == null || _appSettings.Password  == null)
                    return BadRequest("Configuracion de usuarios no definida.");

                users = _appSettings.UserName.Split(',');
                pass = _appSettings.Password.Split(',');

                IEnumerable<string> lstUsers = users;

                var NameUser = lstUsers.Where(x => x.Equals(login.UserName)).FirstOrDefault();

                if (NameUser == null)
                    return Unauthorized();

                var index = Array.IndexOf(users, login.UserName);

                var passUser = pass[index];

                if (passUser == null)
                    return Unauthorized();

                bool isValid = (login.Password.Equals(passUser) && login.UserName.Equals(NameUser));

                if (!isValid)
                    return Unauthorized();

                var token = TokenGenerator.GenerateTokenJwt(login.UserName);

                return Ok(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en autenticar si el problema persiste contactar a SOPORTE TECNICO.");
                return BadRequest(ex.Message.ToString());
            }
        }

    }
}
