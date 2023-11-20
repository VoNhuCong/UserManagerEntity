using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using UserManagerEntity.Models.Entities;
using UserManagerEntity.Models;
using UserManagerEntity.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using UserManagerEntity.Contract.Responses;
using UserManagerEntity.Contract.Requests;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using UserManagerEntity.Services;
using UserManagerEntity.Repository;

namespace UserManagerEntity.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly DataContext _context;
    private readonly TokenValidationParameters _tokenValidationParameters;
    private readonly JwtSettings _jwtSettings;
    private readonly SignInManager<User> _signInManager;
    private readonly IdentitySettings _identitySettings;
    private readonly IUserServices _IUserServices;
    private readonly IAccountRepository _IAccountRepository;

    /// <summary>
    /// Contructor
    /// </summary>
    /// <param name="userManager"></param>
    /// <param name="signInManager"></param>
    /// <param name="roleManager"></param>
    /// <param name="identitySettings"></param>
    /// <param name="context"></param>
    /// <param name="tokenValidationParameters"></param>
    /// <param name="jwtSettings"></param>
    /// <param name="IUserServices"></param>
    /// <param name="IAccountRepository"></param>
    public AuthController(
    UserManager<User> userManager,
    SignInManager<User> signInManager,
    RoleManager<Role> roleManager,
    IdentitySettings identitySettings,
    DataContext context,
    TokenValidationParameters tokenValidationParameters,
    JwtSettings jwtSettings,
    IUserServices IUserServices,
    IAccountRepository IAccountRepository)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _identitySettings = identitySettings;
        _context = context;
        _tokenValidationParameters = tokenValidationParameters;
        _jwtSettings = jwtSettings;
        _IUserServices = IUserServices;
        _IAccountRepository = IAccountRepository;
    }

    /// <summary>
    /// Đăng ký
    /// </summary>
    /// <param name="signUpModel"></param>
    /// <returns></returns>
    [HttpPost("SignUp")]
    public async Task<IActionResult> SignUp(UserRequest signUpModel)
    {
        if (!Extensions.IsEmail(signUpModel.Email))
        {
            return Ok("Email khong hop le");
        }

        var result = await _IAccountRepository.SignUpAsync(signUpModel);
        if (result.Succeeded)
        {
            return Ok(result.Succeeded);
        }
        else
        {
            return Ok(result.Succeeded);
        }
    }

    /// <summary>
    /// Đăng nhập
    /// </summary>
    /// <param name="signInModel"></param>
    /// <returns></returns>
    [HttpPost("SignIn")]
    public async Task<IActionResult> SignIn(LoginRequest signInModel)
    {
        var result = await _IAccountRepository.SignInAsync(signInModel);

        if (string.IsNullOrEmpty(result))
        {
            return Unauthorized();
        }

        return Ok();
    }
}

