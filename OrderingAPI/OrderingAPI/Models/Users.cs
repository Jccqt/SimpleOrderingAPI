namespace OrderingAPI.Models
{
    public class Users
    {
        public int user_id { get; set; }
        public required string full_name { get; set; }
        public required string email { get; set; }
        public required DateTime created_at { get; set; }
    }
}
