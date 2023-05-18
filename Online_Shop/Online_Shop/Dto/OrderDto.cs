using Online_Shop.Common;
using Online_Shop.Models;

namespace Online_Shop.Dto
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public string Address { get; set; }
        public int Price { get; set; }

        public OrderDto()
        {
            
        }

        public OrderDto(int id, string comment, string address, int price)
        {
            Id = id;
            Comment = comment;
            Address = address;
            Price = price;
        }
    }
}
