using Online_Shop.Dto;
using Online_Shop.Models;

namespace Online_Shop.Interfaces.ServiceInterfaces
{
    public interface IUserService
    {
        Task<UserDto> GetById(int id);
        Task<List<UserDto>> GetAll();
        Task<List<UserDto>> GetAllSalesmans();
        Task<UserDto> UpdateProfile(UserDto userDto);
        Task<UserDto> Register(UserDto userDto);
        Task<UserDto> AcceptVerification(int id);
        Task<UserDto> DenieVerification(int id);
    }
}
