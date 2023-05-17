using Online_Shop.Models;

namespace Online_Shop.Interfaces.RepositoryInterfaces
{
    public interface IAdminRepository
    {
        Admin UpdateProfile(int id, Admin admin);
    }
}
