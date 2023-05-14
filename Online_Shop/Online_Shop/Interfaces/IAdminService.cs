using Online_Shop.Dto;

namespace Online_Shop.Interfaces
{
    public interface IAdminService
    {
        AdminDto GetProfile(string token);
        AdminDto UpdateProfile(AdminDto admin);
    }
}
