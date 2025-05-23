using Microsoft.AspNetCore.Authorization;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    public class AuthenticationController(IServiceManager serviceManager) : ApiController
    {
        [HttpPost("Login")]
        public async Task<ActionResult<UserResultDto>> Login([FromBody] LoginDto loginDto)
                    => Ok(await serviceManager.AuthenticationService.LoginAsync(loginDto));

        [HttpPost("Register")]
        public async Task<ActionResult<UserResultDto>> Register([FromBody] RegisterDto registerDto)
                    => Ok(await serviceManager.AuthenticationService.RegisterAsync(registerDto));

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<UserResultDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            return Ok(await serviceManager.AuthenticationService.GetCurrentUserAsync(email!));
        }

        [HttpGet("Address")]
        [Authorize]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            return Ok(await serviceManager.AuthenticationService.GetUserAddressAsync(email!));
        }

        [HttpPut("Address")]
        [Authorize]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(CreateAddressDto updateAddressDto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            return Ok(await serviceManager.AuthenticationService.UpdateUserAddressAsync(email!, updateAddressDto));
        }

    }
}
