﻿using SIG.Infrastructure.Configs;
using SIG.Infrastructure.Helper;
using System.Net;
using System.Net.Mail;

namespace SIG.Infrastructure.Email
{
    public class SMTPService : IEmailService
    {
       
        public void SendMail(string sender,string senderEmail, string mailTo, string mailcc, string subject, string body, 
          string smtpServer,  string fromEmail, string displayName, string userName, string password,int port, bool enableSsl)
        {
            MailMessage message = new MailMessage();

            message.To.Add(mailTo);
            if(!string.IsNullOrEmpty(mailcc))
                message.CC.Add(mailcc); 

            message.Subject = subject;
            message.Body = body; //string.Format("<p>{0}</p><p>发件人：{1} ({2}), 发件人邮箱：{3}</p>", body, name, phone, from);
            message.IsBodyHtml = true;

            message.ReplyToList.Add(new MailAddress(senderEmail, sender));
            //if (!string.IsNullOrEmpty(mailcc))
            //    message.ReplyToList.Add(new MailAddress(mailTo, sender));

            message.From = new MailAddress(fromEmail, displayName);
            SmtpClient smtpClient = new SmtpClient(smtpServer, port)
            {
                UseDefaultCredentials = true,
                EnableSsl = enableSsl,
                //   smtpClient.Port = SettingsManager.SMTP.Port;
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(userName, password)
            };

            smtpClient.Send(message);
        }
    }

}
