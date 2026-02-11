namespace OrderingAPI.DTOs.UserDTOs
{
    public class UpdateUserDTO
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
        public int? Status { get; set; }
    }
}
