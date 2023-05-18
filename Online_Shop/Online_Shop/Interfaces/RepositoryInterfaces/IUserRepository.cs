using Online_Shop.Models;

namespace Online_Shop.Interfaces.RepositoryInterfaces
{
    public interface IUserRepository
    {
        User GetById(int id);
        List<User> GetAll();
        List<User> GetAllSalesmans();
        User UpdateProfile(User newUser);
        User Register(User user);
        User AcceptVerification(string verificationStatus, int id);
    }
}
