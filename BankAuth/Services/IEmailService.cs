using BankAuth.Models;

namespace BankAuth.Services
{
    public interface IEmailService
    {
        void SendEmail(Message message);
    }
}
