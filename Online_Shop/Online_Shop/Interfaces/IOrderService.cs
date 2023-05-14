using Online_Shop.Dto;

namespace Online_Shop.Interfaces
{
    public interface IOrderService
    {
        List<OrderDto> GetOrders();
        OrderDto CreateOrder(OrderDto order);
    }
}
