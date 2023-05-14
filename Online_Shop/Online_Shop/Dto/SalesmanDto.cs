using Online_Shop.Common;
using Online_Shop.Models;

namespace Online_Shop.Dto
{
    public class SalesmanDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public byte[] Image { get; set; }
        public bool Status { get; set; }
        public EVerificationStatus Verified { get; set; }
        public List<ProductDto> Products { get; set; }
        public AdminDto Admin { get; set; }
    }
}
