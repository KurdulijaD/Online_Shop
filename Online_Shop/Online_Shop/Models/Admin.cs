namespace Online_Shop.Models
{
    public class Admin : User
    {
        public List<Salesman> Salesmens { get; set; }
        public List<Customer> Customers { get; set; }
    }
}
