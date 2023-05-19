﻿using Online_Shop.Dto;

namespace Online_Shop.Interfaces.ServiceInterfaces
{
    public interface IAuthService
    {
        Task<string> Login(UserDto userDto);
    }
}
