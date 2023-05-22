using Online_Shop.Data;
using Online_Shop.Interfaces.RepositoryInterfaces;
using Online_Shop.Models;

namespace Online_Shop.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataContext _context;

        public OrderRepository(DataContext dataContext)
        {
            _context = dataContext;
        }
        public async Task<Order> CreateOrder(Order order)
        {
            _context.Orders.Add(order);
            try
            {
                _context.SaveChanges();
                return order;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public async Task<List<Order>> GetAllOrders()
        {
            try
            {
                List<Order> orders = _context.Orders.ToList();
                return orders;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<Order> GetOrderById(int id)
        {
            try
            {
                Order order = _context.Orders.Find((int)id);
                return order;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public async Task SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
