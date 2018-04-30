using TwitterBackUp.DomainModels.Contracts;

namespace TwitterBackUp.DomainModels
{
    public class UsersTwitters
    {
        public string UserId { get; set; }
        public string TwitterId { get; set; }
        public Twitter Twitter { get; set; }
    }
}