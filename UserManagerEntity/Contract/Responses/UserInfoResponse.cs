namespace UserManagerEntity.Contract.Responses
{
    public class UserInfoResponse
    {
        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
    }
}
