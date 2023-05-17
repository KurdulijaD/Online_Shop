using Online_Shop.Models;

namespace Online_Shop.Interfaces.RepositoryInterfaces
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetAllCustomers();
        Customer GetCustomerById(int id);
        Customer Register(Customer customer);
        Customer UpdateProfile(int id, Customer customer);
        Customer AcceptRegistration(int id);
    }
}
