using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserManagerEntity.Contract.Responses;
using UserManagerEntity.Models;
using UserManagerEntity.Models.Entities;

namespace UserManagerEntity.Services;
public interface IUserServices
{
    
    /// <summary>
    /// Chuyển đổi User thành một dạng khác tùy theo nhu cầu ứng dụng
    /// </summary>
    /// <param name="user"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<UserInfoResponse> UserToUserInfoAsync(User user, CancellationToken cancellationToken = default);

    /// <summary>
    /// Trả về True nếu password khớp với thông tin password trong db
    /// </summary>
    /// <param name="user"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public Task<bool> CheckPasswordAsync(User user, string password);

    /// <summary>
    /// Trả về User lấy từ Id của User
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task<User> GetAsync(string id);
}
public class UserServices : IUserServices
{
    private readonly UserManager<User> _UserManager;

    public UserServices(UserManager<User> userManager)
    {
        _UserManager = userManager;
    }
    public async Task<UserInfoResponse> UserToUserInfoAsync(User user, CancellationToken cancellationToken = default)
    {
        var result = new UserInfoResponse()
        {
            FullName = user.UserName,
            UserEmail = user.Email,
            UserId = user.Id,
            UserName = user.UserName
        };
        return result;
    }
    public async Task<bool> CheckPasswordAsync(User user, string password)
    {
        return await _UserManager.CheckPasswordAsync(user, password);
    }
    public async Task<User?> GetAsync(string id)
    {
        return await _UserManager.FindByIdAsync(id);
    }
}
