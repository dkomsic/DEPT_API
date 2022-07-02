using DEPT_Api.Models;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Web.Mvc;

namespace DEPT_Api.Controllers
{
    public class EmailController : Controller
    {
        public ActionResult Index(EmailModel e)
        {
            if (e.Name != null && e.Email != null)
            {
                var toAddress = e.Email;
                var subject = "Requested trailer is here!";
                var message = "Dear " + e.Name + " visit this link to see requested trailer:";

                var tEmail = new Thread(() => SendEmail(toAddress, subject, message));
                tEmail.Start();
            }
            return View();
        }

        private void SendEmail(string toAddress, string subject, string message)
        {
            {
                try
                {
                    using (var mail = new MailMessage())
                    {
                        const string email = "komsicdanijel2@gmail.com"; //temp email
                        const string password = "***************";
                        var loginInfo = new NetworkCredential(email, password);

                        mail.From = new MailAddress(email);
                        mail.To.Add(new MailAddress(toAddress));
                        mail.Subject = subject;
                        mail.Body = message;
                        mail.IsBodyHtml = true;

                        try
                        {
                            using (var smtpClient = new SmtpClient())
                            {
                                smtpClient.Host = "smtp.gmail.com";
                                smtpClient.Port = 587;
                                smtpClient.EnableSsl = true;
                                smtpClient.UseDefaultCredentials = false;
                                smtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                                smtpClient.Credentials = loginInfo;
                                smtpClient.Send(mail);
                            }
                        }

                        finally
                        {
                            mail.Dispose();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.ToString());
                }

            }
        }
    }
}