using Online_Shop.Models;

namespace Online_Shop.Interfaces.RepositoryInterfaces
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllOrders();
        Task<List<Order>> GetAllInProgressOrders();
        Task<Order> GetOrderById(int id);
        Task<Order> CreateOrder(Order order);
        Task<Order> DenieOrder(int id);
    }
}
