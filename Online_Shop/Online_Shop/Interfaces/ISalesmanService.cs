using Online_Shop.Dto;

namespace Online_Shop.Interfaces
{
    public interface ISalesmanService
    {
        SalesmanDto GetProfile(string token);
        SalesmanDto UpdateProfile(SalesmanDto salesman);
        List<SalesmanDto> GetSalesmans();
        bool AcceptRegistration(string username);
        bool Verificate(string username);
    }
}
