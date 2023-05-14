﻿using Online_Shop.Dto;

namespace Online_Shop.Interfaces
{
    public interface ICustomerService
    {
        CustomerDto GetProfile(string token);
        CustomerDto UpdateProfile(CustomerDto customer);
        List<CustomerDto> GetCustomers();
        bool AcceptRegistration(string username);
    }
}
