namespace UserService.DTOs.V1.UserDTOs
{
    public class UpdateUserDTO
    {
        public int UserID { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
        public int? Status { get; set; }
    }
}
