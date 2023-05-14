using Online_Shop.Models;

namespace Online_Shop.Dto
{
    public class AdminDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public byte[] Image { get; set; }
        public List<SalesmanDto> Salesmens { get; set; }
        public List<CustomerDto> Customers { get; set; }
    }
}
