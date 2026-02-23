namespace UserService.Models.Auth
{
    public class UserSessionsModel
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public DateTime? Created { get; set; }
    }
}
