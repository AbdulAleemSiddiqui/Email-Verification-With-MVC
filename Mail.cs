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
        public string ConfimationLink { get; set; }
        public string ConfirmationMessage { get; set; }

        public string UserName { set; get; }
        public string UserMail { set; get; }
        public string Subject { set; get; }

        private string _email = "abc@hotmail.com", _password = "password123"; //You can hardcore your credentials
        
        private string _company_Name = "ABC Traders", _company_Address = "somewhere in Pakistan"; //You can hardcore your Company Info
        
        private string _template_Path { set;get; } = @"emailTemplate.html";  //Template which have some key words to replace like Company_Code, Company_Address, Company_Link, Company_User
            
        public string Company_Name { value = _company_Name; } get { return _company_Name; }
        public string Company_Address { value = _company_Address; } get { return _company_Address; }

        public string SenderMail set { value = _email; } get { return _email; }
        public string SenderPassword { set { value = _password; } get { return _password; } }
        public void Sent(string? attachmentPath)
        {
            SetMessage();
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(SenderMail);
                mail.To.Add(UserMail);
                mail.Subject = Subject;
                mail.Body = ConfirmationMessage;
                mail.IsBodyHtml = true;
                if(attachmentPath.HasValue)
                {
                    mail.Attachments.Add(new Attachment(attachmentPath));
                }
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
            foreach (var item in File.ReadAllLines(_template_Path))
            {
                ConfirmationMessage += item.Replace("Company_Code", Company_Name)
                    .Replace("Company_Address", Company_Address)
                    .Replace("Company_Link", ConfimationLink)
                    .Replace("Company_User", UserName)+ " \n";

            }
        }
        public void SetCredientials(string email, string password)
        {
            _email = email; _password = password;
        }
        public void SetCompany(string company_Name, string company_Address)
        {
            _company_Name = company_Name; _company_Address = company_Address;
        }
        public void SetTemplate(string templatePath)
        {
            _template_Path=templatePath;
        }
    }
}
