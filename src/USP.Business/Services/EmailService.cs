using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using umbraco;
using Umbraco.Core.Logging;
using USP.Business.Constants;
using USP.Business.Enums;
using USP.Business.Extensions;
using USP.Business.Services.Interfaces;

namespace USP.Business.Services
{
    public class EmailService : IEmailService
    {
        public bool SendEmail(string to, string cc, string subject, string body, IEnumerable<Attachment> attachments, string from = null)
        {
            try
            {
                if (string.IsNullOrEmpty(from))
                {
                    from = SiteConstants.EmailServiceSmtpFromKey;
                }
                var host = library.GetDictionaryItem(SiteConstants.EmailServiceSmtpHostKey);

                var smtp = new SmtpClient();
                if (!string.IsNullOrEmpty(host) && host != "127.0.0.1")
                {
                    smtp = new SmtpClient(library.GetDictionaryItem(SiteConstants.EmailServiceSmtpHostKey));
                }
                

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(from),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                var toEmails = to.Split(';');
                toEmails.ForEach(x => mailMessage.To.Add(x));

                if (!string.IsNullOrEmpty(cc))
                {
                    mailMessage.CC.Add(cc);
                }

                attachments?.ForEach(x => mailMessage.Attachments.Add(x));

                smtp.Send(mailMessage);

                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Error(typeof(EmailService), $"Error sending email to:{to} subject{subject}", ex);
                return false;
            }
        }

        public bool SendEmail(string to, string cc, string subject, string body, string from, Dictionary<EmailTokens, string> valuePairs)
        {
            body = ReplaceMessageTokens(body, valuePairs);
            return SendEmail(to, cc, subject, body, null, from);
        }

        public string ReplaceMessageTokens(string message, Dictionary<EmailTokens, string> valuePairs)
        {
            var mssg = new StringBuilder(message);

            if (valuePairs == null) return mssg.ToString();

            foreach (var valuePair in valuePairs)
            {
                mssg = mssg.Replace(valuePair.Key.GetEnumDescription(), valuePair.Value);
            }

            return mssg.ToString();
        }
    }
}
