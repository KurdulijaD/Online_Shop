using Online_Shop.Models;

namespace Online_Shop.Interfaces.RepositoryInterfaces
{
    public interface ISalesmanRepository
    {
        IEnumerable<Salesman> GetAllSalesmans();
        Salesman GetSalesmanById(int id);
        Salesman Register(Salesman salesman);
        Salesman UpdateProfile(int id, Salesman salesman);
        Salesman AcceptRegistration(int id);
        Salesman AcceptVerification(int id);
        Salesman DenieVerification(int id);
    }
}
