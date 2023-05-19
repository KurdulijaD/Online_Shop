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
                List<Order> orders = _context.Orders.Where(o => o.Status != Common.EOrderStatus.DENIED).ToList();
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
        public async Task<Order> DenieOrder(int id)
        {
            try
            {
                Order order = _context.Orders.Find((int)id);
                order.Status = Common.EOrderStatus.DENIED;
                _context.SaveChanges();
                return order;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<List<Order>> GetAllInProgressOrders()
        {
            try
            {
                List<Order> orders = _context.Orders.Where(o => o.Status == Common.EOrderStatus.INPROGRESS).ToList();
                return orders;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
