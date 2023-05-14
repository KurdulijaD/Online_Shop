using Online_Shop.Models;

namespace Online_Shop.Dto
{
    public class CustomerDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public byte[] Image { get; set; }
        public bool Status { get; set; }
        public List<OrderDto> Orders { get; set; }
        public AdminDto Admin { get; set; }
    }
}
