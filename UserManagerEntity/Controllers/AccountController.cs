using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Common;
using UserManagerEntity.Contract.Requests;
using UserManagerEntity.Repository;
//https://www.c-sharpcorner.com/article/jwt-authentication-and-authorization-in-net-6-0-with-identity-framework/
namespace UserManagerEntity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _IAccountRepository;
        public AccountController(IAccountRepository IAccountRepository)
        {
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
            var result = await _IAccountRepository.SignUpAsync(signUpModel);
            if (result.Succeeded)
            {
                return Ok(result.Succeeded);
            }
            else
            {
                return Ok(result);
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

            return Ok(result);
        }
    }
}
