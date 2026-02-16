namespace UserService.DTOs.AuthDTOs
{
    public class LoginResponseDTO
    {
        public bool Success { get; set; }
        public int? UserID { get; set; }
        public string? FullName { get; set; }
        public string? Role { get; set; }
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
    }
}
