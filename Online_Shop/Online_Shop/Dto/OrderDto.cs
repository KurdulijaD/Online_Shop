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
    }
}
