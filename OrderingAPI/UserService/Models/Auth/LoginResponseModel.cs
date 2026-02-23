namespace UserService.Models.Auth
{
    public class LoginResponseModel
    {
        public required bool Success { get; set; }
        public int UserID { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}
