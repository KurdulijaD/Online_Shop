namespace Online_Shop.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string SalesmanUsername { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public List<OrderProduct> OrderProducts { get; set; }
        public Salesman Salesman { get; set; }
        public bool Deleted { get; set; }
    }
}
