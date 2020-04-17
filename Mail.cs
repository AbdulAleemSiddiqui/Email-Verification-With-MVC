using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace FYP1.Models
{
    class Mail
    {
        //Company_User and Company_Code and Company_Address and Company_Link
        //
        public string ConfimationLink { get; set; }
        public string ConfirmationMessage { get; set; }

        public string UserName { set; get; }
        public string UserMail { set; get; }
        public string Subject { set; get; }

        private string email = "fatimaakbar538@gmail.com", password = "fatima8852";

        private string Company_Name { get; set; } = "Pakistan Study Panel";
        private string Company_Address { get; set; } = "Abc, Karachi";

        private string SenderMail { set { value = email; } get { return email; } }
        private string SenderPassword { set { value = password; } get { return password; } }
        public void Sent()
        {
            SetMessage();
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(SenderMail);
                mail.To.Add(UserMail);
                mail.Subject = Subject;
                mail.Body = ConfirmationMessage;
                mail.IsBodyHtml = true;
                //mail.Attachments.Add(new Attachment("C:\\file.zip"));

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(SenderMail, SenderPassword);
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
        }
        private void SetMessage()
        {
            ConfirmationMessage = "";
            foreach (var item in File.ReadAllLines(@"E:\Rafay FYP\fyp folder\FYP1 - Copy\verify-email.html"))
            {
                ConfirmationMessage += item.Replace("Company_Code", Company_Name)
                    .Replace("Company_Address", Company_Address)
                    .Replace("Company_Link", ConfimationLink)
                    .Replace("Company_User", UserName)+ " \n";

            }
        }
        public void SetCredientials(string a, string b)
        {
            email = a; password = b;
        }


    }
}