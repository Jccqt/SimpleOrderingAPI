namespace OrderingAPI.Models
{
    public class Users
    {
        public int user_id { get; set; }
        public required string full_name { get; set; }
        public required string email { get; set; }
        public required string password { get; set; }
        public required string salt { get; set; }
        public DateTime created_at { get; set; }
        public int status { get; set; }
    }
}
