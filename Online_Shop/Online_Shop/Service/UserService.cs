using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Online_Shop.Dto;
using Online_Shop.Interfaces.RepositoryInterfaces;
using Online_Shop.Interfaces.ServiceInterfaces;
using Online_Shop.Models;
using Online_Shop.Repository;
using System.Text;

namespace Online_Shop.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository repository, IMapper mapper, IConfiguration configuration)
        {
            _repository = repository;
            _mapper = mapper;
            _configuration = configuration;
        }
        public async Task<UserDto> AcceptVerification(int id)
        {
            User u = await _repository.AcceptVerification(id);
            if (u == null)
                throw new Exception($"User with ID: {id} doesn't exist.");
            return _mapper.Map<UserDto>(u);
        }

        public async Task<UserDto> DenieVerification(int id)
        {
            User u = await _repository.DenieVerification(id);
            if(u == null)
                throw new Exception($"User with ID: {id} doesn't exist.");
            return _mapper.Map<UserDto>(u);
        }

        public async Task<List<UserDto>> GetAll()
        {
            List<User> users = await _repository.GetAll();
            if (users == null)
                throw new Exception($"There are no users!");
            return _mapper.Map<List<UserDto>>(users);
        }

        public async Task<List<UserDto>> GetAllSalesmans()
        {
            List<User> users = await _repository.GetAllSalesmans();
            if (users == null)
                throw new Exception($"There are no users!");
            return _mapper.Map<List<UserDto>>(users);
        }

        public async Task<UserDto> GetById(int id)
        {
            User u = await _repository.GetById(id);
            if (u == null)
                throw new Exception($"User with ID: {id} doesn't exist.");
            return _mapper.Map<UserDto>(u);
        }
        public async Task<UserDto> Register(UserDto userDto)
        {
            User newUser = _mapper.Map<User>(userDto);
            newUser.Image = Encoding.ASCII.GetBytes(userDto.Image);

            if (String.IsNullOrEmpty(newUser.Name) || String.IsNullOrEmpty(newUser.Username) ||
                String.IsNullOrEmpty(newUser.Email) || String.IsNullOrEmpty(newUser.Address) ||
                String.IsNullOrEmpty(newUser.Password) || String.IsNullOrEmpty(newUser.Type.ToString()))
            {
                throw new Exception($"You must fill in all fields for registration!");
            }

            newUser.Password = BCrypt.Net.BCrypt.HashPassword(newUser.Password);
            newUser.Verification = Common.EVerificationStatus.INPROGRESS;
            UserDto dto = _mapper.Map<UserDto>(await _repository.Register(newUser));
            dto.Image = Encoding.Default.GetString(newUser.Image);
            return dto;
        }

        public async Task<UserDto> UpdateProfile(UserDto userDto)
        {
            User newUser = _mapper.Map<User>(userDto);
            newUser.Image = Encoding.ASCII.GetBytes(userDto.Image);

            if (String.IsNullOrEmpty(newUser.Name) || String.IsNullOrEmpty(newUser.Username) ||
                String.IsNullOrEmpty(newUser.Email) || String.IsNullOrEmpty(newUser.Address) ||
                String.IsNullOrEmpty(newUser.Password))
            {
                throw new Exception($"You must fill in all fields for update profile!");
            }

            newUser.Password = BCrypt.Net.BCrypt.HashPassword(newUser.Password);
            UserDto dto = _mapper.Map<UserDto>(await _repository.UpdateProfile(newUser));
            dto.Image = Encoding.Default.GetString(newUser.Image);
            return dto;
        }
    }
}
