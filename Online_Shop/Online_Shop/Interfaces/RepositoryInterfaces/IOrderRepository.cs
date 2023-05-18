using Online_Shop.Models;

namespace Online_Shop.Interfaces.RepositoryInterfaces
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAllOrders();
        Order GetOrderById(int id);
        Order CreateOrder(Order order);
        Order DenieOrder(int id);
    }
}
