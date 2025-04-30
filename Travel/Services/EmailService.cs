using MailKit.Net.Smtp;
using MimeKit;

namespace TravelistaMVC.Services
{
    public class EmailService
    {
        public async Task SendBookingEmail(string toEmail, string subject, string body)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("kalmahanesjan@gmail.com")); 
            email.To.Add(MailboxAddress.Parse(toEmail));
            email.Subject = subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = body
            };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync("kalmahanesjan@gmail.com", "cowf bhme sfen gszw"); 
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true); 



        }

    }
}
