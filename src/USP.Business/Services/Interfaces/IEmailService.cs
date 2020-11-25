using System.Collections.Generic;
using System.Net.Mail;
using USP.Business.Enums;

namespace USP.Business.Services.Interfaces
{
    public interface IEmailService
    {
        bool SendEmail(string to, string cc, string subject, string body, IEnumerable<Attachment> attachments, string from = null);
        bool SendEmail(string to, string cc, string subject, string body, string from, Dictionary<EmailTokens, string> valuePairs);
        string ReplaceMessageTokens(string message, Dictionary<EmailTokens, string> valuePairs);
    }
}
