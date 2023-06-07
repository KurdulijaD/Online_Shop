using Online_Shop.Models;

namespace Online_Shop.Dto
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public int Price { get; set; }
    }
}
