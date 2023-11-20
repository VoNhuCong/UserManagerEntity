using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserManagerEntity.Contract.Requests;
using UserManagerEntity.Models;
using UserManagerEntity.Models.Entities;

namespace UserManagerEntity.Repository
{
    public interface IAccountRepository
    {
        /// <summary>
        /// Đăng ký
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<IdentityResult> SignUpAsync(UserRequest user);

        /// <summary>
        /// Đăng nhập trả về token
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public Task<string> SignInAsync(LoginRequest login);
    };


    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<User> _UserManager;
        private readonly SignInManager<User> _SignInManager;
        private readonly IConfiguration _IConfiguration;
        public AccountRepository(DataContext context, UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
        {
            _UserManager = userManager;
            _SignInManager = signInManager;
            _IConfiguration = configuration;
        }

        /// <summary>
        /// Đănh nhập
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public async Task<string> SignInAsync(LoginRequest login)
        {
            var result = await _SignInManager.PasswordSignInAsync(login.UserName, login.Password, false, false);

            if (!result.Succeeded)
            {
                return string.Empty;
            }

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, login.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_IConfiguration["Jwt:Key"]));

            var token = new JwtSecurityToken(
                issuer: _IConfiguration["Jwt:Issuer"],
                audience: _IConfiguration["Jwt:Audience"],
                expires: DateTime.Now.AddMinutes(20),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha256Signature)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Đăng ký
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<IdentityResult> SignUpAsync(UserRequest user)
        {
            var newuser = new User
            {
                Id = Guid.NewGuid().ToString(),
                UserName = user.UserName,
                Email = user.Email,
                FullName = user.FullName
            };

            return await _UserManager.CreateAsync(newuser, user.Password);
        }
    }
}
