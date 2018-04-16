using System.Threading.Tasks;

namespace TwitterBackUp.Data.Identity.ExternalServices
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
