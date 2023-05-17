using Online_Shop.Data;
using Online_Shop.Interfaces.RepositoryInterfaces;
using Online_Shop.Models;

namespace Online_Shop.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DataContext _context;

        public CustomerRepository(DataContext dataContext)
        {
            _context = dataContext;
        }
        public Customer AcceptRegistration(int id)
        {
            Customer customer = _context.Customers.Find((int)id);
            customer.Status = true;
            _context.SaveChanges();
            return customer;
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            List<Customer> customers = _context.Customers.ToList();
            return customers;
        }

        public Customer GetCustomerById(int id)
        {
            Customer customer = _context.Customers.Find((int)id);
            return customer;
        }

        public Customer Register(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return customer;
        }

        public Customer UpdateProfile(int id, Customer customer)
        {
            Customer temp = _context.Customers.Find((int)id);

            temp.Username = customer.Username;
            temp.Name = customer.Name;
            temp.Email = customer.Email;
            temp.Address = customer.Address;
            temp.BirthDate = customer.BirthDate;
            temp.Image = customer.Image;
            temp.Password = customer.Password;
            temp.Token = customer.Token;

            _context.SaveChanges();
            return temp;
        }
    }
}
