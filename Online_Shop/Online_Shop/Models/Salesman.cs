using Online_Shop.Common;

namespace Online_Shop.Models
{
    public class Salesman : User
    {
        public bool Status { get; set; }
        public EVerificationStatus Verified { get; set; }
        public List<Product> Products { get; set; }
        public Admin Admin { get; set; }
    }
}
