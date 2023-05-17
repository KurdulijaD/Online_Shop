using Online_Shop.Dto;

namespace Online_Shop.Interfaces.ServiceInterfaces
{
    public interface IOrderService
    {
        List<OrderDto> GetOrders();
        OrderDto CreateOrder(OrderDto order);
    }
}
