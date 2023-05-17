using Online_Shop.Dto;

namespace Online_Shop.Interfaces.ServiceInterfaces
{
    public interface IAdminService
    {
        AdminDto GetProfile(string token);
        AdminDto UpdateProfile(AdminDto admin);
    }
}
