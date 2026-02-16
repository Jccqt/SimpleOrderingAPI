namespace UserService.DTOs.AuthDTOs
{
    public class RefreshTokenRequestDTO
    {
        public string ExpiredToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
