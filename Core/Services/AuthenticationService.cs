using Domain.Entities.Identity;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AuthenticationService(UserManager<User> _userManager,
                                        IOptions<JwtOptions> options,
                                        IMapper _mapper) : IAuthenticationService
    {
        private readonly JwtOptions _jwtOptions = options.Value;

        public async Task<UserResultDto> GetCurrentUserAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email)
                        ?? throw new NotFoundException($"User with email {email} not found");

            return new UserResultDto
                (
                    user.Id
                    , user.UserName ?? " "
                    , user.Email ?? " "
                    , await CreateTokenAsync(user)
                );
        }

        public async Task<AddressDto> GetUserAddressAsync(string email)
        {
            var user = await _userManager.Users.Include(u => u.Address)
                                                .FirstOrDefaultAsync(u => u.Email == email)
                                                ?? throw new NotFoundException($"User with email {email} not found");

            return _mapper.Map<AddressDto>(user.Address);
        }

        public async Task<bool> IsEmailExistsAsync(string email)
           => await _userManager.Users.AnyAsync(u => u.Email == email);


        public async Task<AddressDto> UpdateUserAddressAsync(string email, CreateAddressDto updateAddressDto)
        {
            var user = await _userManager.Users
                                        .Include(u => u.Address)
                                        .FirstOrDefaultAsync(u => u.Email == email)
                                        ?? throw new NotFoundException($"User with email {email} not found");

            if (user.Address == null)
            {
                user.Address = _mapper.Map<Address>(updateAddressDto);
                user.Address.UserId = user.Id; // Important to avoid null FK
            }
            else
            {
                _mapper.Map(updateAddressDto, user.Address); // this updates the existing entity instead of replacing it
            }
            //user.Address = _mapper.Map<Address>(updateAddressDto);

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded) throw new BadRequestExcpetion(string.Join("\n", result.Errors.Select(s => s.Description)));

            return _mapper.Map<AddressDto>(user.Address);
        }


        public async Task<UserResultDto> LoginAsync(LoginDto loginDto)
        {
            // Validate User Existing => UserManager
            var validEmail  = new EmailAddressAttribute().IsValid(loginDto.Email);
            var user        = validEmail ?  await _userManager.FindByEmailAsync(loginDto.Email)
                                          : await _userManager.FindByNameAsync(loginDto.Email);

            if (user == null) throw new UnAuthorizedException();

            // Validate User Password=> UserManager
            var isValidPassword = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!isValidPassword) throw new UnAuthorizedException();


            // Generate Tokin SignIn

            // Return UserResult
            return new UserResultDto
            (
                user.Id,
                user.Description ?? " ",
                user.Email ?? " ",
                await CreateTokenAsync(user)
            );
        }

        public async Task<UserResultDto> RegisterAsync(RegisterDto registerDto)
        {
            // Validate Email and UserName doesn't exist
            var emailExist      = await _userManager.Users.AnyAsync(user => user.Email == registerDto.Email);
            var userNameExist   = await _userManager.Users.AnyAsync(user => user.UserName == registerDto.UserName);

            if (emailExist) throw new BadRequestExcpetion("Email Already Exist");
            if (userNameExist) throw new BadRequestExcpetion("User Name Already Exist");

            // Create USer
            var user = new User
            {
                Description = registerDto.DisplayName,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                UserName = registerDto.UserName,
            };

            var addUserResult = await _userManager.CreateAsync(user, registerDto.Password);
            if (!addUserResult.Succeeded)
                throw new BadRequestExcpetion(string.Join("\n", addUserResult.Errors.Select(err => err.Description)));

            // Generate Token (SignIn)


            // Return UserResult
            return new UserResultDto
            (
                user.Id,
                user.Description ?? " ",
                user.Email ?? " ",
                await CreateTokenAsync(user)
            );
        }

       

        private async Task<string> CreateTokenAsync(User user)
        {
            // create Claims
            var authClaims = new List<Claim>
            {
                new Claim("userId", user.Id),
                new Claim(ClaimTypes.Name, user.UserName ?? " "),
                new Claim(ClaimTypes.Email, user.Email ?? " ")
            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            // create security key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            // Return token
            var token = new JwtSecurityToken
            (
                issuer : _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: authClaims,
                signingCredentials: creds,
                expires: DateTime.Now.AddDays(_jwtOptions.DuartionInDays),
                notBefore: DateTime.Now
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
