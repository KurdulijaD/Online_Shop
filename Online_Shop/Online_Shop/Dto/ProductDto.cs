using Online_Shop.Models;

namespace Online_Shop.Dto
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        public ProductDto()
        {
            
        }
        public ProductDto(int id, string name, int price, int amount, string description, string image)
        {
            Id = id;
            Name = name;
            Price = price;
            Amount = amount;
            Description = description;
            Image = image;
        }


    }
}
