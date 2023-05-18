using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Online_Shop.Common;
using Online_Shop.Data;
using Online_Shop.Interfaces.RepositoryInterfaces;
using Online_Shop.Models;

namespace Online_Shop.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext dataContext)
        {
            _context = dataContext;
        }

        public User AcceptVerification(string verificationStatus, int id)
        {
            User user = _context.Users.Find((int)id);
            user.Verification = (EVerificationStatus)Enum.Parse(typeof(EVerificationStatus), verificationStatus);
            _context.SaveChanges();
            return user;
        }

        public List<User> GetAll()
        {
            List<User> users = _context.Users.ToList();
            return users;
        }

        public List<User> GetAllSalesmans()
        {
            List<User> salesmans = _context.Users.Where(s => s.Type == EUserType.SALESMAN).ToList();
            return salesmans;
        }

        public User GetById(int id)
        {
            try
            {
                User user = _context.Users.Find((int)id);
                return user;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public User Register(User user)
        {
            _context.Users.Add(user);
            try
            {
                _context.SaveChanges();
                return user;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public User UpdateProfile(User newUser)
        {
            try
            {
                _context.Users.Update(newUser);
                _context.SaveChanges();
                return newUser;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
