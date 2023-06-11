using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Online_Shop.Common;
using Online_Shop.Dto;
using Online_Shop.Exceptions;
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
        private readonly IEmailService _emailService;

        public UserService(IUserRepository repository, IEmailService emailService, IMapper mapper, IConfiguration configuration)
        {
            _repository = repository;
            _emailService = emailService;
            _mapper = mapper;
            _configuration = configuration;
        }
        public async Task<UserDto> AcceptVerification(int id)
        {
            User u = await _repository.GetById(id);
            if (u == null)
                throw new NotFoundException($"User with ID: {id} doesn't exist.");
            if(u.Verification != Common.EVerificationStatus.INPROGRESS)
                throw new BadRequestException($"Cant change verification anymore!");

            u = await _repository.AcceptVerification(id);
            if(u != null)
            {
                await _emailService.SendEmail(u.Email, u.Verification.ToString());
            }
            return _mapper.Map<User, UserDto>(u);
        }

        public async Task<UserDto> DenyVerification(int id)
        {
            User u = await _repository.GetById(id);
            if (u == null)
                throw new NotFoundException($"User with ID: {id} doesn't exist.");
            if (u.Verification != Common.EVerificationStatus.INPROGRESS)
                throw new BadRequestException($"Cant change verification anymore!");

            u = await _repository.DenyVerification(id);
            if (u != null)
            {
                _emailService.SendEmail(u.Email, u.Verification.ToString());
            }
            return _mapper.Map<User, UserDto>(u);
        }

        public async Task<List<UserDto>> GetAll()
        {
            List<User> users = await _repository.GetAll();
            if (users == null)
                throw new NotFoundException($"There are no users!");
            return _mapper.Map<List<User>, List<UserDto>>(users);
        }

        public async Task<List<UserVerificationDto>> GetAllSalesmans()
        {
            List<User> users = await _repository.GetAllSalesmans();
            if (users == null)
                throw new NotFoundException($"There are no users!");
            return _mapper.Map<List<User>, List<UserVerificationDto>>(users);
        }

        public async Task<UserDto> GetById(int id)
        {
            User u = await _repository.GetById(id);
            if (u == null)
                throw new NotFoundException($"User with ID: {id} doesn't exist.");
            return _mapper.Map<User, UserDto>(u);
        }
        public async Task<UserDto> Register(RegisterDto registerDto)
        {
            List<User> users = await _repository.GetAll();

            if (String.IsNullOrEmpty(registerDto.FirstName) || String.IsNullOrEmpty(registerDto.LastName) || String.IsNullOrEmpty(registerDto.Username) ||
                String.IsNullOrEmpty(registerDto.Email) || String.IsNullOrEmpty(registerDto.Address) ||
                String.IsNullOrEmpty(registerDto.Password) || String.IsNullOrEmpty(registerDto.RepeatPassword) || String.IsNullOrEmpty(registerDto.Type.ToString()))
                throw new BadRequestException($"You must fill in all fields for registration!");

            if (users.Any(u => u.Username == registerDto.Username))
                throw (Exception)new ConflictException("Username already in use. Try again!").ToActionResult();

            if (users.Any(u => u.Email == registerDto.Email))
                throw new ConflictException("Email already in use. Try again!");

            if (registerDto.Password != registerDto.RepeatPassword)
                throw new BadRequestException("Passwords do not match. Try again!");

            User newUser = _mapper.Map<RegisterDto, User>(registerDto);
            newUser.Image = Encoding.ASCII.GetBytes(registerDto.Image);
            newUser.Password = BCrypt.Net.BCrypt.HashPassword(newUser.Password);
            newUser.Type = (EUserType)Enum.Parse(typeof(EUserType), registerDto.Type.ToUpper());

            if(newUser.Type == Common.EUserType.SALESMAN)
                newUser.Verification = Common.EVerificationStatus.INPROGRESS;
            else
                newUser.Verification = Common.EVerificationStatus.ACCEPTED;

            UserDto dto = _mapper.Map<User, UserDto>(await _repository.Register(newUser));
            dto.Image = Encoding.Default.GetString(newUser.Image);
            return dto;
        }

        public async Task<UserDto> UpdateProfile(int id, UpdateProfileDto profileDto)
        {
            List<User> users = await _repository.GetAll();
            User user = await _repository.GetById(id);
            if (user == null)
                throw new NotFoundException($"User with {id} doesn't exist!");

            if (String.IsNullOrEmpty(profileDto.FirstName) || String.IsNullOrEmpty(profileDto.LastName) || 
                String.IsNullOrEmpty(profileDto.Username) || String.IsNullOrEmpty(profileDto.Email) || 
                String.IsNullOrEmpty(profileDto.Address))
                throw new BadRequestException($"You must fill in all fields for update profile!");

            if(profileDto.Username != user.Username)
                if (users.Any(u => u.Username == profileDto.Username))
                    throw new ConflictException("Username already in use. Try again!");

            if (profileDto.Email != user.Email)
                if (users.Any(u => u.Email == profileDto.Email))
                    throw new ConflictException("Email already in use. Try again!");

            if (!String.IsNullOrEmpty(profileDto.Password))
            {
                if (String.IsNullOrEmpty(profileDto.OldPassword))
                    throw new BadRequestException("You must enter old password!");

                if (!BCrypt.Net.BCrypt.Verify(profileDto.OldPassword, user.Password))
                    throw new BadRequestException("Old password is incorrect!");

                user.Password = BCrypt.Net.BCrypt.HashPassword(profileDto.Password);

            }

            if (String.IsNullOrEmpty(profileDto.Password) && String.IsNullOrEmpty(profileDto.OldPassword))
                profileDto.Password = user.Password;

            _mapper.Map(profileDto, user);
            //user = _mapper.Map<User>(profileDto);
            user.Image = Encoding.ASCII.GetBytes(profileDto.Image);

            UserDto dto = _mapper.Map<User, UserDto>(await _repository.UpdateProfile(user));
            dto.Image = Encoding.Default.GetString(user.Image);
            return dto;
        }
    }
}
