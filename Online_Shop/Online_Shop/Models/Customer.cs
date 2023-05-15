namespace Online_Shop.Models
{
    public class Customer : User
    {
        public bool Status { get; set; }
        public List<Order> Orders { get; set; }
        public Admin Admin { get; set; }
        public string AdminUsername { get; set; }
    }
}
