using Online_Shop.Common;

namespace Online_Shop.Models
{
    public abstract class User
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }    
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public byte[] Image { get; set; }
    }
}
