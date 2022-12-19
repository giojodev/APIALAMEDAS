using Microsoft.AspNetCore.Mvc;
using AlamedasAPI.DTO;
using AlamedasAPI.Infraestructure.Alamedas;

namespace AlamedasAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class AccountController : ControllerBase
    {
        private readonly ISecurityServices _segurityservices;

        public AccountController(ISecurityServices segurityservices)
        {
            _segurityservices = segurityservices;
        }
        
        [HttpPost("[action]")]
        public IActionResult Login(LoginRequest login)
        {
            var response = _segurityservices.Authenticate(login.UserName,login.Password);
            if(!response.Authenticate!)
                return BadRequest(response);

            return Ok(response);
        }

    }
}
