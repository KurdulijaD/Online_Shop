using Online_Shop.Dto;
using Online_Shop.Models;

namespace Online_Shop.Interfaces.ServiceInterfaces
{
    public interface IOrderService
    {
        Task<List<OrderDto>> GetAllOrders();
        Task<List<OrderDto>> GetAllInProgressOrders();
        Task<OrderDto> GetOrderById(int id);
        Task<OrderDto> CreateOrder(OrderDto orderDto);
        Task<OrderDto> DenieOrder(int id);
    }
}
