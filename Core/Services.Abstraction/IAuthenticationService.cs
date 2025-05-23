using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface IAuthenticationService
    {
        // Login user Token
        Task<UserResultDto> LoginAsync(LoginDto loginDto);

        // Register User
        Task<UserResultDto> RegisterAsync(RegisterDto registerDto);

        // Get Current User
        Task<UserResultDto> GetCurrentUserAsync(string email);

        // Check Email is exists
        Task<bool> IsEmailExistsAsync(string email);

        // Get User Address
        Task<AddressDto> GetUserAddressAsync(string email);

        // Update User Address
        Task<AddressDto> UpdateUserAddressAsync(string email, CreateAddressDto updateAddressDto);
    }
}
