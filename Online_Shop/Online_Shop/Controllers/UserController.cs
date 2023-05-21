using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Shop.Dto;
using Online_Shop.Interfaces.ServiceInterfaces;
using Online_Shop.Models;

namespace Online_Shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        //GET api/user
        [HttpGet]
        [Authorize(Roles = "ADMINISTRATOR")]
        public async Task<IActionResult> Get()
        {
            List<UserDto> users = await _service.GetAll();
            if (users == null)
                return BadRequest();
            return Ok(users);
        }

        //GET api/user/id
        [HttpGet("{id}")]
        [Authorize(Roles = "ADMINISTRATOR")]
        public async Task<IActionResult> Get(int id)
        {
            UserDto user = await _service.GetById(id);
            if (user == null)
                return BadRequest();
            return Ok(user);
        }

        [HttpGet("GetAllSalesmans")]
        [Authorize(Roles = "ADMINISTRATOR")]
        public async Task<IActionResult> GetAllSalesmans()
        {
            List<UserDto> salesmans = await _service.GetAllSalesmans();
            if (salesmans == null)
                return BadRequest();
            return Ok(salesmans);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]UserDto userDto)
        {
            UserDto user = await _service.Register(userDto);
            if (user == null)
                return BadRequest();
            return Ok(user);
        }

        [HttpPut]
        public async Task<IActionResult> Put(UserDto userDto)
        {
            UserDto user = await _service.UpdateProfile(userDto);
            if (user == null)
                return BadRequest();
            return Ok(user);
        }

        [HttpPut("AcceptVerification/id")]
        public async Task<IActionResult> AcceptVerification(int id)
        {
            UserDto user = await _service.AcceptVerification(id);
            if (user == null)
                return BadRequest();
            return Ok(user);
        }

        [HttpPut("DenieVerification/id")]
        public async Task<IActionResult> DenieVerification(int id)
        {
            UserDto user = await _service.DenieVerification(id);
            if (user == null)
                return BadRequest();
            return Ok(user);
        }
    }
}
