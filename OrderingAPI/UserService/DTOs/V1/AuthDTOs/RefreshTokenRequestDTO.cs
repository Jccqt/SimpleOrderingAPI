namespace UserService.DTOs.V1.AuthDTOs
{
    public class RefreshTokenRequestDTO
    {
        public string ExpiredToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
