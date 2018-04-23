namespace TwitterBackUp.DomainModels
{
    public class UsersTwitters
    {
        public string UserId { get; set; }
        public string TwitterId { get; set; }
        public User User { get; set; }
        public Twitter Twitter { get; set; }
    }
}