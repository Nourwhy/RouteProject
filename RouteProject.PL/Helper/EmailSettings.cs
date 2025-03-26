using System.Net;
using System.Net.Mail;

namespace RouteProject.PL.Helper
{
    public static class EmailSettings
    {
        public static bool SendEmail(Email email)
        {
            try
            {
                var client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl=true;
                client.Credentials = new NetworkCredential("nourwafik987@gmail.com", "kcihbckfrqqetesu");
                client.Send("nourwafik987@gmail.com", email.To, email.Subject, email.Body);

                return true;    
                    

            }
            catch (Exception e)
            {

                return false;
            }

        }
    }
}
