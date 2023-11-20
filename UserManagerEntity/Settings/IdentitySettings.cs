using Microsoft.AspNetCore.Identity;

namespace UserManagerEntity.Settings
{
    public class IdentitySettings
    {
        public LockoutOptions Lockout { get; set; } = new();
        public string DefaultUserName { get; set; } = string.Empty;
        public string DefaultPassword { get; set; } = string.Empty;
    }
}
