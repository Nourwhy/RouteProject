using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using RouteProject.PL.Settings;


namespace RouteProject.PL.Helper
{
    public class MailServices(IOptions<MailSettings> _options) : IMailServices
    {
       
        public void SendEmail(Email email)
        {
            //Build Message
            var mail = new MimeMessage();

            mail.Subject = email.Subject;
            mail.From.Add(new MailboxAddress(_options.Value.DisplayName , _options.Value.Email));
            mail.To.Add(MailboxAddress.Parse(email.To));

            var builder = new BodyBuilder();
            builder.TextBody = email.Body;
            mail.Body=builder.ToMessageBody();


            //Establish connection

            using var smpt = new SmtpClient();
            smpt.Connect(_options.Value.Host, _options.Value.Port, MailKit.Security.SecureSocketOptions.StartTls);
            smpt.Authenticate(_options.Value.Email, _options.Value.Password);

            //Send Message
            smpt.Send(mail);

        }
    }
}
