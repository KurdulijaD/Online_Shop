using Online_Shop.Models;

namespace Online_Shop.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public byte[] Image { get; set; }

        public UserDto()
        {
            
        }
        public UserDto(int id, string username, string email, string password, string name, DateTime birthDate, string address, byte[] image)
        {
            Id = id;
            Username = username;
            Email = email;
            Password = password;
            Name = name;
            BirthDate = birthDate;
            Address = address;
            Image = image;
        }
    }
}
