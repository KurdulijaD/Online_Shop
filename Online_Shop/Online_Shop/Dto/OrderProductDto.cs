using Online_Shop.Models;

namespace Online_Shop.Dto
{
    public class OrderProductDto
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public int Price { get; set; }
        public OrderDto Order { get; set; }
        public ProductDto Product { get; set; }
    }
}
