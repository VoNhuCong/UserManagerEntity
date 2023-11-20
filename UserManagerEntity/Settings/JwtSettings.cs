namespace UserManagerEntity.Settings
{
    public class JwtSettings
    {
        /// <summary>
        /// Issuer of the token
        /// Tổ chức phát hành token, this app
        /// </summary>
        public string Issuer { get; set; } = string.Empty;

        /// <summary>
        /// Audience of the token
        /// Thực thể nhận token, user
        /// </summary>
        public string Audience { get; set; } = string.Empty;

        /// <summary>
        /// Secret key for JWT generation
        /// </summary>
        public string Key { get; set; } = string.Empty;

        /// <summary>
        /// JWT lifetime
        /// khoảng thời gian token có giá trị
        /// </summary>
        public TimeSpan Lifetime { get; set; }

        /// <summary>
        /// Refresh token Time To Live (TTL) in days.
        /// Inactive tokens are automatically deleted from the database after this time.
        /// thời gian tồn tại của token refresh 
        /// </summary>
        public int RefreshTokenTTL { get; set; }

        /// <summary>
        /// ClockSkew to set token expiration time offset
        /// </summary>
        public TimeSpan ClockSkew { get; set; }
    }
}
