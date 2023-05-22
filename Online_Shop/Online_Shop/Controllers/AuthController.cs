using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Shop.Dto;
using Online_Shop.Interfaces.ServiceInterfaces;

namespace Online_Shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        //POST api/auth
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LoginDto loginDto)
        {
            string token = await _service.Login(loginDto);
            if (token == null)
                return BadRequest();
            return Ok(token);
        }
    }
}
