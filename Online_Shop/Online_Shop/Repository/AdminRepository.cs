using Online_Shop.Data;
using Online_Shop.Interfaces.RepositoryInterfaces;
using Online_Shop.Models;

namespace Online_Shop.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly DataContext _context;

        public AdminRepository(DataContext dataContext)
        {
            _context = dataContext;
        }

        public Admin UpdateProfile(int id, Admin admin)
        {
            Admin temp = _context.Admins.Find((int)id);

            temp.Username = admin.Username;
            temp.Name = admin.Name;
            temp.Email = admin.Email;
            temp.Address = admin.Address;
            temp.BirthDate = admin.BirthDate;
            temp.Image = admin.Image;
            temp.Password = admin.Password;
            temp.Token = admin.Token;

            _context.SaveChanges();
            return temp;
        }
    }
}
