using UserManagerEntity.Models.Entities;

namespace UserManagerEntity.Contract.Responses;

public class LoginResponse
{
    public UserInfoResponse? UserInfo { get; set; }
    public string Token { get; set; } = string.Empty;
    /// <summary>
    /// Là loại token để tạo một token mới khi nó hết hạn, có thời gian tồn tại lâu hơn token
    /// Thời gian tồn tại thường vài ngày hoặc vài tuần
    /// </summary>
    public string RefreshToken { get; set; } = string.Empty;
}
