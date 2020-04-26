using Net_Elsayed_Mohammed_Elsayed_Ali_Leilla.Models;
using System;
using System.Net;
using System.Net.Mail;

namespace Net_Elsayed_Mohammed_Elsayed_Ali_Leilla.Helper
{
    public class Email
    {
        private Table_Users _user;
        private string _link;

        public Email(Table_Users user,string link)
        {
            _user = user;
            _link = link;

        }

        public bool SendEmail()
        {
            try
            {
                var mailBody = "Hi, " + _user.Name + " " + "Lets Confirm You Email: "
                + _user.Email + ". " + "Please Click This Link To Confirm Your Email On Our Website: "
                + _link;

                var smartZoneMail = "net.elsayed.leilla.smart.zone@gmail.com";
                var message = new MailMessage();
                var smtp = new SmtpClient();
                message.From = new MailAddress(smartZoneMail);
                message.To.Add(new MailAddress(_user.Email));
                message.Subject = "SmartZoneTest";
                message.IsBodyHtml = false;  
                message.Body = mailBody;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; 
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(smartZoneMail, "123qawdqwd$@@#SACAC");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
                return true;
            }
            catch (Exception e){
                 return false;
            }
        }

       
    }
}
