using Online_Shop.Common;
using Online_Shop.Data;
using Online_Shop.Interfaces.RepositoryInterfaces;
using Online_Shop.Models;

namespace Online_Shop.Repository
{
    public class SalesmanRepository : ISalesmanRepository
    {
        private readonly DataContext _context;

        public SalesmanRepository(DataContext dataContext)
        {
            _context = dataContext;
        }
        public Salesman AcceptRegistration(int id)
        {
            Salesman salesman = _context.Salesmens.Find((int)id);
            salesman.Status = true;
            _context.SaveChanges();
            return salesman;
        }

        public Salesman AcceptVerification(int id)
        {
            Salesman salesman = _context.Salesmens.Find((int)id);
            salesman.Verified = EVerificationStatus.ACCEPTED;
            _context.SaveChanges();
            return salesman;
        }

        public Salesman DenieVerification(int id)
        {
            Salesman salesman = _context.Salesmens.Find((int)id);
            salesman.Verified = EVerificationStatus.DENIED;
            _context.SaveChanges();
            return salesman;
        }

        public IEnumerable<Salesman> GetAllSalesmans()
        {
            List<Salesman> salesmens = _context.Salesmens.ToList();
            return salesmens;
        }

        public Salesman GetSalesmanById(int id)
        {
            Salesman salesman = _context.Salesmens.Find((int)id);
            return salesman;
        }

        public Salesman Register(Salesman salesman)
        {
            _context.Salesmens.Add(salesman);
            _context.SaveChanges();
            return salesman;
        }

        public Salesman UpdateProfile(int id, Salesman salesman)
        {
            Salesman temp = _context.Salesmens.Find((int)id);

            temp.Username = salesman.Username;
            temp.Name = salesman.Name;
            temp.Email = salesman.Email;
            temp.Address = salesman.Address;
            temp.BirthDate = salesman.BirthDate;
            temp.Image = salesman.Image;
            temp.Password = salesman.Password;
            temp.Token = salesman.Token;

            _context.SaveChanges();
            return temp;
        }
    }
}
