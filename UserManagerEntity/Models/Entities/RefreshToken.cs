using System.Text.Json.Serialization;

namespace UserManagerEntity.Models.Entities;
public class RefreshToken
{
    public string Id { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public DateTime CreationDate { get; set; }
    public DateTime ExpiresDate { get; set; }
    public string CreatedByIp { get; set; } = string.Empty;
    public bool IsUsed { get; set; }
    public bool IsInvalidated { get; set; }
    public string UserId { get; set; } = string.Empty;
    [JsonIgnore]
    public User? User { get; set; }
}
