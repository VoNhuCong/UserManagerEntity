namespace UserManagerEntity.Contract.Responses
{
    public class AuthResponse
    {
        /// <summary>
        /// JWT (JSON Web Token)
        ///     
        ///     JWT Token is short-lived.
        ///     Must call RefreshToken endpoint to get new one after the expiration.
        ///     
        /// </summary>
        /// <example>eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiI5ZjdhN2QyZS1hYmQ4LTQ1MTQtODM4YS1kNjFkOTExZDQzYjgiLCJpZCI6ImVkNzNiODE5LWVkMzUtNDJlZi1iNjBjLTdhY2I3Yzc4N2Q4NSIsInVzZXJOYW1lIjoiQ29vbFVzZXJOYW1lIiwiZGlzcGxheU5hbWUiOiJL4bu5IHRodeG6rXQgdmnDqm4gxJHDoGkgaHV54buHbiIsInJlZ2lvbklkIjoiMSIsImV4cCI6MTY0ODUyNDUwOCwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo1MDAwIiwiYXVkIjoiaHR0cDovL2xvY2FsaG9zdDo1MDAwIn0.HnrOfSk0Z077neXnIv77WgocFh3cX8osUz4Li0W8WXU</example>
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// Refresh token for current session
        ///     
        ///     Refresh Token expires after few days.
        ///     Must re-login after that time since last refresh.
        ///     
        /// </summary>
        /// <example>A4zEjXMnRVMYDLwMiHnYsiwQ1BnoIoTp66Huz1uEtjXxOMI78eBESJoX3eoRZLSvhtsVda3uegKdzxT0SQyTjg==</example>
        public string RefreshToken { get; set; } = string.Empty;
    }
}
