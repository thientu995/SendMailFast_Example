using SendMailFast;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
    class Program
    {
        const string formatDateTime = "yyyy/MM/dd hh:mm:ss";
        const string EmailTo = "email@olitvn.com";
        static void Main(string[] args)
        {
            // Setting
            SendMailSetting.ServerMail = "smtp.gmail.com";//GSuite "smtp-relay.gmail.com";
            SendMailSetting.PortMail = 587;
            SendMailSetting.EnableSsl = true;
            SendMailSetting.Password = "password";
            SendMailSetting.EmailCredential = new EmailInfo()
            {
                EmailAddress = "email@olitvn.com",
                DisplayName = "Display Name"
            };

            // Send mail
            using (SendMail send = new SendMail())
            {
                send.AddEmailTo(new EmailInfo() { EmailAddress = EmailTo });

                Console.WriteLine("Begin SendAsync: " + DateTime.Now.ToString(formatDateTime));
                send.SendAsync("Title mail Async", "Content Mail");
                Console.WriteLine("End SendAsync: " + DateTime.Now.ToString(formatDateTime));

                Console.WriteLine("Begin Send: " + DateTime.Now.ToString(formatDateTime));
                send.Send("Title mail", "Content Mail");
                Console.WriteLine("End Send: " + DateTime.Now.ToString(formatDateTime));
            }

            // Send error from exception
            try
            {
                int a = int.Parse("a");
            }
            catch (Exception ex)
            {
                SendMail sendEx = new SendMail();

                Console.WriteLine("Begin SendErrorAsync: " + DateTime.Now.ToString(formatDateTime));
                sendEx.SendErrorAsync("Title mail exception Async", ex);
                Console.WriteLine("End SendErrorAsync: " + DateTime.Now.ToString(formatDateTime));

                Console.WriteLine("Begin SendError: " + DateTime.Now.ToString(formatDateTime));
                sendEx.SendError("Title mail exception", ex);
                Console.WriteLine("End SendError: " + DateTime.Now.ToString(formatDateTime));
            }

            // Send mail template html with resources
            string htmlBody = "<html><body><h1>Picture</h1><br><img src=\"cid:resource.png\"></body></html>";
            SendMail sendResource = new SendMail(new EmailResource(Path.Combine(Directory.GetCurrentDirectory(), "resource.png"), "resource.png"));
            sendResource.AddAttachment(new EmailAttachment(Path.Combine(Directory.GetCurrentDirectory(), "resource.png"), "fileNameAttachment.pdf"));
            sendResource.Send("Test mail resource", htmlBody);
        }
    }
}
