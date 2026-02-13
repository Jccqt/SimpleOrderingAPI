namespace OrderingAPI.DTOs.AuthDTOs
{
    public class UserSessionsDTO
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public DateTime? Created { get; set; }
    }
}
