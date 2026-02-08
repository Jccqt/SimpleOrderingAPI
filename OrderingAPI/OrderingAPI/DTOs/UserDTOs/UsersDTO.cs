namespace OrderingAPI.DTOs.UserDTOs
{
    public class UsersDTO
    {
        public required int UserID { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string CreatedAt { get; set; }
    }
}
