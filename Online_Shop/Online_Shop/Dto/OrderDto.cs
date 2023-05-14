using Online_Shop.Common;
using Online_Shop.Models;

namespace Online_Shop.Dto
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string CustomerUsername { get; set; }
        public string Comment { get; set; }
        public string Address { get; set; }
        public DateTime OrderTime { get; set; }
        public DateTime DeliveryTime { get; set; }
        public EOrderStatus Status { get; set; }
        public List<OrderProductDto> OrderProducts { get; set; }
        public CustomerDto Customer { get; set; }
    }
}
