using Online_Shop.Models;

namespace Online_Shop.Dto
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string SalesmanUsername { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public List<OrderProductDto> OrderProducts { get; set; }
        public SalesmanDto Salesman { get; set; }
        public bool Deleted { get; set; }
    }
}
