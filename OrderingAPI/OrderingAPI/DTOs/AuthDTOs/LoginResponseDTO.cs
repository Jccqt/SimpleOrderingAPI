namespace OrderingAPI.DTOs.AuthDTOs
{
    public class LoginResponseDTO
    {
        public bool Success { get; set; }
        public string? UserID { get; set; }
        public string? FullName { get; set; }
        public string? Role { get; set; }
        public string? Token { get; set; }
    }
}
