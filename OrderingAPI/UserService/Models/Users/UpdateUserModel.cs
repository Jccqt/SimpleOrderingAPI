namespace UserService.Models.Users
{
    public class UpdateUserModel
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
        public int? Status { get; set; }
    }
}
