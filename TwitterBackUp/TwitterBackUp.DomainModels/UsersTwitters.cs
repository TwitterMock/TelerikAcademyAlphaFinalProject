using TwitterBackUp.DomainModels.Contracts;

namespace TwitterBackUp.DomainModels
{
    public class UsersTwitters : IDomainModel
    {
        public string UserId { get; set; }
        public string TwitterId { get; set; }
        public Twitter Twitter { get; set; }
    }
}